using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class TimeSheet
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime DateAdded { get; set; }


        public string FromDateDisplay
        {
            get
            {
                return FromDate.ToString("MM/dd/yyyy");
            }
        }
        public string ToDateDisplay
        {
            get
            {
                return ToDate.ToString("MM/dd/yyyy");
            }
        }

        public DateTime ToDatePlusOneDay
        {
            get
            {
                return ToDate.AddDays(1);
            }
        }
        public DateTime ToDatePlusMonth
        {
            get
            {
                return ToDate.AddDays(26);
            }
        }

        public string UserId { get; set; }

        public  User User { get; set; }
        public  List<TimeSheetTask> TimeSheetTasks { get; set; }
        public bool HasTasks { get; set; }
    }
   
}