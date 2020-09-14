using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data;
using ProjectTracking.Data.DataAccess;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models.Statistics;

namespace ProjectTracking.Controllers
{
    public class UsersLogsController : Controller
    {
        private readonly IUserMethods _users;

        public UsersLogsController(IUserMethods users)
        {
            _users = users;
        }
        public IActionResult Index()
        {
            return View();
        }
       public JsonResult GetUsersLogs(int page, int countPerPage,string fromDate ,string toDate)
        {
            var records = _users.GetUsersLogs(page, countPerPage,fromDate,toDate, out int totalCount);

            object oRetval = new
            {
                records,
                totalCount
            };
            return Json(oRetval);

        }
    }
}