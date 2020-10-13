using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using Microsoft.AspNetCore.Http;

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
        public TimeSheetActivity Add([FromBody]TimeSheetActivity activity)
        {

            //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            //if (Request.Headers.ContainsKey("X-Forwarded-For"))
            //    remoteIpAddress = Request.Headers["X-Forwarded-For"];

            string ipAddress =  _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(); 

            return _activitiesMethods.Add(activity, ipAddress);
        }
        public TimeSheetActivity Get(int id)
        {
            return _activitiesMethods.Get(id);
        }
        public List<TimeSheetActivity> GetByTimeSheet(int timesheetId)
        {
            return _activitiesMethods.GetByTimeSheet(timesheetId);
        }
        public TimeSheetActivity Update([FromBody]TimeSheetActivity activity)
        {
            //string remoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            //if (Request.Headers.ContainsKey("X-Forwarded-For"))
            //    remoteIpAddress = Request.Headers["X-Forwarded-For"];

            //activity.IpAddress = remoteIpAddress;

            string ipAddress = _accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString(); ;


            return _activitiesMethods.Update(activity, ipAddress);
        }
        public bool Delete(int id)
        {
            return _activitiesMethods.Delete(id);
        }
    }
}