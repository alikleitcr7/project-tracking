using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Users;

namespace ProjectTracking.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class UsersController : BaseSupervisorController
    {
        //private readonly IUserMethods _userMethods;
        //private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserMethods usersMethods, UserManager<ApplicationUser> userManager)
            : base(usersMethods)
        {
            //_userMethods = usersMethods;
            _userManager = userManager;
            //_roleManager = roleManager;
        }

        //[Route("/profile/{userId}")]
        //public IActionResult Profile(string userId)
        //{
        //    User user = _userMethods.GetById(userId);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        [HttpGet]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(_userMethods.GetById(id));
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
        public IActionResult Search(string keyword, int page, int countPerPage)
        {
            try
            {
                var record = _userMethods.Search(keyword, page, countPerPage, out int totalCount);

                return Ok(new { record, totalCount });
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
        public IActionResult GetRoles()
        {
            try
            {
                return Ok(Enum.GetNames(typeof(ApplicationUserRole)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
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

        //[HttpGet]
        //public async Task<IActionResult> GetUserRole(string userId)
        //{
        //    try
        //    {
        //        // get app user by id
        //        var appUser = await _userManager.FindByIdAsync(userId);

        //        if (appUser == null)
        //        {
        //            throw new ClientException("user not found");
        //        }

        //        return Ok(appUser.RoleCode);
        //    }
        //    catch (ClientException ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = ex.Message });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> GetUserRoleLogs(string userId)
        {
            try
            {
                List<UserRoleLog> roleLogs = _userMethods.GetUserRoleLogs(userId);

                return Ok(roleLogs);
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
        public IActionResult GetUsersByRoleKeyValue(int roleCode)
        {
            try
            {
                // has no role
                return Ok(_userMethods.GetUsersByRoleKeyValue(roleCode));
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
        public IActionResult GetUsersByRole(int roleCode)
        {
            try
            {
                // has no role
                return Ok(_userMethods.GetUsersByRole(roleCode));
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

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult SetRole(string userId, short roleCode)
        {
            try
            {
                string currentUserId = GetCurrentUserId();

                return Ok(_userMethods.SetRole(currentUserId, userId, roleCode).ToDisplayDate());
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
        //[Authorize(Roles = "Admin")]
        public IActionResult GetUserRole(string userId)
        {
            try
            {
                UserRoleLog userRole = _userMethods.GetUserRole(userId);

                return Ok(userRole);
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


        public class AddRemoveTeamsFromSupervisorParam
        {
            public string userId { get; set; }
            public int teamId { get; set; }
            //public List<int> teamIds { get; set; }
        }

        [HttpPost]
        public IActionResult AddRemoveTeamsFromSupervisor([FromBody]AddRemoveTeamsFromSupervisorParam model)
        {
            try
            {
                string currentUserId = GetCurrentUserId();

                _userMethods.AssignTeamSupervisor(currentUserId, model.userId, model.teamId);

                return Ok(true);
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

        [HttpPost]
        public IActionResult Save([FromBody]UserSaveModel user)
        {
            try
            {
                return Ok(_userMethods.Save(user));
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

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                _userMethods.Delete(id);
                return Ok(true);
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
        public IActionResult GetEmploymentTypes()
        {
            return Ok(Enum.GetNames(typeof(EmploymentType)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
        }

        [HttpGet]
        public IActionResult GetUserInsights(string userId)
        {
            try
            {
                return Ok(_userMethods.GetUserInsights(userId));
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
        public IActionResult GetLatestUserLog(string userId)
        {
            try
            {
                return Ok(_userMethods.GetLatestUserLog(userId));
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
    }
}