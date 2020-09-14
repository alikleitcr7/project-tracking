using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class RequestedPermissionsStatus
    {
        public string Id { get; set; }

        public string SuperviserId { get; set; }
        public int RequestedPermissionId { get; set; }

        public RequestedPermissionsStatusCode IsApproved { get; set; }
        public string Comments { get; set; }

        public  ApplicationUser Superviser { get; set; }
        public  RequestedPermission RequestedPermission { get; set; }
    }
}   