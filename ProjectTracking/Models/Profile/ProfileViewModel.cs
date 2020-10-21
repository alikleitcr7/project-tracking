using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Profile
{
    public class ProfileViewModel
    {
        public DataContract.User User { get; set; }
        public bool HasSupervisorLog { get; set; }
        public bool HasTimeSheets { get; set; }

        public bool ShowSupervisorTab
        {
            get
            {
                return User.Role == ApplicationUserRole.Supervisor || HasSupervisorLog;
            }
        }

        public bool ShowScheduleTab
        {
            get
            {
                return User.Role == ApplicationUserRole.TeamMember || HasTimeSheets;
            }
        }

        public string CurrentUserId { get; internal set; }
    }
}
