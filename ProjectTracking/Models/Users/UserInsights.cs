﻿using ProjectTracking.DataContract;
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
        public List<TimeSheetActivity> LatestActivities { get; set; }
        public TasksPerformance TasksPerformance { get; set; }
        public List<UserLog> LatestLogs { get; internal set; }
        //public UserLog LatestUserLog { get; set; }
    }
}
