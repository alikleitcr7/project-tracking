using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheet
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DateAdded { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<TimeSheetTask> TimeSheetTasks { get; set; }
    }

}