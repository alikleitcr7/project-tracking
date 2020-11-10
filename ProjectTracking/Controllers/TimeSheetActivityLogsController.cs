using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;

namespace ProjectTracking.Controllers
{
    [Authorize]
    public class TimeSheetActivityLogsController : Controller
    {
        private readonly ITimeSheetActivityLogsMethods _activitiesMethods;
        public TimeSheetActivityLogsController(ITimeSheetActivityLogsMethods activitiesMethods)
        {
            _activitiesMethods = activitiesMethods;
        }
        public TimeSheetActivityLog Add([FromBody]TimeSheetActivityLog activity)
        {
            return _activitiesMethods.Add(activity);
        }
        public TimeSheetActivityLog Get(int id)
        {
            return _activitiesMethods.Get(id);
        }
        public List<TimeSheetActivityLog> GetByTimeSheet(int activityId)
        {
            return _activitiesMethods.GetByActivity(activityId);
        }
        public bool Update(int activityId)
        {
            return _activitiesMethods.Clear(activityId);
        }
        public bool Delete(int id)
        {
            return _activitiesMethods.Delete(id);
        }
    }
}