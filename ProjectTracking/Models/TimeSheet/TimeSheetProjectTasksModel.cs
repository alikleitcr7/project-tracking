using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{
    public class TimeSheetProjectTasksModel
    {
        public int ProjectId { get; set; }
        public string ProjectTitle { get; set; }
        public List<DataContract.TimeSheetTask> TimeSheetTasks { get; set; }
    }
}
