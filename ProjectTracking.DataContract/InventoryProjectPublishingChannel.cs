using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class InventoryProjectPublishingChannel
    {
        public int InventoryProjectId { get; set; }
        public int PublishingChannelId { get; set; }

        public virtual InventoryProject InventoryProject { get; set; }
        public virtual PublishingChannel PublishingChannel { get; set; }
    }
}
