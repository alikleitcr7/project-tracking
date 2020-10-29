using ProjectTracking.DataContract;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Teams;
using ProjectTracking.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Projects
{
    public class ProjectOverview
    {
        public TasksPerformance TasksPerformance { get; set; }

        public List<UserKeyValue> Members { get; set; }
        public List<TeamKeyValue> Teams { get; set; }
        public List<KeyValuePair<DateTime, int>> ActivitiesFrequency { get; set; }

        public List<KeyValuePair<UserKeyValue, TasksWorkload>> Workload { get; set; }
        public List<TimeSheetActivity> ActiveActivities { get; set; }
    }
 
}
