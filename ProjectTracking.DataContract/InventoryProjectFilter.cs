using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class InventoryProjectFilter
    {
        public string title { get; set; }
        public string selectedColumns { get; set; }

        public DateTime? dateModifiedFrom { get; set; }
        public DateTime? dateModifiedTo { get; set; }

        public DateTime? datePublishedFrom { get; set; }
        public DateTime? datePublishedTo { get; set; }

        public int? inventoryTypeId { get; set; }
        public int? inventoryStatusId { get; set; }
        public int? countryId { get; set; }
        public int? updateFrequencyId { get; set; }
        public bool? needsUpdate { get; set; }

        public List<int> publishingChannelIds { get; set; }
        public List<int> subProjectIds { get; set; }

        public int? personResponsibleId { get; set; }
        public int? secondaryPersonResponsibleId { get; set; }

        public int page { get; set; }
        public int countPerPage { get; set; }
    }
}
