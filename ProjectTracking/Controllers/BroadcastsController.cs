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
    public class BroadcastsController : BaseController
    {
        private readonly IBroadcastsMethods _broadcastsMethods;

        public BroadcastsController(IBroadcastsMethods broadcastsMethods)
        {
            _broadcastsMethods = broadcastsMethods;
        }


        public ActionResult Index()
        {
            return View();
        }

        public IActionResult GetFromCurrentUser(int page, int countPerPage)
        {
            try
            {
                string fromUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                List<Broadcast> records = _broadcastsMethods.GetFromUser(fromUserId, page, countPerPage, out int totalCount);

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

        public IActionResult GetToTeam(int toTeamId, int page, int countPerPage)
        {
            try
            {
                List<Broadcast> records = _broadcastsMethods.GetToTeam(toTeamId, page, countPerPage, out int totalCount);

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

        //public IActionResult GetToCurrentUser(int page, int countPerPage)
        //{
        //    try
        //    {
        //        string userId = GetCurrentUserId();
        //        List<Broadcast> records = _broadcastsMethods.GetToUser(userId, page, countPerPage, out int totalCount);
        //        return Ok(new
        //        {
        //            records,
        //            totalCount
        //        });
        //    }
        //    catch (ClientException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

            // try moving to notifications controller...
            // and also the send to sendbroadcast method
            // for furthur notice

        public async Task<IActionResult> Send([FromBody]SendBroadcastObject model)
        {
            try
            {
                List<Broadcast> sent = new List<Broadcast>();

                string fromUser = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                Broadcast notification = await _broadcastsMethods.Send(fromUser, model.toTeamId, model.message, model.type, true);

                sent.Add(notification);

                //await _notificationsHub.Clients.User("a18be9c0-aa65-4af8-bd17-00bd9344e579").SendAsync("ReceiveBroadcast", notification);
                //await _notificationsHub.Clients.All.SendAsync("ReceiveBroadcast", notification);
                //await _notificationsHub.Clients..All.SendAsync("ReceiveNotification", notification );

                return Ok(sent);
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



        public class SendBroadcastObject
        {
            //public string fromUserId { get; set; }
            public int toTeamId { get; set; }
            public string message { get; set; }
            public NotificationType type { get; set; }
        }
    }
}