using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Hubs;
using ProjectTracking.Models;
using ProjectTracking.Models.Admin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IHubContext<ObserverHub> observerHub;
        private readonly IUserMethods _users;
        private readonly IUserLogsMethods _userLogsMethods;

        public HomeController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserMethods users,
            IUserLogsMethods userLogsMethods,
            IHttpContextAccessor accessor,
            IHubContext<ObserverHub> observerHub
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accessor = accessor;
            this.observerHub = observerHub;

            _users = users;
            _userLogsMethods = userLogsMethods;
        }


        //[Authorize] 
        // manually bcz it will add return url to login
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }



            return View();
        }

        [Route("/login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/logout");
            }

            //List<ApplicationIdentityRole> roles = _users.GetAllRoles();

            //ViewData["Roles"] = roles;

            return View();
        }


        [Route("/login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.UserName);

                    if (user == null)
                    {
                        user = await _userManager.FindByNameAsync(model.UserName);
                    }

                    if (Request.Headers["Referer"].Count != 0)
                    {
                        string uriQuery = new System.Uri(Request.Headers["Referer"].ToString()).Query;

                        if (!string.IsNullOrEmpty(uriQuery))
                        {
                            var queryStr = System.Web.HttpUtility.ParseQueryString(uriQuery);

                            if (queryStr["ReturnUrl"] != null)
                            {
                                return Redirect(queryStr["ReturnUrl"]);
                            }
                        }
                    }

                    return Redirect("~/");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Login Faild, Invalid Credentials";

                    return View();
                }
            }

            return View();
        }

        // GET api/role
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<string>> GetRoles()
        {
            string userId = User.FindFirst(ClaimTypes.Name).Value;
            var user = await _userManager.FindByEmailAsync(userId);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userId);
            }
            var role = await _userManager.GetRolesAsync(user);
            return role;
        }

        //[HttpPost]
        [Route("/logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            var id = user?.Id;

            await _signInManager.SignOutAsync();

            // delete from active users
            try
            {
                UserLog log = ApplicationContext.ActiveLogs.FirstOrDefault(k => k.UserId == id);

                if (log != null)
                {
                    ApplicationContext.ActiveLogs.Remove(log);
                }
            }
            catch (Exception ex)
            {
            }

            // end db log session 

            _userLogsMethods.EndActiveLog(id, UserLogStatus.Logout);

            await observerHub.Clients.All.SendAsync("RefreshLogs");

            return Redirect("/login");
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetOverview(bool logsAndCountersOnly)
        {
            try
            {
                string userId = GetCurrentUserId();
                ApplicationUserRole role = GetCurrentUserRole();

                object overview = null;

                switch (role)
                {
                    case ApplicationUserRole.TeamMember:
                        overview = _users.GetTeamMemberOverview(userId);
                        break;
                    case ApplicationUserRole.Supervisor:
                        overview = _users.GetSupervisorOverview(userId, logsAndCountersOnly);
                        break;
                    case ApplicationUserRole.Admin:
                        overview = _users.GetAdminOverview(userId, logsAndCountersOnly);
                        break;
                }

                return Ok(overview);
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
