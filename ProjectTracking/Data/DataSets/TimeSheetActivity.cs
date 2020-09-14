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
        //public string FileName { get; set; }
        public int? Number { get; set; }
        public string IpAddress { get; set; }

        public int TimeSheetProjectId { get; set; }
        public int? TypeOfWorkId { get; set; }
        public int? MeasurementUnitId { get; set; }
        public int? ProjectFileId { get; set; }


        public ProjectReference ProjectFile { get; set; }
        public TypeOfWork TypeOfWork { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
    }

    public class TimeSheetActivity : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public TimeSheetProject TimeSheetProject { get; set; }

        public List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public int TimeSheetActivityId { get; set; }
        public DateTime DateAdded { get; set; }

        public TimeSheetActivity TimeSheetActivity { get; set; }
    }
}