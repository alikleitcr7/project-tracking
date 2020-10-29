using System;
using System.Collections.Generic;

namespace ProjectTracking.Models.Teams
{
    public class TeamActivitiesFrequency
    {
        public List<KeyValuePair<DateTime, int>> Activities { get; set; }
        public TeamKeyValue Team { get; set; }
    }

}
