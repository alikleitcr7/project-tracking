using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Models;
using ProjectTracking.Data;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;

namespace ProjectTracking.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly IUserMethods _users;
        private readonly ITeamsMethods _departments;
        private readonly ITimeSheetsMethods _timeSheets;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IProjectsMethods _projects;
        //private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EmployeesController(IUserMethods users, ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager,
                                  //RoleManager<ApplicationIdentityRole> roleManager,
                                  ITeamsMethods departments,
                                  ITimeSheetsMethods timeSheets,
                                  SignInManager<ApplicationUser> signInManager,
                                  IProjectsMethods projects)
        {
            _users = users;
            _departments = departments;
            _timeSheets = timeSheets;
            _projects = projects;
            _signInManager = signInManager;
            _userManager = userManager;
            //_roleManager = roleManager;
            _context = context;
        }
        //[Authorize(Policy = "Administration")]

        public IActionResult Index()
        {
            //var employees = _users.GetEmployees();

            return View();
        }

        public IActionResult Profile(string id)
        {
            DataContract.User user = _users.GetEmployee(id);
            ViewData["UserId"] = id;
            //TimeSheet tsGet = _timeSheets.Get(1, out List<Project> projects);
            //var model = Models.TimeSheet.TimeSheetModel.GenerateModel(tsGet, projects);
            return View(user);
        }

        public JsonResult GetUser(string id)
        {
            DataContract.User user = _users.GetEmployee(id);

            return Json(user);
        }

        public string GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return null;
        }

        [HttpPost]
        public async Task<bool> ValidateCurrentUserPassword([FromQuery]string password)
        {
            //var passwordValidator = new PasswordValidator<ApplicationUser>();

            string email = User.FindFirst(ClaimTypes.Name).Value;

            var result = await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

            //var result = await passwordValidator.ValidateAsync(_userManager, await _userManager.GetUserAsync(User), password);

            return result.Succeeded;
        }

        public TimeSheet AddTimeSheet([FromBody] TimeSheet timeSheet)
        {
            return _timeSheets.Add(timeSheet.UserId, timeSheet.FromDate, timeSheet.ToDate);
        }

        public List<TimeSheetTask> GetTimeSheetProjects(int timeSheetId)
        {
            return _timeSheets.GetTimeSheetTasks(timeSheetId);
        }

        [HttpPost]
        public bool AddProjectToTimeSheet([FromBody]ProjectAssignModel model)
        {
            return _timeSheets.AddTasks(model.timeSheetId, model.projectIds);
        }

        [HttpPost]
        public bool RemoveProjectFromTimeSheet([FromBody]ProjectAssignModel model)
        {
            return _timeSheets.RemoveTasks(model.timeSheetId, model.projectIds);
        }

        public List<int> GetTimeSheetYears(string userId)
        {
            List<TimeSheet> timesheets = _timeSheets.GetByUser(userId);

            return timesheets.Select(k => k.FromDate.Year).Distinct().ToList();
        }

        public List<TimeSheet> GetTimeSheets(string userId, int? year)
        {
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }

            return _timeSheets.GetByUser(userId).Where(k => k.FromDate.Year == year.Value).ToList();
        }

        public TimeSheet GetLatest(string userId)
        {
            return _timeSheets.GetLatest(userId);

        }
      
    }
}