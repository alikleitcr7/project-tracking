using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Teams;

namespace ProjectTracking.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    // if u place the policy here, then if you call the other methods 
    // via ajax they will require that policy!!!!!
    [Authorize]
    [Route("[controller]/[action]")]
    public class TeamsController : BaseController
    {
        private readonly ITeamsMethods _teamsMethods;
        private readonly IUserMethods userMethods;

        public TeamsController(ITeamsMethods teamsMethods, IUserMethods userMethods)
        {
            _teamsMethods = teamsMethods;
            this.userMethods = userMethods;
        }

        [Route("/teams")]
        [Authorize(AuthPolicies.Managers)]
        public IActionResult Index()
        {
            string userId = GetCurrentUserId();

            ViewData["UserId"] = userId;

            return View();
        }

        [Route("/teams/form")]
        [Authorize(AuthPolicies.Admins)]
        public IActionResult Form()
        {
            string userId = GetCurrentUserId();

            ViewData["UserId"] = userId;

            return View();
        }


        [Route("/teams/{id:int}")]
        public IActionResult Details(int id)
        {
            Team team = _teamsMethods.GetById(id, true);

            if (team == null)
            {
                return NotFound();
            }

            string userId = GetCurrentUserId();
            Models.Users.UserKeyValue user = userMethods.GetUserKeyValue(userId);

            bool isSupervisor = team.SupervisorId == userId;

            ViewData["UserId"] = userId;
            ViewData["IsTeamSupervisor"] = isSupervisor;

            bool userIsTeamMember = user.TeamId.HasValue && user.TeamId.Value == id;

            bool secureMode = !userIsTeamMember && !isSupervisor && user.RoleCode != (short)ApplicationUserRole.Admin;

            ViewData["SecureMode"] = secureMode;

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }


        [HttpGet]
        public IActionResult GetById(int id, bool includeMembers)
        {
            try
            {
                return Ok(_teamsMethods.GetById(id, includeMembers));
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
        public JsonResult GetAll()
        {
            return Json(_teamsMethods.GetAll());
        }

        [HttpGet]
        public JsonResult GetTeamUsers(int teamId)
        {
            return Json(_teamsMethods.GetTeamUsers(teamId));
        }

        [HttpGet]
        public IActionResult GetAllSupervisableTeams(string userId)
        {
            try
            {
                var supervisable = _teamsMethods.GetAllSupervisableTeams(userId);
                //var supervised = includeSupervisedTeamsIds ? _teamsMethods.GetAllSupervisingTeamIds(userId) : null;

                return Ok(supervisable);
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
        public IActionResult GetAllSupervisingTeamIds(string userId)
        {
            try
            {
                var supervised = _teamsMethods.GetAllSupervisingTeamIds(userId);

                return Ok(supervised);
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
        public IActionResult GetSupervisingTeamId(string userId)
        {
            try
            {
                var supervised = _teamsMethods.GetSupervisingTeamId(userId);

                return Ok(supervised);
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

        public IActionResult GetSupervisorTeamsModel(string userId)
        {
            try
            {
                SupervisorTeamsModel model = _teamsMethods.GetSupervisorTeamsModel(userId);

                return Ok(model);
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

        public IActionResult GetSupervisingTeamsModel(string userId)
        {
            try
            {
                List<SupervisingTeamModel> model = _teamsMethods.GetSupervisingTeamsModel(userId);

                return Ok(model);
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

        public IActionResult GetCurrentUserTeamsViewModel()
        {
            try
            {
                string userId = GetCurrentUserId();
                ApplicationUserRole role = GetCurrentUserRole();

                if (role == ApplicationUserRole.Admin)
                {
                    // this will get all the teams
                    userId = null;
                }
                else if (role != ApplicationUserRole.Supervisor)
                {
                    throw new UnauthorizedAccessException("unauthorized");
                }

                List<SupervisingTeamModel> model = _teamsMethods.GetSupervisingTeamsModel(userId);

                return Ok(model);
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

        public IActionResult GetTeamViewModel(int teamId)
        {
            try
            {
                TeamViewModel model = _teamsMethods.GetTeamViewModel(teamId);

                return Ok(model);
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

        public class AddRemoveTeamsUsersParam
        {
            public int teamId { get; set; }
            public List<string> userIds { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveTeamsUsers([FromBody]AddRemoveTeamsUsersParam model)
        {
            try
            {
                await _teamsMethods.AddRemoveTeamsUsers(model.teamId, model.userIds);

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
        public JsonResult Add([FromBody]Team team)
        {
            return Json(_teamsMethods.Add(team));
        }

        [HttpPut]
        public JsonResult Update([FromBody]Team team)
        {
            return Json(_teamsMethods.Update(team));
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]TeamSaveModel model)
        {
            try
            {
                if (!model.id.HasValue)
                {
                    model.SetAddedByUserId(GetCurrentUserId());
                }

                model.SetAssignedByUserId(GetCurrentUserId());

                return Ok(await _teamsMethods.Save(model));
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
        public IActionResult GetSupervisorLog(int teamId)
        {
            try
            {
                return Ok(_teamsMethods.GetSupervisorLog(teamId));
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
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_teamsMethods.Delete(id));
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