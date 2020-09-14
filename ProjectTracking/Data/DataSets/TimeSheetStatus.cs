using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheetStatus
    {
        public int ID { get; set; }
        //public RequestedPermissionsStatusCode IsApproved { get; set; }
        public string Comments { get; set; }

        public int TimeSheetId { get; set; }
        public string SuperviserId { get; set; }

        public TimeSheet TimeSheet { get; set; }
        public ApplicationUser Superviser { get; set; }
    }

}