using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods;
using ProjectTracking.DataContract;
using System.Collections.Generic;
using System.Linq;
using System;
using ProjectTracking.Exceptions;

namespace ProjectTracking.Controllers
{

    public class PermissionsController : Controller
    {
        private readonly IPermissionsMethods _permission;

        public PermissionsController(IPermissionsMethods permission)
        {
            _permission = permission;
        }
        [Authorize(Policy = "Administration")]

        public IActionResult Index()
        {
            return View();
        }
        public List<Permission> Get()
        {
            return _permission.Get();
        }

        public JsonResult Add([FromBody] Permission permission)
        {
            bool success = false;
            string message = "";
            Permission record = null;

            if (!ModelState.IsValid)
            {
                List<string> errors = SharedHelper.GetErrorListFromModelState(ModelState);

                message = "Invalid Input: " + string.Join(", ", errors);

                return Json(new { message, success });
            }

            try
            {
                record = _permission.Add(permission);
                success = true;
            }
            catch (ClientException recordExsitException)
            {
                message = recordExsitException.Message;
            }
            catch (Exception ex)
            {
                message = "Internal Server Error";
            }

            return Json(new { success, message, record });
        }
        public JsonResult Update([FromBody] Permission permission)
        {
            string error = "";
            bool success = false;

            if (!ModelState.IsValid)
            {
                error = "Inavlid Input: " + string.Join(", ", SharedHelper.GetErrorListFromModelState(ModelState));

                return Json(new { error, success });
            }

            try
            {
                success = _permission.Update(permission);
            }
            catch (ClientException clientEx)
            {
                error = clientEx.Message;
            }
            catch (Exception ex)
            {
                error = "Internal Server Error";
            }

            return Json(new { error, success });
        }

        public bool Delete(int id)
        {
            return _permission.Delete(id);
        }


    }
}