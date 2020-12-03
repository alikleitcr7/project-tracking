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
    [Authorize(AuthPolicies.Managers)]
    public class ManageController : Controller
    {
        private readonly IUserMethods _userMethods;
        private readonly IRoleKeyMethods roleKeyMethods;

        public ManageController(IUserMethods userMethods, IRoleKeyMethods roleKeyMethods)
        {
            _userMethods = userMethods;
            this.roleKeyMethods = roleKeyMethods;
        }

        //[Route("/manage")]
        //public IActionResult Manage()
        //{
        //    return View();
        //}

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



        [HttpPut]
        [Authorize(AuthPolicies.Admins)]
        public IActionResult RegenerateKey(string role)
        {
            try
            {
                string key = Guid.NewGuid().ToString("N").Substring(0, 13).ToUpper();

                bool parsed = Enum.TryParse(role, out ApplicationUserRole appUserRole);

                if (!parsed)
                {
                    throw new Exception("invalid role");
                }

                roleKeyMethods.ChangeKey(appUserRole, key);

                return Ok(key);
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
        [Authorize(AuthPolicies.Admins)]
        public IActionResult GetRoleKeys()
        {
            try
            {
                return Ok(roleKeyMethods.GetRoleKeys().Select(k => new { Key = k.Key.ToString(), k.Value }).ToList());
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


        public IActionResult Index()
        {
            return View();
        }
    }
}