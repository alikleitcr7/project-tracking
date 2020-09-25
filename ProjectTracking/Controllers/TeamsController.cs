using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class TeamsController : Controller
    {
        private readonly ITeamsMethods _teamsMethods;

        public TeamsController(ITeamsMethods teamsMethods)
        {
            _teamsMethods = teamsMethods;
        }

        [HttpGet]
        public JsonResult GetById(int id)
        {
            return Json(_teamsMethods.GetById(id));
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

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return Json(_teamsMethods.Delete(id));
        }

    }
}