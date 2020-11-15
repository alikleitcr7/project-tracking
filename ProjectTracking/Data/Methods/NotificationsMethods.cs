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
    public class NotificationMethods : INotificationMethods
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationsHub> _notificationsHub;

        public NotificationMethods(IMapper mapper, IHubContext<NotificationsHub> notificationsHub, IConfiguration config)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Setting.ConnectionString);
            _context = new ApplicationDbContext(optionsBuilder.Options, config);
            _mapper = mapper;
            //_context = context;
            _notificationsHub = notificationsHub;
        }


        private IQueryable<DataSets.UserNotification> NotificationsPopulated()
        {
            return _context.UserNotifications
                .Include(k => k.FromUser)
                .Include(k => k.ToUser);
        }
        private IQueryable<DataSets.Broadcast> BroadcastPopulate()
        {
            return _context.Broadcasts
                .Include(k => k.FromUser);
            //.Include(k => k.ToTeam);
        }


        public async Task<Broadcast> SendToTeam(string fromUserId, int toTeamId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false)
        {
            // get users under the team that user is supervising...
            // this will validate the supervisor constraint
            List<string> userIds = _context.Users.Where(k => k.TeamId == toTeamId)
                           .Select(k => k.Id).ToList();

            if (userIds.Count == 0)
            {
                throw new ClientException("there are no users under the team");
            }

            DataSets.Broadcast broadcast = new DataSets.Broadcast()
            {
                FromUserId = fromUserId,
                ToTeamId = toTeamId,
                Message = message,
                NotificationTypeCode = (short)notificationType,
                DateSent = DateTime.Now
            };

            _context.Broadcasts.Add(broadcast);
            _context.SaveChanges();

            Broadcast sent = GetBroadcast(broadcast.ID);

            if (sendLiveNotification && sent != null)
            {
                foreach (var user in userIds)
                {
                    SetHasNotificationFlag(user, true);
                    await _notificationsHub.Clients.User(user).SendAsync("ReceiveNotification", sent);
                }
                //await _notificationsHub.Clients.Users(userIds).SendAsync("ReceiveNotification", sent);
            }

            return sent;
        }


        public void MarkAsRead(int id)
        {
            var dbNotification = _context.UserNotifications.FirstOrDefault(k => k.ID == id);

            if (dbNotification == null)
            {
                throw new ClientException("not found");
            }

            dbNotification.IsRead = true;

            _context.SaveChanges();
        }

        public void SetHasNotificationFlag(string userId, bool hasNotificaion)
        {
            var dbUser = _context.Users.FirstOrDefault(k => k.Id == userId);

            if (dbUser == null)
            {
                throw new ClientException("not found");
            }

            dbUser.NotificationFlag = hasNotificaion;

            _context.SaveChanges();
        }

        public bool GetHasNotificationFlag(string userId)
        {
            var dbUser = _context.Users.FirstOrDefault(k => k.Id == userId);

            if (dbUser == null)
            {
                throw new ClientException("not found");
            }

            return dbUser.NotificationFlag;
        }

        public async Task<UserNotification> Send(string fromUserId, string toUserId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false, int? timesheetId = null, int? projectId = null, int? taskId = null)
        {
            DataSets.UserNotification notification = new DataSets.UserNotification()
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message,
                NotificationTypeCode = (short)notificationType,
                DateSent = DateTime.Now,
                TimeSheetId = timesheetId,
                ProjectId = projectId,
                ProjectTaskId = taskId
            };

            _context.UserNotifications.Add(notification);
            _context.SaveChanges();

            UserNotification sent = Get(notification.ID);

            if (sendLiveNotification && sent != null)
            {
                SetHasNotificationFlag(sent.ToUserId, true);

                await _notificationsHub.Clients.User(sent.ToUserId).SendAsync("ReceiveNotification", sent);
            }

            return sent;
        }

        public UserNotification Get(int id)
        {
            var dbNotification = NotificationsPopulated().FirstOrDefault(k => k.ID == id);

            if (dbNotification != null)
            {
                return _mapper.Map<UserNotification>(dbNotification);
            }

            return null;
        }

        public Broadcast GetBroadcast(int id)
        {
            var dbBroadcast = BroadcastPopulate().FirstOrDefault(k => k.ID == id);

            if (dbBroadcast != null)
            {
                return _mapper.Map<Broadcast>(dbBroadcast);
            }

            return null;
        }

        public List<UserNotification> GetToUser(string toUserId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.ToUserId == toUserId);

            totalCount = records.Count();

            List<UserNotification> notifications = new List<UserNotification>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<UserNotification>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }

        public List<UserNotification> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.FromUserId == fromUserId);

            totalCount = records.Count();

            List<UserNotification> notifications = new List<UserNotification>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<UserNotification>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }


    }
}
