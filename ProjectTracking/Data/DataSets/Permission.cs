using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class Permission
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<RequestedPermission> RequestedPermissions { get; set; }
    }
}