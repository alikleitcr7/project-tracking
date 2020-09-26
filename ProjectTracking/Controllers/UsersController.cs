using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public UsersController(IUserMethods usersMethods)
        {
            _usersMethods = usersMethods;
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