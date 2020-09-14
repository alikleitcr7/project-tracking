using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
   public class ProjectFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }


        public virtual List<TimeSheetActivity> TimeSheetActivities { get; set; }
    }
}
