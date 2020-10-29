using System;
using System.Collections.Generic;

namespace ProjectTracking.Models.Users
{
    public class MemberActivitiesFrequency
    {
        public List<KeyValuePair<DateTime, int>> Activities { get; set; }
        public UserKeyValue User { get; set; }
    }
}
