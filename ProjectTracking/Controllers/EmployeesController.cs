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
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public EmployeesController(IUserMethods users, ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager,
                                  RoleManager<ApplicationIdentityRole> roleManager,
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
            _roleManager = roleManager;
            _context = context;
        }
        [Authorize(Policy = "Administration")]

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


        public List<string> GetSupervisorsIdsIncludingParents([FromQuery]string forUserId,int levels)
        {
            return _users.GetSupervisorsIdsIncludingParents(forUserId,levels);
        }

        public List<Project> GetProjectsByDepartment(int departmentId, int companyId)
        {
            return _projects.Get(departmentId, companyId, true);
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

        public List<Team> GetDepartments()
        {
            return _departments.GetAll();
        }
        #region SupervisingsOperaitons


        public bool AddSupervising([FromBody]  AssignSuperVisorModel assignModel)
        {
            return _users.AddSupervising(assignModel.UserId, assignModel.SuperViseIds);
        }
        public bool RemoveSuperVised([FromBody]  AssignSuperVisorModel assignModel)
        {
            return _users.RemoveSuperVised(assignModel.UserId, assignModel.SuperViseIds);
        }

        public JsonResult GetSuperVising(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }
            return Json(_users.GetSupervising(userId));
        }
        #endregion

        #region SupervisorsOperaitons
        public bool AddSupervisors([FromBody]  AssignSuperVisorModel assignModel)
        {
            return _users.AddSupervisors(assignModel.UserId, assignModel.SuperViseIds);
        }
        public bool RemoveSuperVisors([FromBody]  AssignSuperVisorModel assignModel)
        {
            return _users.RemoveSuperVisors(assignModel.UserId, assignModel.SuperViseIds);
        }

        public JsonResult GetSuperVisors(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException();
            }
            return Json(_users.GetSupervisors(userId));
        }
        #endregion

        #region AssigningRolesOperations
        public bool AddRolesToUser([FromBody] AssignSuperVisorModel assign)
        {
            var employee = _context.Users.FirstOrDefault(c => c.Id == assign.UserId);
            if (employee == null)
                return false;
            foreach (string roleId in assign.SuperViseIds)
            {
                //var role = _context.Roles.FirstOrDefault(c => c.Id == roleId);
                //if (role == null)
                //    continue;
                //IdentityUserRole<string> userRole = new IdentityUserRole<string>
                //{
                //    RoleId = roleId,
                //    UserId = employee.Id
                //};
                //_context.UserRoles.Add(userRole);

            }
            _context.SaveChanges();
            return true;
        }
        public bool RemoveRolesToUser([FromBody] AssignSuperVisorModel assign)
        {
            throw new NotImplementedException();
        }
        public JsonResult GetRoles(string userId)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(userId))
            //    throw new ArgumentNullException();
            //ApplicationUser employee = _context.Users.FirstOrDefault(c => c.Id == userId);
            //if (employee == null)
            //    throw new NullReferenceException();
            //List<ApplicationIdentityRole> roles = _roleManager.Roles.ToList();
            ////string[] rolesTakenIds = _context.UserRoles.Where(c => c.UserId == userId).Select(c => c.RoleId).ToArray();
            //return Json(new { All = roles, rolesTakes = rolesTakenIds });

        }
        #endregion
        public List<User> GetEmployeesByDepartment(int departmentId)
        {

            return _users.GetTeamMembers(departmentId);
        }
        public TimeSheet GetLatest(string userId)
        {
            return _timeSheets.GetLatest(userId);

        }
      
    }
}