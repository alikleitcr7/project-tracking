using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheetTask : DataContract.Entity
    {
        //public int ID { get; set; }

        public int TimeSheetId { get; set; }
        public int ProjectTaskId { get; set; }

        public virtual TimeSheet TimeSheet { get; set; }
        public  virtual ProjectTask ProjectTask { get; set; }

        public virtual ICollection<TimeSheetActivity> Activities { get; set; }
    }

}