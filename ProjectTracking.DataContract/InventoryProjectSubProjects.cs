using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class InventoryProjectSubProjects
    {
        public int InventoryProjectId { get; set; }
        public int InventorySubProjectId { get; set; }

        public virtual InventoryProject InventoryProject { get; set; }
        public virtual InventorySubProject InventorySubProject { get; set; }
    }
}
