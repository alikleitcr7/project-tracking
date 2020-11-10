using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Hubs;

namespace ProjectTracking.Controllers
{
    [Authorize]
    public class NotificationsController : BaseController
    {
        private readonly INotificationMethods _notificationMethods;
        //private readonly IHubContext<NotificationsHub> _notificationsHub;

        //, IHubContext<NotificationsHub> notificationsHub
        public NotificationsController(INotificationMethods notificationMethods)
        {
            _notificationMethods = notificationMethods;
            //_notificationsHub = notificationsHub;
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public object GetFromUser(string fromUserId, int page, int countPerPage)
        {
            if (fromUserId == "0")
            {
                fromUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }


            List<UserNotification> records = _notificationMethods.GetFromUser(fromUserId, page, countPerPage, out int totalCount);

            return new
            {
                records,
                totalCount
            };
        }

        public object GetToUser(string toUserId, int page, int countPerPage)
        {
            if (toUserId == "0")
            {
                toUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            List<UserNotification> records = _notificationMethods.GetToUser(toUserId, page, countPerPage, out int totalCount);

            return new
            {
                records,
                totalCount
            };
        }

        public IActionResult GetToCurrentUser(int page, int countPerPage)
        {
            try
            {
                string userId = GetCurrentUserId();

                List<UserNotification> records = _notificationMethods.GetToUser(userId, page, countPerPage, out int totalCount);

                return Ok(new
                {
                    records,
                    totalCount
                });
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        public IActionResult GetFromCurrentUser(int page, int countPerPage)
        {
            try
            {
                string userId = GetCurrentUserId();

                List<UserNotification> records = _notificationMethods.GetFromUser(userId, page, countPerPage, out int totalCount);

                return Ok(new
                {
                    records,
                    totalCount
                });
            }
            catch (ClientException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        public class SendBroadCastObject
        {
            public List<string> selectedUserIds { get; set; }
            public string message { get; set; }
            public NotificationType type { get; set; }
        }

        public class SendNotificationObject
        {
            public string fromUserId { get; set; }
            public string toUserId { get; set; }
            public string message { get; set; }
            public NotificationType notificationType { get; set; }
        }

        public async Task<List<UserNotification>> SendBroadCast([FromBody]SendBroadCastObject model)
        {
            List<UserNotification> sent = new List<UserNotification>();

            string fromUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            foreach (string id in model.selectedUserIds)
            {
                UserNotification notification = await _notificationMethods.Send(fromUser, id, model.message, model.type, true);
                sent.Add(notification);
            }

            //foreach (Notification notification in sent)
            //{
            //    try
            //    {
            //        await _notificationsHub.Clients.User(notification.ToUserId).SendAsync("ReceiveNotification", notification);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}


            return sent;
        }

        public async Task<UserNotification> Send([FromBody]SendNotificationObject model)
        {
            UserNotification notification = await _notificationMethods.Send(model.fromUserId, model.toUserId, model.message, model.notificationType, true);

            //if (notification != null)
            //{
            //    try
            //    {
            //        await _notificationsHub.Clients.User(model.toUserId).SendAsync("ReceiveNotification", notification);
            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}

            return notification;
        }

        public async Task<Broadcast> SendToTeam([FromBody]SendBroadcastObject model)
        {
            string userId = GetCurrentUserId();

            Broadcast notification = await _notificationMethods.SendToTeam(userId, model.toTeamId, model.message, model.type, true);

            return notification;
        }


        public class SendBroadcastObject
        {
            //public string fromUserId { get; set; }
            public int toTeamId { get; set; }
            public string message { get; set; }
            public NotificationType type { get; set; }
        }

    }
}