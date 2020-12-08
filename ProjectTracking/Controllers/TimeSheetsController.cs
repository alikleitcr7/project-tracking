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
    [Authorize]
    [Route("[controller]/[action]")]
    public class TimeSheetsController : BaseController
    {

        private readonly ITimeSheetsMethods _timeSheets;
        private readonly IUserMethods _users;

        public TimeSheetsController(ITimeSheetsMethods timeSheets, IUserMethods users)
        {
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

        [Route("/timesheets/explore")]
        public IActionResult UserTimeSheets(string uid, int? tid)
        {
            // uid: userId
            // tid: timesheetId

            string currentUserId = GetCurrentUserId();
            string userId = uid ?? currentUserId;

            bool isSameUser = currentUserId == userId;

            TimeSheetExploreModel model = new TimeSheetExploreModel()
            {
                UserId = userId,
                CurrentUserId = currentUserId,
                IsSameUser = isSameUser,
                CurrentUserIsSupervisor = isSameUser ? false : _users.IsSupervisorOf(currentUserId, userId),
                CurrentUserRole = GetCurrentUserRole(),
                TimeSheetId = tid
            };

            return View(model);
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
        public async Task<IActionResult> Save([FromBody] TimeSheetSaveModel model)
        {
            try
            {
                model.SetAddedByUser(GetCurrentUserId());

                return Ok(await _timeSheets.Save(model));
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
        [Authorize(AuthPolicies.Supervisors)]
        public async Task<IActionResult> AddProjectToTimeSheet([FromBody]ProjectAssignModel model)
        {
            try
            {
                string byUserId = GetCurrentUserId();

                return Ok(await _timeSheets.AddTasks(byUserId, model.timeSheetId, model.projectIds));
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
        [Authorize(AuthPolicies.Supervisors)]
        public async Task<IActionResult> RemoveProjectFromTimeSheet([FromBody]ProjectAssignModel model)
        {
            try
            {
                string byUserId = GetCurrentUserId();

                return Ok(await _timeSheets.RemoveTasks(byUserId, model.timeSheetId, model.projectIds));
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

        [HttpGet]
        public IActionResult GetTimeSheetProjectsWithTasks(int timeSheetId)
        {
            try
            {
                List<Project> projects = _timeSheets.GetTimeSheetProjectsWithTasks(timeSheetId);

                return Ok(projects);
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

        public IActionResult GetTimeSheetProjectModel(int timeSheetId)
        {
            try
            {

                var timeSheet = _timeSheets.Get(timeSheetId, out List<Project> project, false);

                if (timeSheet == null)
                {
                    return null;
                }

                var model = Models.TimeSheet.TimeSheetProjectModel.GenerateModel(timeSheet, project);

                return Ok(model);
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

        public IActionResult GetActivities(int timesheetId)
        {
            try
            {
                return Ok(_timeSheets.GetTimeSheetActivities(timesheetId));
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

        public IActionResult GetActivitiesByDate(int timeSheetId, int? taskId, DateTime? date, bool includeDeleted)
        {
            try
            {
                return Ok(_timeSheets.GetTimeSheetActivities(timeSheetId, taskId, date, includeDeleted));
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

        public IActionResult CurrentUserTimeSheets()
        {
            try
            {
                string currentUserId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(_timeSheets.GetByUser(currentUserId));
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

        public IActionResult GetUserTimeSheets(string userId)
        {
            try
            {
                return Ok(_timeSheets.GetByUser(userId));
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
    }
}