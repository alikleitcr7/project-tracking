using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
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
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUserMethods _users;
        //private readonly IProjectsMethods _projects;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IUserMethods users,
            IHttpContextAccessor accessor
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accessor = accessor;
            _users = users;
            //_projects = projects;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login");
            }

            return Redirect("/dashboard");
        }

        [Route("/login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/logout");
            }

            List<IdentityRole<string>> roles = _users.GetAllRoles();

            ViewData["Roles"] = roles;

            return View();
        }

        [Route("/login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            List<IdentityRole<string>> roles = _users.GetAllRoles();

            ViewData["Roles"] = roles;

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

                    var id = user?.Id;
                    var ip = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                    ApplicationContext.LogsLastUpdatedDate = DateTime.Now;

                    UserLog log = _users.AddStartLog(id, ip);

                    if (!ApplicationContext.ActiveLogs.Any(k => k.UserId == id))
                    {
                        ApplicationContext.ActiveLogs.Add(log);
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

            _users.EndLog(id, "Logout");

            return Redirect("/login");
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
