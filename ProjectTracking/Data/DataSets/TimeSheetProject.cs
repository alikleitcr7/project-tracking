using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheetProject
    {
        public int ID { get; set; }

        public int ProjectId { get; set; }
        public int TimeSheetId { get; set; }


        public Project Project { get; set; }
        public TimeSheet TimeSheet { get; set; }

        public ICollection<TimeSheetActivity> Activities { get; set; }
    }

}