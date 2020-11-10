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

        public ManageController(IUserMethods userMethods)
        {
            _userMethods = userMethods;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}