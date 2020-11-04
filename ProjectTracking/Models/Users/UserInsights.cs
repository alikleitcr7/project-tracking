using ProjectTracking.DataContract;
using ProjectTracking.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Users
{
    public class UserInsights
    {
        public List<KeyValuePair<DateTime, int>> ActivitiesFrequency { get; set; }
        public List<KeyValuePair<DateTime, int>> ActivitiesMinuts { get; set; }
        public List<KeyValuePair<DateTime, int>> ActiveMinuts { get; set; }
        //public UserActionStatistics UserActionStatistics { get; set; }
        public List<TimeSheetActivity> LatestActivities { get; set; }
        public TasksPerformance TasksPerformance { get; set; }
        public List<UserLog> LatestLogs { get; internal set; }
        //public UserLog LatestUserLog { get; set; }
    }

    public class UserActionStatistics
    {
        public int ProjectsAdded { get; set; }
        public int TeamsAdded { get; set; }
        public int BroadcastsSent { get; set; }


        public int NotificationsSent { get; set; }
        public int NotificationsReceived { get; set; }


        public int SupervisorsAssigned { get; set; }
        public int NumberOfLogins { get; set; }


        //public string Action { get; set; }
        //public DateTime DateAndTime { get; set; }
        //public DateTime Date { get; set; }
        //public string DateAndTimeDisplay => DateAndTime.ToDisplayDateTime();
        //public string DateDisplay => Date.ToDisplayDateTime();
        //public string Link { get; set; }
    }
}
