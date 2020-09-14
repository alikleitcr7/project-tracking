using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class TimeSheetStatus
    {
        public int ID { get; set; }
        //public RequestedPermissionsStatusCode IsApproved { get; set; }
        //public string SheetStatus
        //{
        //    get
        //    {
        //        return IsApproved.ToString();
        //    }
        //}
        public string Comments { get; set; }

        public int TimeSheetId { get; set; }
        public string SuperviserId { get; set; }

        public User Superviser { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }

}