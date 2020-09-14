using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models;
using System;
using System.Linq;
using System.Security.Claims;


namespace ProjectTracking.Controllers
{

    public class PendingRequestsController : Controller
    {
        private readonly IRequestedPermissions _permissionRequest;
        private readonly IUserMethods _users;
        private string _userId;
        public PendingRequestsController(IRequestedPermissions permissionRequest, IUserMethods users)
        {
            _permissionRequest = permissionRequest;
            _users = users;
        }
        [Authorize(Policy = "AllUsersCanAccess")]

        public IActionResult Index()
        {
            return View();
        }
        [Route("PendingRequests/Statistics/{userId}")]
        public IActionResult Statistics(string userId)
        {
            ViewData["UserId"] = userId;
            DataContract.User user = _users.GetEmployee(userId);
            return View(user);
        }
        public JsonResult AddRequestStatus([FromBody] RequestedPermission permission)
        {
            _userId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.Select(k => k.Errors).ToList();
                return Json(new { error = errors, success = false });
            }
            else
            {
                permission.ApplicationUserId = _userId;
                return Json(new { success = true, Added = _permissionRequest.Add(permission) });
            }
        }
        public JsonResult CheckIfEmployeeHasSubordinates()
        {
            try { _userId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value; }
            catch
            {
                return Json(new { error = "invalid user authentication", success = false });
            };

            return Json(new
            {
                success = true,
                userHasSubordinates = _users.CheckIfEmployeeHasSubordinates(_userId)
            });
        }
        public JsonResult GetPermissionRequests(int page, int countPerPage)
        {
            try { _userId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value; }
            catch
            {
                return Json(new { error = "invalid user authentication", success = false });
            }
            ;

            var records = _permissionRequest.GetPermissionRequests(_userId, page, countPerPage, out int totalCount);

            object oRetval = new
            {
                records,
                totalCount
            };

            return Json(oRetval);
        }
        public JsonResult PermitRequestedPermission([FromBody] PermitModel permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException(nameof(permission));
            }

            return Json(_permissionRequest.PermitRequestedPermission(permission));

        }
        public JsonResult GetSuperVisingPermissionRequests(int page, int countPerPage)
        {
            try { _userId = User.Claims.First(k => k.Type == ClaimTypes.NameIdentifier).Value; }
            catch
            {
                return Json(new { error = "invalid user authentication", success = false });
            }
         ;

            var records = _permissionRequest.GetSupervisingPermissionRequests(_userId, page, countPerPage, out int totalCount);
            object oRetval = new
            {
                records,
                totalCount
            };

            return Json(oRetval);
        }
        public JsonResult GetPermissionRequestsYearsOrMonthsByUser(string userId,int? year)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("invalid user ID ", nameof(userId));
            }
          return Json(_permissionRequest.GetPermissionRequestsYearsOrMonthsByUser(userId, year));
        }
        public JsonResult GetApprovedRequestedPermission(string userId, int page, int countPerPage, int? year, int? month)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("invalid user ID ", nameof(userId));
            }
            var records = _permissionRequest.GetApprovedRequestedPermission(userId, page, countPerPage, out int totalCount, year, month);
            object oRetval = new
            {
                records,
                totalCount
            };

            return Json(oRetval);

        }
        public JsonResult GetPermissionRequestsTotals(string userId,int? year, int? month)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("invalid user ID ", nameof(userId));
            }
           return Json(_permissionRequest.GetPermissionRequestsTotals(userId, year, month));

        }

    }
}