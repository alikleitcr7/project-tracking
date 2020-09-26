using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class UsersController : Controller
    {
        private readonly IUserMethods _usersMethods;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(IUserMethods usersMethods, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _usersMethods = usersMethods;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(_usersMethods.GetById(id));
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
                var record = _usersMethods.Search(keyword, page, countPerPage, out int totalCount);

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
                var record = _roleManager.Roles.ToList();

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
        public async Task<IActionResult> GetUserRole(string userId)
        {
            try
            {
                // get app user by id
                var appUser = await _userManager.FindByIdAsync(userId);

                if (appUser == null)
                {
                    throw new ClientException("user not found");
                }

                // get current roles
                var currentRoles = await _userManager.GetRolesAsync(appUser);

                if (currentRoles.Count == 1)
                {
                    return Ok(currentRoles.First());
                }

                // has no role
                return Ok("");
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
        public async Task<IActionResult> SetRole(string userId, string role)
        {
            try
            {
                // get app user by id
                var appUser = await _userManager.FindByIdAsync(userId);

                if (appUser == null)
                {
                    throw new ClientException("user not found");
                }

                // get current roles
                var currentRoles = await _userManager.GetRolesAsync(appUser);

                // clear other roles
                if (currentRoles.Count > 0 && !currentRoles.Contains(role))
                {
                    await _userManager.RemoveFromRolesAsync(appUser, currentRoles);
                }

                // add user to role
                await _userManager.AddToRoleAsync(appUser, role);

                return Ok(role);
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
            public List<int> teamIds { get; set; }
        }

        [HttpPost]
        public IActionResult AddRemoveTeamsFromSupervisor([FromBody]AddRemoveTeamsFromSupervisorParam model)
        {
            try
            {
                _usersMethods.AddRemoveTeamsFromSupervisor(model.userId, model.teamIds);

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
                return Ok(_usersMethods.Save(user));
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
                _usersMethods.Delete(id);
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

    }
}