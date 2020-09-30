using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectTracking.Controllers
{
    public class BaseController : Controller
    {
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        public string GetCurrentUserId()
        {
            return IsAuthenticated() ? User.FindFirst(ClaimTypes.NameIdentifier).Value : null;
        }
    }
}
