using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class InventoryProjectPublishingChannel
    {
        public int InventoryProjectId { get; set; }
        public int PublishingChannelId { get; set; }

        public  InventoryProject InventoryProject { get; set; }
        public  PublishingChannel PublishingChannel { get; set; }
    }
}
