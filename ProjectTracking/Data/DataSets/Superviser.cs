using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class Superviser
    {
        public string SupervisorId { get; set; }
        public  ApplicationUser Supervisor { get; set; }

        public string UserId { get; set; }
        public  ApplicationUser User { get; set; }

        //public virtual List<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
    }
}
