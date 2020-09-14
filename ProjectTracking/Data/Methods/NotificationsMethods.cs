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


        private IQueryable<DataSets.Notification> NotificationsPopulated()
        {
            return _context.Notifications
                .Include(k => k.FromUser)
                .Include(k => k.ToUser);
        }


        public async Task<Notification> Send(string fromUserId, string toUserId, string message, NotificationType notificationType = NotificationType.Default, bool sendLiveNotification = false)
        {
            DataSets.Notification notification = new DataSets.Notification()
            {
                FromUserId = fromUserId,
                ToUserId = toUserId,
                Message = message,
                NotificationTypeCode = (short)notificationType,
                DateSent = DateTime.Now
            };

            _context.Notifications.Add(notification);
            _context.SaveChanges();

            Notification sent = Get(notification.ID);

            if (sendLiveNotification && sent != null)
            {
                await _notificationsHub.Clients.User(sent.ToUserId).SendAsync("ReceiveNotification", sent);
            }

            return sent;
        }

        public Notification Get(int id)
        {
            var dbNotification = NotificationsPopulated().FirstOrDefault(k => k.ID == id);

            if (dbNotification != null)
            {
                return _mapper.Map<Notification>(dbNotification);
            }

            return null;
        }

        public List<Notification> GetToUser(string toUserId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.ToUserId == toUserId);

            totalCount = records.Count();

            List<Notification> notifications = new List<Notification>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<Notification>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }

        public List<Notification> GetFromUser(string fromUserId, int page, int countPerPage, out int totalCount)
        {
            totalCount = 0;

            var records = NotificationsPopulated().Where(k => k.FromUserId == fromUserId);

            totalCount = records.Count();

            List<Notification> notifications = new List<Notification>();

            if (totalCount == 0)
            {
                return notifications;
            }

            notifications = records.Select(_mapper.Map<Notification>).OrderByDescending(k => k.DateSent).ToList();

            notifications = notifications.Skip(page * countPerPage).Take(countPerPage).ToList();

            return notifications;
        }


    }
}
