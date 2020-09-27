
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;

namespace ProjectTracking.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }

        [Route("/error")]
        public IActionResult Index(int? statusCode)
        {
            int code = (statusCode ?? 0);

            return View(code);
        }
    }
}