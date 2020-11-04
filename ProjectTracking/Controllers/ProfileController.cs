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
using ProjectTracking.Exceptions;
using ProjectTracking.Models.TimeSheet;
using ProjectTracking.Models.Profile;

namespace ProjectTracking.Controllers
{

    [Route("[controller]/[action]")]
    public class ProfileController : BaseController
    {
        private readonly IUserMethods _userMethods;
        //private readonly ITeamsMethods _departments;
        private readonly ITimeSheetsMethods _timeSheets;
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ApplicationDbContext _context;
        //private readonly IProjectsMethods _projects;
        ////private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(IUserMethods users, ITimeSheetsMethods timeSheets)
        {
            _userMethods = users;
            _timeSheets = timeSheets;
        }

        [Route("/profile/{userId?}")]
        public IActionResult Index(string userId)
        {
            string currentUserId = GetCurrentUserId();

            userId = userId ?? currentUserId;

            DataContract.User user = _userMethods.GetById(userId);


            if (user == null)
            {
                return NotFound();
            }


            bool isDifferentUser = user.Id != currentUserId;

            DataContract.User currentUser = isDifferentUser ? _userMethods.GetById(currentUserId) : user;

            ProfileViewModel model = new ProfileViewModel()
            {
                User = user,
                HasSupervisorLog = _userMethods.HasSupervisorLog(userId),
                HasTimeSheets = _userMethods.HasTimeSheets(userId),
                CurrentUserId = currentUserId,
                IsSupervisingUser = isDifferentUser ? _userMethods.IsSupervisorOf(currentUserId, user.Id) : false
            };

            // user is not the current user 
            // only admins and supervising users can access other profiles 
            if (isDifferentUser && !model.IsSupervisingUser && currentUser.Role != ApplicationUserRole.Admin)
            {
                return Unauthorized();
            }

            return View(model);
        }

        //[Route("/supervisor/{userId}")]
        //public IActionResult SupervisorTeams(string userId)
        //{
        //    User user = _userMethods.GetById(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    Models.Teams.SupervisorTeamsViewModel supervisorTeamsViewModel = new Models.Teams.SupervisorTeamsViewModel()
        //    {
        //        IncludeTitle = true,
        //        SupervisorId = user.Id,
        //        SupervisorName = user.FullName
        //    };

        //    return View(supervisorTeamsViewModel);
        //}
    }
}