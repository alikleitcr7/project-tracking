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

    [Authorize]
    [Route("[controller]/[action]")]
    public class ProfileController : BaseController
    {
        private readonly IUserMethods _userMethods;

        public ProfileController(IUserMethods users)
        {
            _userMethods = users;
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
            ViewData["SecureMode"] = isDifferentUser && !model.IsSupervisingUser && currentUser.Role != ApplicationUserRole.Admin;


            return View(model);
        }
    }
}