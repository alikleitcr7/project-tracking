using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data;
using ProjectTracking.Models.Admin;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Controllers
{
    //[Authorize(Policy = ("Administration"))]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        //private readonly ICompanies _comapnyDto;
        private readonly ITeamsMethods _departmentDto;
        private readonly IUserMethods _userMethods;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ITeamsMethods departmentDto, ApplicationDbContext context
            , IUserMethods userMethods
            )
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            _context = context;
            _departmentDto = departmentDto;
            _userMethods = userMethods;
        }


        [Route("/manage")]
        //[Authorize]
        public IActionResult Manage()
        {
            //UserManagerViewModel _userManagerViewModel = new UserManagerViewModel();
            //_userManagerViewModel._departmentDto = _departmentDto.GetAll();
            ////_userManagerViewModel._comapnyDto = _comapnyDto.GetAll();
            //_userManagerViewModel._userManager = _userManager.Users;
            //return View(_userManagerViewModel);
            return View();
        }

        [HttpGet]
        public IActionResult GetAllUsersKeyValues()
        {
            try
            {
                var record = _userMethods.GetAllUsersKeyValues();

                return Ok(record);
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
        
        [HttpGet]
        public IActionResult GetAllUsersExecludeTeamSupervisors(int teamId)
        {
            try
            {
                var record = _userMethods.GetAllUsersExecludeTeamSupervisors(teamId);

                return Ok(record);
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


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetUsers()
        //{
        //    return Json(_userManager.Users);
        //}

        //public IActionResult AddUser()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        //{
        //    if (!ModelState.IsValid) return View(addUserViewModel);
        //    var dbUser = _context.Users.FirstOrDefault(c => c.FirstName == addUserViewModel.FirstName
        //                                                 && c.LastName == addUserViewModel.LastName
        //                                                 && c.MiddleName == addUserViewModel.MiddleName);
        //    if (dbUser != null)
        //    {
        //        ModelState.AddModelError("", "User Already Exists");
        //        return View();
        //    }

        //    var user = new ApplicationUser()
        //    {
        //        UserName = addUserViewModel.Email,
        //        Email = addUserViewModel.Email,
        //        //CompanyID = addUserViewModel.CompanyID,
        //        FirstName = addUserViewModel.FirstName,
        //        LastName = addUserViewModel.LastName,
        //        //IsTracked = true,
        //        DateOfBirth = DateTime.Parse(addUserViewModel.DateOfBirth),
        //        MiddleName = addUserViewModel.MiddleName,
        //        //TeamId = addUserViewModel.TeamId,
        //        HourlyRate = addUserViewModel.HourlyRate,
        //        MonthlySalary = addUserViewModel.MonthlySalary,
        //        EmploymentTypeCode = addUserViewModel.AgreementType.HasValue ? (short?)addUserViewModel.AgreementType.Value : null,
        //        //HoursPerDay = addUserViewModel.HoursPerDay

        //    };

        //    IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

        //    if (result.Succeeded)
        //    {

        //        //var RoleName = _context.Roles.FirstOrDefault(c => c.Name == addUserViewModel.Role);
        //        //if (RoleName != null)
        //        //{
        //        //    await _userManager.AddToRoleAsync(user, RoleName.Name);
        //        //}
        //        ModelState.Clear();
        //        ViewData["success"] = "A new user has been added successfully";
        //        return View(new AddUserViewModel());

        //    }
        //    foreach (IdentityError error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //    }
        //    return View(addUserViewModel);
        //}
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //        return RedirectToAction("UserManagement", _userManager.Users);
        //    var claims = await _userManager.GetClaimsAsync(user);
        //    var vm = new EditUserViewModel()
        //    {
        //        Id = user.Id,
        //        Email = user.Email,
        //        //CompanyID = user.CompanyID,
        //        //DepartmentID = user.DepartmentID,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        //IsTracked = user.IsTracked,
        //        HourlyRate = user.HourlyRate,
        //        MonthlySalary = user.MonthlySalary,
        //        AgreementType = (EmploymentType?)user.EmploymentTypeCode,
        //        //HoursPerDay = user.HoursPerDay
        //        MiddleName = user.MiddleName
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        //{
        //    if (!ModelState.IsValid) return View(editUserViewModel);
        //    var dbUser = _context.Users.FirstOrDefault(c => c.FirstName == editUserViewModel.FirstName
        //                                                && c.LastName == editUserViewModel.LastName
        //                                                && c.MiddleName == editUserViewModel.MiddleName
        //                                                && c.Id != editUserViewModel.Id);
        //    if (dbUser != null)
        //    {
        //        ModelState.AddModelError("", "User not updated, something went wrong.");

        //        return View(editUserViewModel);
        //    }
        //    var user = await _userManager.FindByIdAsync(editUserViewModel.Id);

        //    if (user != null)
        //    {
        //        user.Email = editUserViewModel.Email;
        //        user.UserName = editUserViewModel.Email;
        //        //user.DepartmentID = editUserViewModel.DepartmentID;
        //        //user.CompanyID = editUserViewModel.CompanyID;
        //        user.DateOfBirth = DateTime.Parse(editUserViewModel.DateOfBirth);
        //        user.FirstName = editUserViewModel.FirstName;
        //        user.LastName = editUserViewModel.LastName;
        //        user.MiddleName = editUserViewModel.MiddleName;

        //        //user.IsTracked = editUserViewModel.IsTracked;
        //        user.HourlyRate = editUserViewModel.HourlyRate;
        //        user.MonthlySalary = editUserViewModel.MonthlySalary;
        //        user.EmploymentTypeCode = editUserViewModel.AgreementType.HasValue ? (short?)editUserViewModel.AgreementType.Value : null;
        //        //user.HoursPerDay = editUserViewModel.HoursPerDay;

        //        var result = await _userManager.UpdateAsync(user);

        //        if (result.Succeeded)
        //            return RedirectToAction("Index", "Employees");


        //        ModelState.AddModelError("", "User not updated, something went wrong.");

        //        return View(editUserViewModel);
        //    }

        //    return RedirectToAction("Index", "Employees");
        //}
        //public async Task<IActionResult> DeleteUser(string Id)
        //{
        //    var user = await _userManager.FindByIdAsync(Id);

        //    if (user != null)
        //    {
        //        IdentityResult result = await _userManager.DeleteAsync(user);

        //        if (result.Succeeded)
        //            return RedirectToAction("UserManagement");
        //        else
        //            ModelState.AddModelError("", "Something went wrong while deleting this user.");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "This user can't be found");
        //    }

        //    return View("UserManagement", _userManager.Users);
        //}


        //Roles management
        //public IActionResult RoleManagement()
        //{
        //    var roles = _roleManager.Roles;
        //    return View(roles);
        //}

        //public JsonResult GetRoles()
        //{
        //    var roles = _roleManager.Roles;
        //    return Json(roles);
        //}

        //public IActionResult AddNewRole() => View();

        //Claims
        //public async Task<IActionResult> ManageClaimsForUser(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);

        //    if (user == null)
        //        return RedirectToAction("UserManagement", _userManager.Users);

        //    var claimsManagementViewModel = new ClaimsManagementViewModel { UserId = user.Id, AllClaimsList = BethanysPieShopClaimTypes.ClaimsList };

        //    return View(claimsManagementViewModel);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ManageClaimsForUser(ClaimsManagementViewModel claimsManagementViewModel)
        //{
        //    var user = await _userManager.FindByIdAsync(claimsManagementViewModel.UserId);

        //    if (user == null)
        //        return RedirectToAction("UserManagement", _userManager.Users);

        //    IdentityUserClaim<string> claim =
        //        new IdentityUserClaim<string> { ClaimType = claimsManagementViewModel.ClaimId, ClaimValue = claimsManagementViewModel.ClaimId };

        //    user.Claims.Add(claim);
        //    var result = await _userManager.UpdateAsync(user);

        //    if (result.Succeeded)
        //        return RedirectToAction("UserManagement", _userManager.Users);

        //    ModelState.AddModelError("", "User not updated, something went wrong.");

        //    return View(claimsManagementViewModel);
        //}
    }
}