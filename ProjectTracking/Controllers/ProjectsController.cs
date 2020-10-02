using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Data.Methods.Interfaces.Statistics;
using ProjectTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTracking.Models.Projects;
using ProjectTracking.Exceptions;
using System.Security.Claims;

namespace ProjectTracking.Controllers
{
    //[Authorize(Policy = "Administration")]
    public class ProjectsController : Controller
    {
        private readonly ITeamsMethods _teams;
        private readonly ICategoriesMethods _categoriesMethods;
        //private readonly IProjectFilesMethods _file;
        private readonly IProjectsStatistics _projectsStatistics;
        private readonly IUserMethods _users;



        private readonly IProjectsMethods _projects;
        public ProjectsController(ITeamsMethods teams,
            ICategoriesMethods categoriesMethods,
            IUserMethods users, IProjectsMethods projects, IProjectsStatistics projectsStatistics)
        {
            _teams = teams;
            _categoriesMethods = categoriesMethods;
            _projects = projects;
            _users = users;
            _projectsStatistics = projectsStatistics;
        }

        //[Authorize(Policy = "Administration")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProjectTrack(int projectId, int? year, int? month, int? day)
        {
            ViewData["ProjectId"] = projectId;

            Project project = _projects.GetProjectWithActivities(projectId);

            ViewData["Project"] = project;

            List<TimeSheetActivity> activities = new List<TimeSheetActivity>();

            foreach (ProjectTask projectActivity in project.Tasks)
            {
                foreach (TimeSheetTask timeSheetProject in projectActivity.TimeSheetTasks)
                {
                    activities.AddRange(timeSheetProject.Activities.ToList());
                }
            }

            if (year.HasValue && year.Value != 0)
            {
                activities = activities.Where(k => k.FromDate.Year == year).ToList();

                if (month.HasValue && month.Value != 0)
                {
                    activities = activities.Where(k => k.FromDate.Month == month).ToList();

                    if (day.HasValue && day.Value != 0)
                    {
                        activities = activities.Where(k => k.FromDate.Day == day).ToList();
                    }
                }
            }


            ViewData["Activities"] = activities.OrderByDescending(k => k.FromDate).ToList();

            List<KeyValuePair<string, string>> insights = new List<KeyValuePair<string, string>>();

            //List<KeyValuePair<string, string>> measurmentUnitInsights = activities
            //    .GroupBy(g => new { g.MeasurementUnit.ID })
            //    .Select(g => new KeyValuePair<string, string>(g.First().MeasurementUnit.Name, g.Where(k => k.Number.HasValue).Sum(k => k.Number).Value.ToString()))
            //    .ToList();

            //insights.AddRange(measurmentUnitInsights);
            insights.Add(new KeyValuePair<string, string>("Total Activities", activities.Count.ToString()));


            long totalTicks = activities.Where(k => k.ToDate.HasValue).Select(k => (k.ToDate.Value - k.FromDate).Ticks).ToList().Sum();
            TimeSpan timeSpan = new TimeSpan(totalTicks);

            string totalTimeDisplay = "";

            totalTimeDisplay += $"<p>days: {timeSpan.Days}</p> ";
            totalTimeDisplay += $"<p>hours: {timeSpan.Hours}h </p>";
            totalTimeDisplay += $"<p>mins: {timeSpan.Minutes}m </p>";


            //totalTimeDisplay = totalSeconds.ToString("0.##");

            insights.Add(new KeyValuePair<string, string>("Total Time", totalTimeDisplay));

            TimeSheetActivity latestActivity = activities.OrderByDescending(k => k.FromDate).FirstOrDefault();

            //string latestActivityDisplay = latestActivity == null ? "-" :
            //    $"<p>Type: {(latestActivity.TypeOfWork == null ? "-" : latestActivity.TypeOfWork.Name)}</p>" +
            //    $"<p>M.Unit: {(latestActivity.MeasurementUnit == null ? "-" : latestActivity.MeasurementUnit.Name)}</p>" +
            //    $"<p>Number: {latestActivity.Number}</p>";
            string timeSheetUrl = latestActivity == null ? "Latest Activity" : $"<a target=\"_blank\" class=\"Button-Shared\" href=\"/timesheets/TimeSheetActivityLog?activityId={latestActivity.ID}\">Latest Activity</a>";

            //insights.Add(new KeyValuePair<string, string>(timeSheetUrl, latestActivityDisplay));

            ViewData["Insights"] = insights;

            return View();
        }

