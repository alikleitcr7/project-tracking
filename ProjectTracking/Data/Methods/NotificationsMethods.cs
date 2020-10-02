using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
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

        public async Task<UserNotification> Send(string fromUserId, string toUserId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false)
        {
            DataSets.UserNotification notification = new DataSets.UserNotification()
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message,
                NotificationTypeCode = (short)notificationType,
                DateSent = DateTime.Now
            };

            _context.UserNotifications.Add(notification);
            _context.SaveChanges();

            UserNotification sent = Get(notification.ID);

            if (sendLiveNotification && sent != null)
            {
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
