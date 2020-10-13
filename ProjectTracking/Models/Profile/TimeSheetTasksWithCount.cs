using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Profile
{
    public class TimeSheetTasksWithActivityCheck
    {
        public int TaskId { get; set; }
        public bool HasActivity { get; set; }
    }
}
