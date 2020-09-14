using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Hubs;
using ProjectTracking.DataContract.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;

namespace ProjectTracking.Services
{
    public class LiveObserverHost : IHostedService, IDisposable
    {
        private Timer _timer;
        private const int interval = 600000;
        private readonly IUserMethods _users;
        private readonly INotificationMethods _notifications;

        //private readonly INotificationMethods _notificationMethods;
        private readonly IConfiguration _config;

        private List<string> managerIds = new List<string>();
        private string systemAdminId;
        public bool CheckedToday { get; set; }

        public LiveObserverHost(IUserMethods users, IConfiguration config, INotificationMethods notifications)
        {
            _config = config;
            _users = users;
            _notifications = notifications;
            //_notificationMethods = notificationMethods;
            systemAdminId = _config.GetValue<string>("Tokens:Admin");
            managerIds = _users.GetUsersInRole("Manager");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, 2000, interval);


            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //check active logs

            //|| ApplicationContext.LogsLastUpdatedDate.Value > DateTime.Now

            try
            {
                DateTime dateNow = DateTime.Now;


                // CHECK ACTIVITIES AT 4 PM EVERY WORKING WEEK DAY

                if (dateNow.DayOfWeek != DayOfWeek.Saturday && dateNow.DayOfWeek != DayOfWeek.Sunday)
                {
                    const int HOUR_TO_CHECK = 14;

                    if (CheckedToday && dateNow.Hour < HOUR_TO_CHECK)
                    {
                        CheckedToday = false;
                    }
                    else if (!CheckedToday && dateNow.Hour >= HOUR_TO_CHECK)
                    {
                        // check activities here

                        //List<User> usersWithNoActivity = _users.UsersNotRegisteredTimeSheetActivityToday();

                        //if (usersWithNoActivity.Count > 0)
                        //{
                        //    string userNames = string.Join(", ", usersWithNoActivity.Select(k => k.FullName).ToArray());
                        //    string message = $"The following employees did not fill any activity today ${userNames}";

                        //    foreach (string managerId in managerIds)
                        //    {
                        //        Notification sent = _notifications.Send(systemAdminId, managerId, message, NotificationType.Important, true).Result;
                        //    }
                        //}

                        CheckedToday = true;
                    }
                }




                if (!ApplicationContext.LogsLastUpdatedDate.HasValue)
                {
                    ApplicationContext.ActiveLogs = _users.GetActiveLogs();
                }


                List<ObservedUser> observedUsers = ObserverHub.Users;
                List<UserLog> activeUsers = ApplicationContext.ActiveLogs;

                if (activeUsers == null || observedUsers == null)
                {
                    return;
                }

                // any active users not observed after interval of time 
                // will be considered out

                List<UserLog> inactiveUsers = new List<UserLog>();

                // end log for inactive users
                foreach (var activeUsr in activeUsers)
                {
                    if (!observedUsers.Any(k => k.UserId == activeUsr.UserId))
                    {
                        inactiveUsers.Add(activeUsr);
                        _users.EndLog(activeUsr.UserId, "Disconnected");
                    }
                }

                // remove inactive users from active
                foreach (var inactiveUser in inactiveUsers)
                {
                    ApplicationContext.ActiveLogs.Remove(inactiveUser);
                }
            }
            catch (Exception ex)
            {

            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
