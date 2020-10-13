
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
using ProjectTracking.Exceptions;
using ProjectTracking.Models.TimeSheet;

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

        public IActionResult UserTimeSheets()
        {
            ViewData["ID"] = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
            return View();
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery]int id)
        {
            try
            {
                return Ok(_timeSheets.Delete(id));
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

        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_timeSheets.GetById(id));
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

        [HttpPost]
        public IActionResult Save([FromBody] TimeSheetSaveModel model)
        {
            try
            {
                return Ok(_timeSheets.Save(model));
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

        [HttpGet]
        public IActionResult GetTimeSheetTasks(int timeSheetId)
        {
            try
            {
                //var tasks = _timeSheets.GetTimeSheetTasks(timeSheetId);

                //Newtonsoft.Json.JsonConvert.DefaultSettings = () => new Newtonsoft.Json.JsonSerializerSettings
                //{
                //    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                //    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                //};

                //string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(tasks);

                return Ok(_timeSheets.GetTimeSheetTasks(timeSheetId));
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

        [HttpPost]
        public IActionResult AddProjectToTimeSheet([FromBody]ProjectAssignModel model)
        {
            try
            {
                return Ok(_timeSheets.AddTasks(model.timeSheetId, model.projectIds));
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

        [HttpPost]
        public IActionResult RemoveProjectFromTimeSheet([FromBody]ProjectAssignModel model)
        {
            try
            {
                return Ok(_timeSheets.RemoveTasks(model.timeSheetId, model.projectIds));
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

        [HttpGet]
        public IActionResult GetTimeSheetYears(string userId)
        {
            try
            {
                return Ok(_timeSheets.GetTimeSheetYears(userId));
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

        [HttpGet]
        public IActionResult GetTimeSheets(string userId, int? year, bool includeTasks)
        {
            try
            {
                var timesheets = _timeSheets.GetByUser(userId, year, includeTasks);

                string ser = Newtonsoft.Json.JsonConvert.SerializeObject(timesheets);

                return Ok(timesheets);
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

        [HttpGet]
        public IActionResult GetLatest(string userId)
        {
            try
            {
                return Ok(_timeSheets.GetLatest(userId));
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

        public IActionResult GetTimeSheetProjects(int timesheetId)
        {
            try
            {
                var timesheets = _timeSheets.GetTimeSheetTasks(timesheetId);

                string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(_timeSheets.GetTimeSheetTasks(timesheetId));

                return Ok(timesheets);
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
        public IActionResult TimeSheetTasksWithActivityCheck(int timesheetId)
        {
            try
            {
                return Ok(_timeSheets.TimeSheetTasksWithActivityCheck(timesheetId));
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