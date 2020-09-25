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
    [Authorize(Policy = ("Administration"))]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly ICompanies _comapnyDto;
        private readonly ITeamsMethods _departmentDto;
        private readonly IUserMethods _userMethods;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITeamsMethods departmentDto, ApplicationDbContext context
            , IUserMethods userMethods
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _departmentDto = departmentDto;
            _userMethods = userMethods;
        }


        public IActionResult UserManagement()
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

        public IActionResult GetUsers()
        {
            return Json(_userManager.Users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid) return View(addUserViewModel);
            var dbUser = _context.Users.FirstOrDefault(c => c.FirstName == addUserViewModel.FirstName
                                                         && c.LastName == addUserViewModel.LastName
                                                         && c.MiddleName == addUserViewModel.MiddleName);
            if (dbUser != null)
            {
                ModelState.AddModelError("", "User Already Exists");
                return View();
            }

            var user = new ApplicationUser()
            {
                UserName = addUserViewModel.Email,
                Email = addUserViewModel.Email,
                //CompanyID = addUserViewModel.CompanyID,
                FirstName = addUserViewModel.FirstName,
                LastName = addUserViewModel.LastName,
                IsTracked = true,
                DateOfBirth = DateTime.Parse(addUserViewModel.DateOfBirth),
                MiddleName = addUserViewModel.MiddleName,
                //TeamId = addUserViewModel.TeamId,
                HourlyRate = addUserViewModel.HourlyRate,
                MonthlySalary = addUserViewModel.MonthlySalary,
                AgreementType = addUserViewModel.AgreementType.HasValue ? (short?)addUserViewModel.AgreementType.Value : null,
                HoursPerDay = addUserViewModel.HoursPerDay

            };

            IdentityResult result = await _userManager.CreateAsync(user, addUserViewModel.Password);

            if (result.Succeeded)
            {

                var RoleName = _context.Roles.FirstOrDefault(c => c.Name == addUserViewModel.Role);
                if (RoleName != null)
                {
                    await _userManager.AddToRoleAsync(user, RoleName.Name);
                }
                ModelState.Clear();
                ViewData["success"] = "A new user has been added successfully";
                return View(new AddUserViewModel());

            }
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addUserViewModel);
        }
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return RedirectToAction("UserManagement", _userManager.Users);
            var claims = await _userManager.GetClaimsAsync(user);
            var vm = new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                //CompanyID = user.CompanyID,
                //DepartmentID = user.DepartmentID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsTracked = user.IsTracked,
                HourlyRate = user.HourlyRate,
                MonthlySalary = user.MonthlySalary,
                AgreementType = (EmploymentType?)user.AgreementType,
                HoursPerDay = user.HoursPerDay
                ,
                MiddleName = user.MiddleName
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (!ModelState.IsValid) return View(editUserViewModel);
            var dbUser = _context.Users.FirstOrDefault(c => c.FirstName == editUserViewModel.FirstName
                                                        && c.LastName == editUserViewModel.LastName
                                                        && c.MiddleName == editUserViewModel.MiddleName
                                                        && c.Id != editUserViewModel.Id);
            if (dbUser != null)
            {
                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(editUserViewModel);
            }
            var user = await _userManager.FindByIdAsync(editUserViewModel.Id);

            if (user != null)
            {
                user.Email = editUserViewModel.Email;
                user.UserName = editUserViewModel.Email;
                //user.DepartmentID = editUserViewModel.DepartmentID;
                //user.CompanyID = editUserViewModel.CompanyID;
                user.DateOfBirth = DateTime.Parse(editUserViewModel.DateOfBirth);
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                user.MiddleName = editUserViewModel.MiddleName;

                user.IsTracked = editUserViewModel.IsTracked;
                user.HourlyRate = editUserViewModel.HourlyRate;
                user.MonthlySalary = editUserViewModel.MonthlySalary;
                user.AgreementType = editUserViewModel.AgreementType.HasValue ? (short?)editUserViewModel.AgreementType.Value : null;
                user.HoursPerDay = editUserViewModel.HoursPerDay;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Employees");


                ModelState.AddModelError("", "User not updated, something went wrong.");

                return View(editUserViewModel);
            }

            return RedirectToAction("Index", "Employees");
        }
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("UserManagement");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found");
            }

            return View("UserManagement", _userManager.Users);
        }


        //Roles management
        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public JsonResult GetRoles()
        {
            var roles = _roleManager.Roles;
            return Json(roles);
        }

        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel addRoleViewModel)
        {

            if (!ModelState.IsValid) return View(addRoleViewModel);

            var role = new IdentityRole
            {
                Name = addRoleViewModel.RoleName
            };

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addRoleViewModel);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
                return RedirectToAction("RoleManagement", _roleManager.Roles);

            var editRoleViewModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<ApplicationUser>()
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    editRoleViewModel.Users.Add(user);
            }
            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role != null)
            {
                role.Name = editRoleViewModel.RoleName;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("RoleManagement", _roleManager.Roles);

                ModelState.AddModelError("", "Role not updated, something went wrong.");

                return View(editRoleViewModel);
            }

            return RedirectToAction("RoleManagement", _roleManager.Roles);
        }

        [HttpDelete]
        public bool DeleteDepartment(int id)
        {
            return _departmentDto.Delete(id);
        }
        //[HttpPut]
        //public IActionResult EditCompany([FromBody] Category Obj)
        //{
        //    if (Obj == null)
        //    {
        //        throw new ArgumentNullException(nameof(Obj));
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(k => k.Errors).ToList();
        //        return Json(new { success = false, error = errors });
        //    }
        //    else
        //    {
        //        return Json(new { success = true, Update = _comapnyDto.Edit(Obj.ID, Obj) });
        //    }
        //}
        [HttpPut]
        public IActionResult EditDepartment([FromBody] Team Obj)
        {
            if (Obj == null)
            {
                throw new ArgumentNullException(nameof(Obj));
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(k => k.Errors).ToList();
                return Json(new { success = false, error = errors });
            }
            else
            {
                return Json(new { success = true, Update = _departmentDto.Update(Obj) });
            }
        }
        //[HttpDelete]
        //public bool DeleteCompany(int id)
        //{
        //    return _comapnyDto.Delete(id);
        //}
        public IActionResult AddDepartment([FromBody]  Team Obj)
        {
            if (ModelState.IsValid)
            {
                return Json(_departmentDto.Add(Obj));
            }
            else
            {
                throw new System.Exception();
            }
        }

        //[HttpPost]
        //public Category AddCompany([FromBody]  Category Obj)
        //{
        //    return _comapnyDto.Add(Obj);
        //}

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("RoleManagement", _roleManager.Roles);
                ModelState.AddModelError("", "Something went wrong while deleting this role.");
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found.");
            }
            return View("RoleManagement", _roleManager.Roles);
        }

        //Users in roles

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return RedirectToAction("RoleManagement", _roleManager.Roles);

            var addUserToRoleViewModel = new UserRoleViewModel { RoleId = role.Id };

            foreach (var user in _userManager.Users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }

            return View(addUserToRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);
            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return View(userRoleViewModel);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }

        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return RedirectToAction("RoleManagement", _roleManager.Roles);

            var addUserToRoleViewModel = new UserRoleViewModel { RoleId = role.Id };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }
            return View(addUserToRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return View(userRoleViewModel);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }

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