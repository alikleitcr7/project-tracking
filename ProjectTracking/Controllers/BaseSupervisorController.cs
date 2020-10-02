using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Controllers
{
    public class BaseSupervisorController : BaseController
    {
        protected readonly IUserMethods _userMethods;

        public BaseSupervisorController(IUserMethods userMethods)
        {
            this._userMethods = userMethods;
        }

        public bool IsSupervisor()
        {
            string userid = GetCurrentUserId();

            if (userid == null)
            {
                return false;
            }

            return _userMethods.IsSupervisor(userid);
        }

        /// <summary>
        /// gets supervising users for the current user if supervisor
        /// under the teams he or she is supervising
        /// </summary>
        /// <returns>UserIds</returns>
        public List<string> SupervisingUsers()
        {
            return SupervisingUsers(GetCurrentUserId());
        }

        /// <summary>
        /// gets supervising users for a supervisor
        /// under the teams he or she is supervising
        /// </summary>
        /// <returns>UserIds</returns>
        public List<string> SupervisingUsers(string supervisorId)
        {
            return _userMethods.SupervisingUsers(supervisorId);
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
