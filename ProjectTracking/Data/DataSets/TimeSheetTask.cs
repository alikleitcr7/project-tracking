using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheetTask
    {
        public int ID { get; set; }

        public int TimeSheetId { get; set; }
        public int ProjectTaskId { get; set; }

        public TimeSheet TimeSheet { get; set; }
        public ProjectTask ProjectTask { get; set; }

        public List<TimeSheetActivity> Activities { get; set; }
    }

}