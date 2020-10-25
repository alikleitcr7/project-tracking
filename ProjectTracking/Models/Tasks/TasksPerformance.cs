using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectTracking.Models.Tasks
{


    public class TasksPerformance
    {
        public int TotalCount { get; set; }
        public int DoneCount { get; set; }
        public int ProgressCount { get; set; }
        public int PendingCount { get; set; }
        public int FailedOrTerminatedCount { get; set; }
    }
}
