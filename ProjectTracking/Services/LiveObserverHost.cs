﻿using Microsoft.Extensions.Hosting;
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
using Microsoft.Extensions.DependencyInjection;

namespace ProjectTracking.Services
{
    public class LiveObserverHost : IHostedService, IDisposable
    {
        private Timer _timer;
        private const int interval = 600000;
        private readonly IUserMethods _users;
        public IServiceProvider Services { get; }
        private readonly INotificationMethods _notifications;

        //private readonly INotificationMethods _notificationMethods;
        private readonly IConfiguration _config;

        //private List<string> managerIds = new List<string>();
        //private string systemAdminId;
        public bool CheckedToday { get; set; }

        public LiveObserverHost(IServiceProvider services, IConfiguration config, INotificationMethods notifications)
        {
            //IUserMethods users, 
            _config = config;
            Services = services;
            //_users = users;
            _notifications = notifications;
            //_notificationMethods = notificationMethods;
            //systemAdminId = _config.GetValue<string>("Tokens:Admin");
            //managerIds = _users.GetUsersInRole("Manager");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IUserMethods>();

                _users = scopedProcessingService;
            }
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
                // will be considered disconnected

                List<UserLog> inactiveUsers = new List<UserLog>();

                // end log for inactive users
                foreach (var activeUsr in activeUsers)
                {
                    if (!observedUsers.Any(k => k.UserId == activeUsr.UserId))
                    {
                        inactiveUsers.Add(activeUsr);
                        _users.EndActiveLog(activeUsr.UserId, UserLogStatus.Disconnected);
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
