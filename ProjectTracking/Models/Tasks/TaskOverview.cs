using ProjectTracking.DataContract;
using ProjectTracking.Models.Teams;
using ProjectTracking.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Tasks
{
    public class TaskOverview
    {
        public List<KeyValuePair<DateTime, int>> ActivitiesFrequency { get; set; }
        public List<KeyValuePair<DateTime, int>> ActivitiesMinutes { get; set; }
        public List<KeyValuePair<UserKeyValue, int>> UserActivitiesFrequency { get; set; }
        public List<KeyValuePair<UserKeyValue, int>> UserActivitiesMinutes { get; set; }
        public List<TimeSheetActivity>  ActiveActivities { get; set; }
    }
}
