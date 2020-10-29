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
    public class AdminOverview
    {
        public List<KeyValuePair<ApplicationUserRole, int>> LoggedInUsers { get; set; }
        public List<ProjectDashboardView> Projects { get; set; }

        public List<UserLog> LatestLogsToday { get; internal set; }

        public List<TeamActivitiesFrequency> TeamsActivitiesFrequency { get; set; }
        public List<TeamActivitiesFrequency> TeamsActivitiesMinuts { get; set; }
    }

    public class ProjectDashboardView
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public short StatusCode { get; set; }
        public string StatusCodeDisplay => ((ProjectStatus)StatusCode).ToString();
        public int NumberOfTasks { get; set; }
    }
}
