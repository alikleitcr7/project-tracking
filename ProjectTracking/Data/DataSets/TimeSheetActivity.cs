using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheetActivityBase
    {
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Comment { get; set; }
        public string IpAddress { get; set; }

        public int TimeSheetProjectTaskId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }

    public class TimeSheetActivity : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public TimeSheetTask TimeSheetTask { get; set; }

        public List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public int TimeSheetActivityId { get; set; }

        public TimeSheetActivity TimeSheetActivity { get; set; }
    }
}