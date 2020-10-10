using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectTracking.Data;
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
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;

        private readonly IConfiguration _configuration;


        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger, IConfiguration configuration, RoleManager<ApplicationIdentityRole> roleManager,
            IEmailSender emailSender
            , ApplicationDbContext context, IEnumerable<ApplicationIdentityRole> roles
            )
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public List<ApplicationIdentityRole> Roles
        {
            get
            {
                return _roleManager.Roles.ToList();
            }
        }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Date Of Birth")]
            public string DateOfBirth { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            //[Required]
            //public int CompanyID { get; set; }
            //[Required]
            //public int DepartmentID { get; set; }
            [Display(Name = "Secret Key")]
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
            var secretToken = _configuration.GetSection($"Tokens:ApplicationUser").Value;

            if (secretToken != Input.RoleKey)
            {
                ViewData["ErrorMessage"] = "your are not authorized to register";

                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    NormalizedUserName = Input.Email.ToUpper(),
                    Email = Input.Email,
                    NormalizedEmail = Input.Email.ToUpper(),
                    EmailConfirmed = false,
                    //CompanyID = Input.CompanyID,
                    //DepartmentID = Input.DepartmentID,
                    DateOfBirth = DateTime.Parse(Input.DateOfBirth),
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

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
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("RegistrationError", error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
