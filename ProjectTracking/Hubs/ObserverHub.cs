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

        public ObserverHub(IHttpContextAccessor httpContextAccessor, IUserMethods userMethods, IUserLogsMethods userLogsMethods, IHttpContextAccessor context)
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

            if (Context.User.Identity.IsAuthenticated)
            {
                string id = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string role = Context.User.FindFirst(ClaimTypes.Role).Value;

                ObservedUser user = Users.FirstOrDefault(k => k.UserId == id);

                if (user == null)
                {
                    user = new ObservedUser()
                    {
                        UserId = id,
                    };

                    // add connection id
                    user.AddConnection(Context.ConnectionId);

                    // append to obs. users
                    Users.Add(user);
                }
                else
                {
                    // append connections
                    user.AddConnection(Context.ConnectionId);
                }

                // add log if not exist as login
                if (!ApplicationContext.ActiveLogs.Any(k => k.UserId == id))
                {
                    var ip = _context.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                    var log = _userLogsMethods.AddStartLog(id, ip, DataContract.UserLogStatus.Login);

                    ApplicationContext.ActiveLogs.Add(log);

                    await Clients.All.SendAsync("RefreshLogs");
                }

                //var cId = Context.User.FindFirst(ClaimTypes.NameIdentifier);
                //string id = cId.Value;

                //ApplicationContext.Methods.Users.AddStartLog(id, "");
            }

            await base.OnConnectedAsync();
        }

        public async Task CheckIfUserDisconnected(string userId, string role)
        {
            var user = Users.FirstOrDefault(k => k.UserId == userId);

            bool disconnected = true;

            if (user != null)
            {
                // user has no connections
                disconnected = !user.IsActive;

                if (disconnected)
                {
                    // remove from obs users
                    Users.Remove(user);

                    // end log
                    var activeLog = ApplicationContext.ActiveLogs.FirstOrDefault(k => k.UserId == userId);

                    if (activeLog != null)
                    {
                        _userLogsMethods.EndActiveLog(userId, DataContract.UserLogStatus.Disconnected);

                        ApplicationContext.ActiveLogs.Remove(activeLog);

                        await Clients.All.SendAsync("RefreshLogs");
                    }
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ActiveConnections.Remove(Context.ConnectionId);

            await Clients.All.SendAsync("ReceiveMessage", "User Disconnected " + Context.ConnectionId);

            if (Context.User.Identity.IsAuthenticated)
            {
                string id = Context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string role = Context.User.FindFirst(ClaimTypes.Role).Value;

                ObservedUser user = Users.FirstOrDefault(x => x.HasConnection(Context.ConnectionId));

                if (user != null)
                {
                    // remove connectionid
                    user.RemoveConnection(Context.ConnectionId);


                    // no connections left 
                    // this maybe a refresh of one page 
                    // or closed the app
                    // we will wait n seconds to see if the user still disconnected
                    // then we will mark as disconnected
                    if (!user.IsActive)
                    {
                        await Task.Delay(5000).ContinueWith(k => CheckIfUserDisconnected(id, role));
                    }
                    //// if not active > disconnected
                    //if (!user.IsActive)
                    //{

                    //}
                }

                //if (user != null)
                //{
                //    Users.Remove(user);
                //}

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
