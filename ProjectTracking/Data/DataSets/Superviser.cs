using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class Superviser
    {
        public string UserId { get; set; }
        public string AssignedByUserId { get; set; }
        public int TeamId { get; set; }
        public DateTime DateAssigned { get; set; } = DateTime.Now;

        public ApplicationUser User { get; set; }
        public ApplicationUser AssignedByUser { get; set; }
        public Team Team { get; set; }
    }
}
