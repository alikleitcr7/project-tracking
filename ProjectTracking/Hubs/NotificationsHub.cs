﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Hubs
{
    public class NotificationsHub : Hub
    {
        //private IHttpContextAccessor _httpContextAccessor;
        //private IHttpContextAccessor _context;

        //IHttpContextAccessor httpContextAccessor, IHttpContextAccessor context
        public NotificationsHub()
        {
            //_httpContextAccessor = httpContextAccessor;
            //_context = context;
        }

        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", "You have a new message: " + message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
