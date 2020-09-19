﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Data.Methods.Interfaces.Statistics;
using ProjectTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracking.Controllers
{
    //[Authorize(Policy = "Administration")]

    public class ProjectsController : Controller
    {
        private readonly ITeamsMethods _teams;
        private readonly ICategoriesMethods _categoriesMethods;
        private readonly IProjectFilesMethods _file;
        private readonly IProjectsStatistics _projectsStatistics;
        private readonly IUserMethods _users;



        private readonly IProjectsMethods _projects;
        public ProjectsController(ITeamsMethods teams, 
            ICategoriesMethods categoriesMethods,
            IUserMethods users, IProjectsMethods projects, IProjectsStatistics projectsStatistics,
             IProjectFilesMethods file)
        {
            _teams = teams;
            _categoriesMethods = categoriesMethods;
            _projects = projects;
            _file = file;
            _users = users;
            _projectsStatistics = projectsStatistics;
        }
        [Authorize(Policy = "Administration")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Files(int fileId, int? year, int? month)
        {
            ViewData["FileId"] = fileId;

            ProjectFile file = _file.GetFileWithActivities(fileId);

            ViewData["File"] = file;

            ViewData["Year"] = year;
            ViewData["Month"] = month;

            //ViewData["Years"] = file;

            List<int> years = new List<int>();
            List<int> months = new List<int>();

            years = file.TimeSheetActivities.Select(k => k.FromDate.Year).Distinct().ToList();

            ViewData["Years"] = years;

            if (year.HasValue && year.Value != 0)
            {
                file.TimeSheetActivities = file.TimeSheetActivities.Where(k => k.FromDate.Year == year).ToList();

                months = file.TimeSheetActivities
                    .Where(k => k.FromDate.Year == year.Value)
                    .Select(k => k.FromDate.Month).Distinct().ToList();

            }


            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }

            ViewData["Months"] = months;



            List<KeyValuePair<string, string>> insights = new List<KeyValuePair<string, string>>();

            List<KeyValuePair<string, string>> measurmentUnitInsights = file.TimeSheetActivities
                .Where(k => k.MeasurementUnit != null)
                .GroupBy(g => new { g.MeasurementUnit.ID })
                .Select(g => new KeyValuePair<string, string>(g.First().MeasurementUnit.Name, g.Where(k => k.Number.HasValue).Sum(k => k.Number).Value.ToString()))
                .ToList();

            insights.AddRange(measurmentUnitInsights);
            insights.Add(new KeyValuePair<string, string>("Total Activities", file.TimeSheetActivities.Count.ToString()));

            TimeSheetActivity latestActivity = file.TimeSheetActivities.OrderByDescending(k => k.FromDate).FirstOrDefault();

            string latestActivityDisplay = latestActivity == null ? "-" :
                $"<p>Type: {(latestActivity.TypeOfWork == null ? "-" : latestActivity.TypeOfWork.Name)}</p>" +
                $"<p>M.Unit: {(latestActivity.MeasurementUnit == null ? "-" : latestActivity.MeasurementUnit.Name)}</p>" +
                $"<p>Number: {latestActivity.Number}</p>";
            string timeSheetUrl = latestActivity == null ? "Latest Activity" : $"<a target=\"_blank\" class=\"Button-Shared\" href=\"/timesheets/TimeSheetActivityLog?activityId={latestActivity.ID}\">Latest Activity</a>";
            ;
            insights.Add(new KeyValuePair<string, string>(timeSheetUrl, latestActivityDisplay));

            ViewData["Insights"] = insights;


            return View();
        }


        //public DateFilterModel GetProjectWithActivitiesFilterOptions(int projectId, int? year, int? month)
        //{
        //    return _projects.GetProjectWithActivities(projectId, year, month);
        //}


        public IActionResult ProjectTrack(int projectId, int? year, int? month, int? day)
        {
            ViewData["ProjectId"] = projectId;

            Project project = _projects.GetProjectWithActivities(projectId);

            ViewData["Project"] = project;

            List<TimeSheetActivity> activities = new List<TimeSheetActivity>();

            foreach (Project projectActivity in project.Activities)
            {
                foreach (TimeSheetProject timeSheetProject in projectActivity.TimeSheetProjects)
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

            List<KeyValuePair<string, string>> measurmentUnitInsights = activities
                .Where(k => k.MeasurementUnit != null)
                .GroupBy(g => new { g.MeasurementUnit.ID })
                .Select(g => new KeyValuePair<string, string>(g.First().MeasurementUnit.Name, g.Where(k => k.Number.HasValue).Sum(k => k.Number).Value.ToString()))
                .ToList();

            insights.AddRange(measurmentUnitInsights);
            insights.Add(new KeyValuePair<string, string>("Total Activities", activities.Count.ToString()));


            long totalTicks = activities.Where(k=>k.ToDate.HasValue).Select(k => (k.ToDate.Value - k.FromDate).Ticks).ToList().Sum();
            TimeSpan timeSpan = new TimeSpan(totalTicks);

            string totalTimeDisplay = "";

            totalTimeDisplay += $"<p>days: {timeSpan.Days}</p> ";
            totalTimeDisplay += $"<p>hours: {timeSpan.Hours}h </p>";
            totalTimeDisplay += $"<p>mins: {timeSpan.Minutes}m </p>";


            //totalTimeDisplay = totalSeconds.ToString("0.##");

            insights.Add(new KeyValuePair<string, string>("Total Time", totalTimeDisplay));

            TimeSheetActivity latestActivity = activities.OrderByDescending(k => k.FromDate).FirstOrDefault();

            string latestActivityDisplay = latestActivity == null ? "-" :
                $"<p>Type: {(latestActivity.TypeOfWork == null ? "-" : latestActivity.TypeOfWork.Name)}</p>" +
                $"<p>M.Unit: {(latestActivity.MeasurementUnit == null ? "-" : latestActivity.MeasurementUnit.Name)}</p>" +
                $"<p>Number: {latestActivity.Number}</p>";
            string timeSheetUrl = latestActivity == null ? "Latest Activity" : $"<a target=\"_blank\" class=\"Button-Shared\" href=\"/timesheets/TimeSheetActivityLog?activityId={latestActivity.ID}\">Latest Activity</a>";

            insights.Add(new KeyValuePair<string, string>(timeSheetUrl, latestActivityDisplay));

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


        [HttpDelete]
        public bool Delete(int id)
        {
            return _projects.Delete(id);
        }

        [HttpPut]
        public bool Update([FromBody] UpdateProjectModel model)
        {
            return _projects.Update(model.id, model.title, model.description, model.categoryId, model.teamId);
        }


        #endregion

    }
}