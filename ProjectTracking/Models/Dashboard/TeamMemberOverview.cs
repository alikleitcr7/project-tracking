using ProjectTracking.DataContract;
using ProjectTracking.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Dashboard
{
    public class TeamMemberOverview
    {
        public List<KeyValuePair<DateTime, int>> ActivitiesFrequency { get; set; }
        public List<KeyValuePair<DateTime, int>> ActivitiesMinuts { get; set; }
        public List<TimeSheetActivity> LatestActivities { get; set; }
        public List<ProjectTask> AssignedTasks { get; set; }
        //public TasksPerformance TasksPerformance { get; set; }

        //public List<UserLog> LatestLogs { get; internal set; }
        //public UserLog LatestUserLog { get; set; }
    }
}
