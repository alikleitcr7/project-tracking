using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Hubs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class BroadcastsMethods : IBroadcastsMethods
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<BroadcastsHub> _notificationsHub;

        public BroadcastsMethods(IMapper mapper, IHubContext<BroadcastsHub> notificationsHub, IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Setting.ConnectionString);
            _context = new ApplicationDbContext(optionsBuilder.Options, config);
            _mapper = mapper;
            //_context = context;
            _notificationsHub = notificationsHub;
        }

        private IQueryable<DataSets.Broadcast> NotificationsPopulated()
        {
            return _context.Broadcasts
                .Include(k => k.FromUser)
                .Include(k => k.ToTeam);
        }


        public void MarkAsRead(int id)
        {
            var dbBroadcast = _context.Broadcasts.FirstOrDefault(k => k.ID == id);

            if (dbBroadcast == null)
            {
                throw new ClientException("not found");
            }

            dbBroadcast.IsRead = true;

            _context.SaveChanges();
        }

        public async Task<Broadcast> Send(string fromUserId, int toTeamId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false)
        {
            // get users under the team that user is supervising...
            // this will validate the supervisor constraint
            List<string> userIds = _context.Users.Where(k => k.TeamId == toTeamId)
                           .Select(k => k.Id).ToList();

            if (userIds.Count == 0)
            {
                throw new ClientException("there are no users under the team");
            }

            DataSets.Broadcast notification = new DataSets.Broadcast()
            {
                FromUserId = fromUserId,
                ToTeamId = toTeamId,
                Message = message,
                NotificationTypeCode = (short)notificationType,
                DateSent = DateTime.Now
            };

            _context.Broadcasts.Add(notification);
            _context.SaveChanges();

            Broadcast sent = Get(notification.ID);

            if (sendLiveNotification && sent != null)
            {
                await _notificationsHub.Clients.Users(userIds).SendAsync("ReceiveNotification", sent);
            }

            return sent;
        }

        public Broadcast Get(int id)
        {
            var dbNotification = NotificationsPopulated().FirstOrDefault(k => k.ID == id);

            if (dbNotification != null)
            {
                return _mapper.Map<Broadcast>(dbNotification);
            }

            return null;
        }

        public List<Broadcast> GetToTeam(int toTeamId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.ToTeamId == toTeamId);

            totalCount = records.Count();

            List<Broadcast> notifications = new List<Broadcast>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<Broadcast>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }

        public List<Broadcast> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.FromUserId == fromUserId);

            totalCount = records.Count();

            List<Broadcast> notifications = new List<Broadcast>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<Broadcast>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }
    }
}
