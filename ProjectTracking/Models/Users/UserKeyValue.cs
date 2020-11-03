using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Users
{
    public class UserKeyValue
    {
        public UserKeyValue(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int? TeamId { get; set; }

        public KeyValuePair<int, string> Team { get; set; }
        public short? RoleCode { get; set; }
        public string RoleDisplay => RoleCode.HasValue ? ((ApplicationUserRole)RoleCode.Value).ToString() : null;
    }
}
