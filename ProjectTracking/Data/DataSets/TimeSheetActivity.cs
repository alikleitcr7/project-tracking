using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public abstract class TimeSheetActivityBase
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Message { get; set; } // was Comment
        [Column("IpAdd")]
        public string Address { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.Now;

        public IpAddress IpAddress { get; set; }
    }

    public class TimeSheetActivity : TimeSheetActivityBase
    {
        public int TimeSheetTaskId { get; set; }
        public TimeSheetTask TimeSheetTask { get; set; }

        public List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        //public int ID { get; set; }
        //public int TimeSheetActivityId { get; set; }

        public int TimeSheetActivityId { get; set; }
        public TimeSheetActivity TimeSheetActivity { get; set; }
    }
}