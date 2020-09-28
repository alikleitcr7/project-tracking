﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;

namespace ProjectTracking.Controllers
{
    [Route("ProjectTasks")]
    public class ProjectTasksController : Controller
    {
        private readonly IProjectsMethods _projectsMethods;
        private readonly ITasksMethods _tasksMethods;

        public ProjectTasksController(IProjectsMethods projectsMethods, ITasksMethods tasksMethods)
        {
            _projectsMethods = projectsMethods;
            _tasksMethods = tasksMethods;
        }

        [Route("/projects/{projectId}/tasks")]
        public IActionResult Index(int projectId)
        {
            Project project = _projectsMethods.GetById(projectId);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult Save([FromBody]TaskSaveModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
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
        [Route("GetById")]
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
        [Route("GetProjectTaskStatuses")]
        public IActionResult GetProjectTaskStatuses()
        {
            return Ok(Enum.GetNames(typeof(ProjectTaskStatus)).Select((key, value) => new KeyValuePair<int, string>(value, key)).ToList());
        }

        [HttpGet]
        [Route("Search")]
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

        [HttpDelete]
        [Route("Delete")]
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
        [Route("ChangeStatus")]
        public IActionResult ChangeStatus(int taskId, int? statusCode)
        {
            try
            {
                _tasksMethods.ChangeStatus(taskId, statusCode);

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