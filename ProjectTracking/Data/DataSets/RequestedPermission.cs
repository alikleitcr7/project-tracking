using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Data.DataSets
{
    public class RequestedPermission
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }
        public string Notes { get; set; }
        public ICollection<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}