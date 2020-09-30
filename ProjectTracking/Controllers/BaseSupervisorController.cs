using ProjectTracking.Data.Methods.Interfaces;
using System;

namespace ProjectTracking.Controllers
{
    public class BaseSupervisorController : BaseController
    {
        private readonly IUserMethods userMethods;

        public BaseSupervisorController(IUserMethods userMethods)
        {
            this.userMethods = userMethods;
        }

        public bool IsSupervisor()
        {
            string userid = GetCurrentUserId();

            if (userid == null)
            {
                return false;
            }

            return userMethods.IsSupervisor(userid);
        }

        public void EnsureSupervisor()
        {
            if (!IsSupervisor())
            {
                throw new UnauthorizedAccessException("you are not a supervisor");
            }
        }
    }
}
