using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class InventoryProjectSubProjects
    {
        public int InventoryProjectId { get; set; }
        public int InventorySubProjectId { get; set; }

        public  InventoryProject InventoryProject { get; set; }
        public  InventorySubProject InventorySubProject { get; set; }
    }
}