        public JsonResult GetCategories()
        {
            return Json(_categoriesMethods.GetAll());
        }


        [Authorize(Policy = "Administration")]
        public IActionResult ProjectsProgress()
        {
            return View();
        }

        [HttpGet]
        [Route("Projects/ProjectsProgress/{UserId}")]
        public IActionResult ProjectsProgress(string userId)
        {
            ViewData["UserId"] = userId;
            DataContract.User user = _users.GetEmployee(userId);

            return View(user);
        }

        public JsonResult GetProjectsProgress(bool byYear, bool byYearAndMonth, string userId = null)
        {
            return Json(_projectsStatistics.GetProjectsProgress(byYear, byYearAndMonth, userId));
        }

        public JsonResult GetMeasurementUnitsTotalProgress(string userId, string year, string month, string day)
        {
            return Json(_projectsStatistics.GetMeasurementUnitsTotalProgress(userId, year, month, day));
        }

        public JsonResult GetProjectProgressYears(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
                return Json(_projectsStatistics.GetProjectProgressYearsByUser(userId));
            return Json(_projectsStatistics.GetProjectProgressYears());

        }

        public JsonResult GetProjectProgressMonthsByYear(int year, string userId)
        {
            if (!string.IsNullOrEmpty(userId))
                return Json(_projectsStatistics.GetProjectProgressMonthsByYearAndUser(userId, year));
            return Json(_projectsStatistics.GetProjectProgressMonthsByYear(year));
        }

        public JsonResult GetProjectProgressDayByMonthAndYear(int year, int month, string userId)
        {
            if (!string.IsNullOrEmpty(userId))
                return Json(_projectsStatistics.GetProjectProgressDaysByMonthAndYearAndUser(userId, year, month));
            return Json(_projectsStatistics.GetProjectProgressDaysByMonthAndYear(year, month));

        }

        #region Methods

        [HttpGet]
        public List<Team> GetTeams()
        {
            return _teams.GetAll();
        }
        //public List<Category> GetCategories()
        //{
        //    return _companies.GetAll();
        //}

        [HttpPost]
        public JsonResult Add([FromBody] AddProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Select(k => k.Errors).ToList();
                return Json(new { success = false, error = errors });
            }

            return Json(new { Added = _projects.Add(model.title, model.description, model.teamId, model.categoryId, model.parentId), success = true });
        }

        [HttpGet]
        public JsonResult Get(int teamId, int categoryId, int page, int countPerPage)
        {

            var records = _projects.Get(teamId, categoryId, page, countPerPage, out int totalCount);

            object oRetval = new
            {
                records,
                totalCount
            };

            return Json(oRetval);
        }

        [HttpPost]
        public IActionResult Save([FromBody]ProjectSaveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                return Ok(_projects.Save(model, userId));
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
        public JsonResult GetById(int id)
        {
            return Json(_projects.GetById(id));
        }

        [HttpGet]
        public IActionResult GetProjectStatuses()
        {
            return Ok(Enum.GetNames(typeof(ProjectStatus)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
        }
        
        [HttpGet]
        public IActionResult Search(int? categoryId, string keyword, int page, int countPerPage)
        {
            try
            {
                var record = _projects.Search(categoryId, keyword, page, countPerPage, out int totalCount);

                return Ok(new
                {
                    record,
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


        [HttpGet]
        public IActionResult GetStatusModifications(int projectId)
        {
            try
            {
                var record = _projects.GetStatusModifications(projectId);

                return Ok(record);
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
        public IActionResult GetByTeam(int teamId)
        {
            try
            {
                var record = _projects.GetByTeam(teamId);

                return Ok(record);
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_projects.Delete(id));
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

        #endregion

    }
}