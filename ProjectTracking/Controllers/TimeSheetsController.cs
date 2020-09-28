
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models;
using ProjectTracking.Models.Statistics;
using ProjectTracking.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Controllers
{
    public class TimeSheetsController : Controller
    {

        private readonly ITimeSheetsMethods _timeSheets;
        private readonly IUserMethods _users;
        private readonly INotificationMethods _notificationMethods;
        //private readonly IHubContext<NotificationsHub> _notificationsHub;
        public TimeSheetsController(ITimeSheetsMethods timeSheets, IUserMethods users,
            INotificationMethods notificationMethods)
        {
            _notificationMethods = notificationMethods;
            //_notificationsHub = notificationsHub;
            _timeSheets = timeSheets;
            _users = users;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            ViewData["ID"] = id;

            TimeSheet timeSheet = _timeSheets.Get(id, out _, false);

            ViewData["TimeSheet"] = timeSheet;

            return View();
        }

        public IActionResult TimeSheetActivityLog(int activityId)
        {
            ViewData["ActivityId"] = activityId;

            return View();
        }

        //public IActionResult SubordinatesTimeSheets()
        //{
        //    ViewData["SuperVisorId"] = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
        //    return View();
        //}
        //public JsonResult GetSubordinatesTimeSheets(int page, int countPerPage)
        //{
        //    string supervisorId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value.ToString();
        //    //string supervisorId = "6a3b5438-fd4a-4c67-a5ff-82b99e876503";
        //    var records = _timeSheets.GetSubordinatesTimeSheets(supervisorId, page, countPerPage, out int totalPages);
        //    object oRetval = new
        //    {
        //        records,
        //        totalPages
        //    };

        //    return Json(oRetval);
        //}
        //[Authorize]

        public IActionResult UserTimeSheets()
        {
            ViewData["ID"] = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
            return View();
        }

        //public JsonResult PermitTimeSheetStatus([FromBody] PermitModel permission)
        //{
        //    if (permission == null)
        //    {
        //        throw new ArgumentNullException(nameof(permission));
        //    }

        //    return Json(_timeSheets.PermitTimeSheetStatus(permission));
        //}
    
        //[HttpPost]
        //public async Task<bool> SignTimeSheet(int timeSheetId)
        //{
        //    string currentUserId = "";

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    if (_timeSheets.SignTimeSheet(currentUserId, timeSheetId))
        //    {
        //        // SEND NOTIFICATION TO SUPERVISORS 

        //        List<string> supervisorsIds = _users.GetSupervisorsIds(currentUserId);

        //        User user = _users.GetEmployee(currentUserId);
        //        TimeSheet timesheet = _timeSheets.Get(timeSheetId, out _, false);

        //        foreach (string supervisorId in supervisorsIds)
        //        {
        //            var sent = await _notificationMethods.Send(currentUserId, supervisorId, $"Timesheet for {timesheet.FromDateDisplay}-{timesheet.ToDateDisplay} Signed", NotificationType.Information, true);

        //            //await _notificationsHub.Clients.User(supervisorId).SendAsync("ReceiveNotification", sent);
        //        }

        //        return true;
        //    }

        //    return false;
        //}


        public object Add([FromBody]AddTimeSheetModel modal)
        {
            TimeSheet timeSheet = _timeSheets.Add(modal.userId, modal.fromDate, modal.toDate, out string message);

            return new
            {
                message,
                timeSheet
            };
        }


        [HttpDelete]
        public bool Delete([FromQuery]int id)
        {
            //return true;
            return _timeSheets.Delete(id);
        }

        public TimeSheet GetLatest(string userId)
        {
            //string currentUserId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;

            return _timeSheets.GetLatest(userId);
        }
        public Models.TimeSheet.TimeSheetProjectModel GetTimeSheetProjectModel(int timeSheetId)
        {
            var timeSheet = _timeSheets.Get(timeSheetId, out List<Project> project, false);

            if (timeSheet == null)
            {
                return null;
            }

            var model = Models.TimeSheet.TimeSheetProjectModel.GenerateModel(timeSheet, project);

            return model;
        }

        public List<TimeSheetActivity> GetActivities(int timesheetId)
        {
            return _timeSheets.GetTimeSheetActivities(timesheetId);
        }

        public List<TimeSheetTask> GetTimeSheetProjects(int timesheetId)
        {
            return _timeSheets.GetTimeSheetTasks(timesheetId);
        }

        public List<TimeSheetActivity> GetActivitiesByDate(int timesheetId, DateTime date)
        {
            return _timeSheets.GetTimeSheetActivities(timesheetId, date);
        }

        public List<DataContract.TimeSheet> CurrentUserTimeSheets()
        {
            string currentUserId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
            return _timeSheets.GetByUser(currentUserId);
        }

        public List<DataContract.TimeSheet> GetUserTimeSheets(string userId, int timeSheetId)
        {
            return _timeSheets.GetByUser(userId);
        }


    }
}