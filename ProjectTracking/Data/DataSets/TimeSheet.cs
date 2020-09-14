using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheet
    {
        public int ID { get; set; }
        //public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DateAdded { get; set; }

        public string UserId { get; set; }
        public bool IsSigned { get; set; }

        public ApplicationUser User { get; set; }
        public  ICollection<TimeSheetStatus> TimeSheetStatuses { get; set; }
        public ICollection<TimeSheetProject> TimeSheetProjects { get; set; }
    }

}