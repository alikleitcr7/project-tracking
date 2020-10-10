using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class IpAddress
    {
        [MaxLength(15), Required]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        public List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
        public List<TimeSheetActivity> TimeSheetActivities { get; set; }
        public List<UserLog> UserLogs { get; set; }
    }
}