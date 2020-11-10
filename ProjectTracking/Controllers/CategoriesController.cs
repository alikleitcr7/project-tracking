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

namespace ProjectTracking.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesMethods _categoriesMethods;

        public CategoriesController(ICategoriesMethods categoriesMethods)
        {
            _categoriesMethods = categoriesMethods;
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_categoriesMethods.GetById(id));
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
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_categoriesMethods.GetAll());
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
        [Authorize(AuthPolicies.Managers)]
        public IActionResult Add([FromBody]Category category)
        {
            try
            {
                return Ok(_categoriesMethods.Add(category));
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
        [Authorize(AuthPolicies.Managers)]
        public IActionResult Update([FromBody]Category category)
        {
            try
            {
                return Ok(_categoriesMethods.Update(category));
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
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_categoriesMethods.Delete(id));
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