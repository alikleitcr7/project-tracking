using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using Microsoft.AspNetCore.Http;
using ProjectTracking.Models.TimeSheet;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Controllers
{
    public class TimeSheetActivitiesController : Controller
    {
        private readonly ITimeSheetActivitiesMethods _activitiesMethods;
        private readonly IHttpContextAccessor _accessor;
        public TimeSheetActivitiesController(ITimeSheetActivitiesMethods activitiesMethods, IHttpContextAccessor accessor)
        {
            _activitiesMethods = activitiesMethods;
            _accessor = accessor;
        }

        [HttpPost]
        public IActionResult Start(int timeSheetTaskId)
        {
            try
            {
                string ipAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                return Ok(_activitiesMethods.Start(timeSheetTaskId, ipAddress));
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
        public IActionResult Stop([FromBody]TimeSheetActivityStopModel model)
        {
            try
            {
                return Ok(_activitiesMethods.Stop(model));
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
        public IActionResult Update([FromBody]TimeSheetActivityUpdateModel model)
        {
            try
            {
                string ipAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

                model.SetIpAddress(ipAddress);

                return Ok(_activitiesMethods.Update(model));
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
        public IActionResult GetUserActiveActivity(string userId)
        {
            try
            {
                TimeSheetActivity activity = _activitiesMethods.GetUserActiveActivity(userId);

                return Ok(activity);
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
                _activitiesMethods.Delete(id);

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

        //public TimeSheetActivity Add([FromBody]TimeSheetActivity activity)
        //{

        //    //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        //    //if (Request.Headers.ContainsKey("X-Forwarded-For"))
        //    //    remoteIpAddress = Request.Headers["X-Forwarded-For"];

        //    string ipAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();

        //    return _activitiesMethods.Add(activity, ipAddress);
        //}

        public TimeSheetActivity Get(int id)
        {
            return _activitiesMethods.Get(id);
        }

        public List<TimeSheetActivity> GetByTimeSheet(int timesheetId)
        {
            return _activitiesMethods.GetByTimeSheet(timesheetId);
        }

        //public TimeSheetActivity Update([FromBody]TimeSheetActivityUpdateModel model)
        //{
        //    //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

        //    //if (Request.Headers.ContainsKey("X-Forwarded-For"))
        //    //    remoteIpAddress = Request.Headers["X-Forwarded-For"];

        //    //activity.IpAddress = remoteIpAddress;

        //    string ipAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(); ;


        //    return _activitiesMethods.Update(activity, ipAddress);
        //}

    }
}