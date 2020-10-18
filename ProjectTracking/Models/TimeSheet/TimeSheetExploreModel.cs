using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{
    public class TimeSheetExploreModel
    {
        public string UserId { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsSameUser { get; set; }
        public bool CurrentUserIsSupervisor { get; set; }
        public ApplicationUserRole CurrentUserRole { get; set; }
        public int? TimeSheetId { get; set; }
    }
}
