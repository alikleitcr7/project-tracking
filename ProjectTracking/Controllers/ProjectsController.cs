using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectTracking.Models.Projects;
using ProjectTracking.Exceptions;
using System.Security.Claims;

namespace ProjectTracking.Controllers
{
    //[Authorize(Policy = "Administration")]
    [Route("[controller]/[action]")]
    public class ProjectsController : Controller
    {
        private readonly ITeamsMethods _teams;
        private readonly ICategoriesMethods _categoriesMethods;
        private readonly IProjectsMethods _projects;
        //private readonly IProjectFilesMethods _file;
        //private readonly IProjectsStatistics _projectsStatistics;
        //private readonly IUserMethods _users;

        public ProjectsController(ITeamsMethods teams,
            ICategoriesMethods categoriesMethods, IProjectsMethods projects)
        {
            _teams = teams;
            _categoriesMethods = categoriesMethods;
            _projects = projects;
            //_users = users;
            //_projectsStatistics = projectsStatistics;
        }

        [Route("/projects")]
        //[Authorize(Policy = "Administration")]
        public IActionResult Index()
        {
            return View();
        }


        #region Methods

        [HttpGet]
        public List<Team> GetTeams()
        {
            return _teams.GetAll();
        }

        public JsonResult GetCategories()
        {
            return Json(_categoriesMethods.GetAll());
        }

        [HttpPost]
        public JsonResult Add([FromBody] AddProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Select(k => k.Errors).ToList();
                return Json(new { success = false, error = errors });
            }

            return Json(new { Added = _projects.Add(model.title, model.description, model.teamId, model.categoryId, model.parentId), success = true });
        }

        [HttpGet]
        public JsonResult Get(int teamId, int categoryId, int page, int countPerPage)
        {

            var records = _projects.Get(teamId, categoryId, page, countPerPage, out int totalCount);

            object oRetval = new
            {
                records,
                totalCount
            };

            return Json(oRetval);
        }

        [HttpPost]
        public IActionResult Save([FromBody]ProjectSaveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                return Ok(_projects.Save(model, userId));
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
        public JsonResult GetById(int id)
        {
            return Json(_projects.GetById(id));
        }

        [HttpGet]
        public IActionResult GetProjectStatuses()
        {
            return Ok(Enum.GetNames(typeof(ProjectStatus)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
        }

        [HttpGet]
        public IActionResult GetOverview(int projectId)
        {
             try
            {

                return Ok(_projects.GetOverview(projectId));
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
        public IActionResult Search(int? categoryId, string keyword, int page, int countPerPage)
        {
            try
            {
                var record = _projects.Search(categoryId, keyword, page, countPerPage, out int totalCount);

                return Ok(new
                {
                    record,
                    totalCount
                });
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
        public IActionResult GetStatusModifications(int projectId)
        {
            try
            {
                var record = _projects.GetStatusModifications(projectId);

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
        public IActionResult GetByTeam(int teamId)
        {
            try
            {
                var record = _projects.GetByTeam(teamId);

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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_projects.Delete(id));
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

        #endregion

    }
}