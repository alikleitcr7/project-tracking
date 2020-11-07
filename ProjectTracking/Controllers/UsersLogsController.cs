using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data;
using ProjectTracking.Data.DataAccess;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models.Statistics;

namespace ProjectTracking.Controllers
{

    [Authorize(Policy = AuthPolicies.Managers)]
    public class UsersLogsController : BaseSupervisorController
    {
        //private readonly IUserMethods _users;

        public UsersLogsController(IUserMethods users) : base(users)
        {
            //_users = users;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsersLogs(int page, int countPerPage, DateTime? fromDate, DateTime? toDate)
        {

            List<string> supervisingIds = IsSupervisor() ? SupervisingUsers() : null;

            var records = _userMethods.GetUsersLogs(supervisingIds, page, countPerPage, fromDate, toDate, out int totalCount);

            object oRetval = new
            {
                records,
                totalCount
            };
            return Json(oRetval);

        }
    }
}