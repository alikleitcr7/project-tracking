using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;

namespace ProjectTracking.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class ProjectTasksController : BaseController
    {
        private readonly IProjectsMethods _projectsMethods;
        private readonly ITasksMethods _tasksMethods;

        public ProjectTasksController(IProjectsMethods projectsMethods, ITasksMethods tasksMethods)
        {
            _projectsMethods = projectsMethods;
            _tasksMethods = tasksMethods;
        }

        [Route("/projects/{projectId}")]
        public IActionResult Index(int projectId)
        {
            Project project = _projectsMethods.GetById(projectId);

            if (project == null)
            {
                return NotFound();
            }

            string currentUserId = GetCurrentUserId();

            ApplicationUserRole currentUserRole = GetCurrentUserRole();

            bool isProjectSupervisor = false;

            if (currentUserRole == ApplicationUserRole.TeamMember && !_projectsMethods.MemberCanAccessProject(currentUserId, project.ID))
            {
                return NotFound();
            }
            else if (currentUserRole == ApplicationUserRole.Supervisor)
            {
                isProjectSupervisor = _projectsMethods.IsSupervisorOfProject(currentUserId, project.ID);
            }

            ViewData["IsProjectSupervisor"] = isProjectSupervisor;

            return View(project);
        }

        [Route("/tasks/{taskId}")]
        public IActionResult Details(int taskId)
        {
            ProjectTask task = _tasksMethods.GetByIdWithProjectTitle(taskId);

            if (task == null)
            {
                return NotFound();
            }

            string currentUserId = GetCurrentUserId();

            ApplicationUserRole currentUserRole = GetCurrentUserRole();

            bool isProjectSupervisor = false;

            if (currentUserRole == ApplicationUserRole.TeamMember && !_projectsMethods.MemberCanAccessProject(currentUserId, task.ProjectId))
            {
                return NotFound();
            }
            else if (currentUserRole == ApplicationUserRole.Supervisor)
            {
                isProjectSupervisor = _projectsMethods.IsSupervisorOfProject(currentUserId, task.ProjectId);
            }

            ViewData["IsProjectSupervisor"] = isProjectSupervisor;

            return View(task);
        }

        [HttpPost]
        [Authorize(AuthPolicies.Managers)]
        public IActionResult Save([FromBody]TaskSaveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                model.SetStatusByUserId(GetCurrentUserId());

                return Ok(_tasksMethods.Save(model));
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
        //[Route("GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_tasksMethods.GetById(id));
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
        //[Route("GetProjectTaskStatuses")]
        public IActionResult GetProjectTaskStatuses()
        {
            return Ok(Enum.GetNames(typeof(ProjectTaskStatus)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
        }

        [HttpGet]
        //[Route("Search")]
        public IActionResult Search(string keyword, int projectId, int page, int countPerPage)
        {
            try
            {
                var record = _tasksMethods.Search(keyword, projectId, page, countPerPage, out int totalCount);

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
        public IActionResult GetByProject(int projectId)
        {
            try
            {
                var record = _tasksMethods.GetByProject(projectId);

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
        public IActionResult GetStatusModifications(int taskId)
        {
            try
            {
                var record = _tasksMethods.GetStatusModifications(taskId);

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
        public IActionResult GetOverview(int taskId)
        {
            try
            {
                var record = _tasksMethods.GetOverview(taskId);

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
        [Authorize(AuthPolicies.Managers)]
        //[Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_tasksMethods.Delete(id));
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
        //[Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int taskId, short statusCode)
        {
            try
            {
                await _tasksMethods.ChangeStatus(taskId, statusCode, GetCurrentUserId());

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

        [HttpPut]
        //[Route("ChangeStatus")]
        public async Task<IActionResult> ChangeTimeSheetTaskStatus(int timeSheetId, int taskId, short statusCode)
        {
            try
            {
                await _tasksMethods.ChangeTimeSheetTaskStatus(timeSheetId, taskId, statusCode, GetCurrentUserId());

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