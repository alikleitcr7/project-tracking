using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
   public class PermitModel
    {
        //supervisingTimeSheetStatusId
        public string supervisingPermissionRequestStatusId { get; set; }
        //public RequestedPermissionsStatusCode Status { get; set; }
        public string Comment { get; set; } 
    }
}
