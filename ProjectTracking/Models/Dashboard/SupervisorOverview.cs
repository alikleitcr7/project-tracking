using ProjectTracking.DataContract;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Teams;
using ProjectTracking.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Dashboard
{
    public class SupervisorOverview
    {
        public List<TimeSheetActivity> LatestActivities { get; set; }
        public List<UserLog> UserLogsToday { get; internal set; }

        public List<MemberActivitiesFrequency> MembersActivitiesFrequency { get; set; }
        public List<MemberActivitiesFrequency> MembersActivitiesMinutes { get; set; }


        public List<TeamActivitiesFrequency> TeamsActivitiesFrequency { get; set; }
        public List<TeamActivitiesFrequency> TeamsActivitiesMinutes { get; set; }
    }

}
