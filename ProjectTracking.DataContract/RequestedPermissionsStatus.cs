using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class RequestedPermissionsStatus
    {
        public string Id { get; set; }
        public string SuperviserId { get; set; }
        public int RequestedPermissionId { get; set; }
        public RequestedPermissionsStatusCode IsApproved { get; set; }
        public string RequestStatus
        {
            get
            {
                return IsApproved.ToString();
            }
        }
        public string Comments { get; set; }
        public virtual User Superviser { get; set; }
        public virtual RequestedPermission RequestedPermission { get; set; }



    }
}