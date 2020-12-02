using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Hubs;
using ProjectTracking.Models.Users;

namespace ProjectTracking.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class UsersController : BaseSupervisorController
    {
        private readonly IHubContext<ObserverHub> observerHub;
        //, IHubContext<ObserverHub> observerHub
        public UsersController(IUserMethods usersMethods, IHubContext<ObserverHub> observerHub)
            : base(usersMethods)
        {
            this.observerHub = observerHub;
        }

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
                var record = _userMethods.Search(keyword, page, countPerPage, GetCurrentUserId(), out int totalCount);

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
        public IActionResult GetTotalCountByRoles()
        {
            try
            {
                var record = _userMethods.GetTotalCountByRoles();

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
                var users = _userMethods.GetUsersByRoleKeyValue(roleCode, GetCurrentUserId());

                //if (execludeCurrentUser)
                //{
                //    string currentUserId = GetCurrentUserId();

                //    users = users.Where(k => k.Key != currentUserId).ToList();
                //}

                // has no role
                return Ok(users);
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
        [Authorize(AuthPolicies.Admins)]
        public async Task<IActionResult> SetRole(string userId, short roleCode)
        {
            try
            {
                string currentUserId = GetCurrentUserId();

                string date = _userMethods.SetRole(currentUserId, userId, roleCode).ToDisplayDate();

                await observerHub.Clients.User(userId).SendAsync("SessionEnd", "your role has been changed, you are required to login again");

                return Ok(date);
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

        [Authorize(AuthPolicies.Admins)]
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

        [Authorize(AuthPolicies.Admins)]
        [HttpPost]
        public IActionResult Save([FromBody]UserSaveModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();

                    foreach (var modelState in ModelState.Values)
                    {
                        errors.Add(string.Join(", ", modelState.Errors.Select(k => k.ErrorMessage)));
                    }

                    if (errors.Count > 0)
                    {
                        throw new ClientException(string.Join(", ", errors));
                    }
                }

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

        //[Authorize(AuthPolicies.Admins)]
        //[HttpDelete]
        //public IActionResult Delete(string id)
        //{
        //    try
        //    {
        //        _userMethods.Delete(id);
        //        return Ok(true);
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

        //[HttpGet]
        //public IActionResult GetSupervisorOverview(string userId)
        //{
        //    try
        //    {
        //        return Ok(_userMethods.GetSupervisorOverview(userId));
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

        //[HttpGet]
        //public IActionResult GetTeamMemberOverview(string userId)
        //{
        //    try
        //    {
        //        return Ok(_userMethods.GetTeamMemberOverview(userId));
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

        //[HttpGet]
        //public IActionResult GetAdminOverview(string userId)
        //{
        //    try
        //    {
        //        return Ok(_userMethods.GetAdminOverview(userId));
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

        [HttpGet]
        public IActionResult GetLatestUserLogs(string userId, int take)
        {
            try
            {
                return Ok(_userMethods.GetLatestUserLogs(userId, take));
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