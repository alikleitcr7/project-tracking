using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectTracking.Data;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ProjectTracking.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRoleKeyMethods roleKeyMethods;
        private readonly ApplicationDbContext _context;
        //private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private readonly IConfiguration _configuration;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //ILogger<RegisterModel> logger,
            IConfiguration configuration,
            IEmailSender emailSender,
            IRoleKeyMethods roleKeyMethods,
            ApplicationDbContext context
            )
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            //_logger = logger;
            //_roleManager = roleManager;
            _emailSender = emailSender;
            this.roleKeyMethods = roleKeyMethods;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        //public List<ApplicationIdentityRole> Roles
        //{
        //    get
        //    {
        //        return _roleManager.Roles.ToList();
        //    }
        //}

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name"), MaxLength(30)]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name"), MaxLength(30)]
            public string LastName { get; set; }

            //[Required]
            //[Display(Name = "Title")]
            //public string Title { get; set; }

            [Required]
            [Display(Name = "Date Of Birth")]
            public string DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email"), MaxLength(255)]
            public string Email { get; set; }

            [Required]
            [Display(Name = "UserName"), RegularExpression(@"^(?=.{3,30}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$", ErrorMessage = "Username should be between 3 to 30 characters and can contain letters from a-z and numbers 0-9 and can contain (.-_) but not in a row and not in the beginning or end")]
            public string UserName { get; set; }

            [Required]
            //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password"), MaxLength(65)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Role Key"), MaxLength(20)]
            public string RoleKey { get; set; }
        }
        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
        public bool CheckPage()
        {
            return true;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string roleKey = Input.RoleKey;

            if (string.IsNullOrWhiteSpace(roleKey))
            {
                ViewData["ErrorMessage"] = "role key is required";

                return Page();
            }




            Dictionary<ApplicationUserRole, string> roleKeys = roleKeyMethods.GetRoleKeys();

            bool hasValue = roleKeyMethods.GetRoleKeys().ContainsValue(roleKey);

            ApplicationUserRole? role = hasValue ? roleKeys.First(k => k.Value == roleKey).Key : (ApplicationUserRole?)null;

            if (!role.HasValue)
            {
                ViewData["ErrorMessage"] = "your are not authorized to register";

                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Save today's date.
                var today = DateTime.Today;
                DateTime dateOfBirth = DateTime.Parse(Input.DateOfBirth);
                // Calculate the age.
                var age = today.Year - dateOfBirth.Year;

                if (age < 14)
                {
                    ViewData["ErrorMessage"] = "Age should be above 14 years old";
                    return Page();
                }

                string password = Input.Password;
                var validator = new PasswordValidator<ApplicationUser>();
                var result_password = await validator.ValidateAsync(_userManager, null, password);

                if (!result_password.Succeeded)
                {
                    ViewData["ErrorMessage"] = "Password should be at least 6 characters and contain at least one upper-case and one digit";
                    return Page();
                }

                if (_context.Users.Any(k => k.Email == Input.Email.ToLower()))
                {
                    ViewData["ErrorMessage"] = "Email already exist";
                    return Page();
                }

                if (_context.Users.Any(k => k.UserName == Input.UserName.ToLower()))
                {
                    ViewData["ErrorMessage"] = "Username already exist";
                    return Page();
                }

                var user = new ApplicationUser
                {
                    UserName = Input.UserName.ToLower(),
                    NormalizedUserName = Input.UserName.ToUpper(),
                    Email = Input.Email.ToLower(),
                    NormalizedEmail = Input.Email.ToUpper(),
                    EmailConfirmed = false,
                    //CompanyID = Input.CompanyID,
                    //DepartmentID = Input.DepartmentID,
                    DateOfBirth = dateOfBirth,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    RoleCode = (short)role.Value,
                    RoleAssignedDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Page(
                        "/account/confirmemail",
                        pageHandler: null,
                        values: new { userId = user.Id, code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <br /> <a href=\"{HtmlEncoder.Default.Encode(callbackUrl)}\">clicking here</a>.");

                    ViewData["Message"] = "Thank you for registering! a confirmation link was sent to your email";

                    //await _signInManager.SignInAsync(user, isPersistent: false);

                    //var role = _context.Roles.FirstOrDefault(c => c.Name == "User");

                    //if (role != null)
                    //{
                    //    await _userManager.AddToRoleAsync(user, role.Name);
                    //}

                    //Using LocalRedirect ensures that the "return URL" is a route actually on your site, 
                    //instead of some malicious third-party bad actor's.
                    return Page();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("RegistrationError", error.Description);
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "fill required fields";
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
