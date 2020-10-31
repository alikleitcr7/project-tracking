using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
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
        //private readonly RoleManager<ApplicationIdentityRole> roleManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserMethods _users;
        private readonly IIpAddressMethods _ipAddressMethods;
        private readonly IUserLogsMethods _userLogsMethods;

        //private readonly IProjectsMethods _projects;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            //RoleManager<ApplicationIdentityRole> roleManager,
            IUserMethods users,
            IIpAddressMethods ipAddressMethods,
            IUserLogsMethods userLogsMethods,
            IHttpContextAccessor accessor
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //this.roleManager = roleManager;
            _accessor = accessor;
            _users = users;
            this._ipAddressMethods = ipAddressMethods;
            _userLogsMethods = userLogsMethods;
            //_projects = projects;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            return View();
        }

        [Route("/login")]
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
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //List<ApplicationIdentityRole> roles = _users.GetAllRoles();

            //ViewData["Roles"] = roles;

            if (ModelState.IsValid)

            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                // either add a claim of supervisor
                // or a policy

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.UserName);

                    if (user == null)
                    {
                        user = await _userManager.FindByNameAsync(model.UserName);
                    }

                    var id = user?.Id;
                    var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                    ApplicationContext.LogsLastUpdatedDate = DateTime.Now;

                    UserLog log = _userLogsMethods.GetActiveUserLog(id);

                    if (log == null || log.ToDate.HasValue)
                    {
                        log = _userLogsMethods.AddStartLog(id, ip, UserLogStatus.Login);
                    }

                    if (ApplicationContext.ActiveLogs == null || !ApplicationContext.LogsLastUpdatedDate.HasValue)
                    {
                        ApplicationContext.ActiveLogs = _users.GetActiveLogs() ?? new List<UserLog>();
                        ApplicationContext.LogsLastUpdatedDate = DateTime.Now;
                    }

                    if (log != null && !ApplicationContext.ActiveLogs.Any(k => k.UserId == id))
                    {
                        ApplicationContext.ActiveLogs.Add(log);
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

            //ViewData["ErrorMessage"] = "Enter Above Required Fields";

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

            _users.EndActiveLog(id, UserLogStatus.Logout);

            return Redirect("/login");
        }

        [HttpGet]
        public IActionResult GetOverview()
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
                        overview = _users.GetSupervisorOverview(userId);
                        break;
                    case ApplicationUserRole.Admin:
                        overview = _users.GetAdminOverview(userId);
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

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
