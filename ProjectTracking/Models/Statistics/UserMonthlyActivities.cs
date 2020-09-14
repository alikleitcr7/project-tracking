using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models.Statistics
{
    public class UserMonthlyActivities
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public int TotalHours { get; set; }
    }
}
