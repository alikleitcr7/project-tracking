using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Hubs;

namespace ProjectTracking.Controllers
{
    public class BroadcastsController : BaseSupervisorController
    {
        private readonly IBroadcastsMethods _broadcastsMethods;
        //private readonly IHubContext<BroadcastsHub> _notificationsHub;

        public BroadcastsController(IBroadcastsMethods notificationMethods, IUserMethods userMethods)
            : base(userMethods)
        {
            _broadcastsMethods = notificationMethods;
            //_notificationsHub = notificationsHub;
        }


        public ActionResult Index()
        {
            return View();
        }

        public object GetFromUser(string fromUserId, int page, int countPerPage)
        {
            if (fromUserId == "0")
            {
                fromUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            List<Broadcast> records = _broadcastsMethods.GetFromUser(fromUserId, page, countPerPage, out int totalCount);

            return new
            {
                records,
                totalCount
            };
        }

        public object GetToTeam(int toTeam, int page, int countPerPage)
        {
            List<Broadcast> records = _broadcastsMethods.GetToTeam(toTeam, page, countPerPage, out int totalCount);

            return new
            {
                records,
                totalCount
            };
        }

        public async Task<List<Broadcast>> Send([FromBody]SendBroadcastObject model)
        {
            List<Broadcast> sent = new List<Broadcast>();

            string fromUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            Broadcast notification = await _broadcastsMethods.Send(fromUser, model.toTeamId, model.message, model.notificationType, true);
            sent.Add(notification);

            return sent;
        }

        public class SendBroadcastObject
        {
            public string fromUserId { get; set; }
            public int toTeamId { get; set; }
            public string message { get; set; }
            public NotificationType notificationType { get; set; }
        }
    }
}