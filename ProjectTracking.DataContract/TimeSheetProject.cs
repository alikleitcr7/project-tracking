using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class TimeSheetProject
    {
        public int ID { get; set; }
        public int ProjectId { get; set; }
        public int TimeSheetId { get; set; }

        public virtual Project Project { get; set; }
        //public virtual TimeSheet TimeSheet { get; set; }

        public virtual List<TimeSheetActivity> Activities { get; set; }
    }
}