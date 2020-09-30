using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.AppStart;
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
    public class ObserverHub : Hub
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserMethods _users;
        private readonly IUserLogsMethods _userLogsMethods;
        private IHttpContextAccessor _context;

        public ObserverHub(IHttpContextAccessor httpContextAccessor, IUserMethods userMethods,IUserLogsMethods userLogsMethods, IHttpContextAccessor context)
        {
            _httpContextAccessor = httpContextAccessor;
            _users = userMethods;
            _userLogsMethods = userLogsMethods;
            _context = context;
        }

        public static List<string> ActiveConnections = new List<string>();
        public static List<ObservedUser> Users = new List<ObservedUser>();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", "User Connected " + Context.ConnectionId);

            ActiveConnections.Add(Context.ConnectionId);

            //&& Users.Where(k => k.UserId == Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).Count() < 2

            if (Context.User.Identity.IsAuthenticated )
            {
                string id = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                ObservedUser user = new ObservedUser()
                {
                    UserId = id,
                    ConnectionId = Context.ConnectionId
                };

                Users.Add(user);

                if (!ApplicationContext.ActiveLogs.Any(k => k.UserId == id))
                {
                    var ip = _context.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                    ApplicationContext.ActiveLogs.Add(_userLogsMethods.AddStartLog(id, ip));
                }

                //var cId = Context.User.FindFirst(ClaimTypes.NameIdentifier);
                //string id = cId.Value;

                //ApplicationContext.Methods.Users.AddStartLog(id, "");
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ActiveConnections.Remove(Context.ConnectionId);

            await Clients.All.SendAsync("ReceiveMessage", "User Disconnected " + Context.ConnectionId);

            if (Context.User.Identity.IsAuthenticated)
            {
                ObservedUser user = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

                if (user != null)
                {
                    Users.Remove(user);
                }

                //if (Users.Any(x => x.ConnectionId == Context.ConnectionId))
                //{
                //    ObservedUser user = Users.First(x => x.ConnectionId == Context.ConnectionId);

                //    var cId = Context.User.FindFirst(ClaimTypes.NameIdentifier);
                //    string id = cId.Value;

                //    ApplicationContext.Methods.Users.EndLog(id, "");

                //}
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
