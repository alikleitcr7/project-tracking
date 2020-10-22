using ProjectTracking.DataContract;
using ProjectTracking.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Teams
{
    public class TeamViewModel : SupervisingTeamModel
    {
        public List<KeyValuePair<string, int>> Workload { get; set; }
        public List<TimeSheetActivity> ActiveActivities { get; set; }
    }

}
