using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class InventoryType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class InventoryStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class UpdateFrequency
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class PublishingChannel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class InventorySubProject 
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class InventoryProject
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? DatePublished { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? NeedsUpdate { get; set; }

        public string DatePublishedDisplay
        {
            get
            {
                return DatePublished.HasValue ? DatePublished.Value.ToShortDateString() : "-";
            }
        }
        public string DateAddedDisplay
        {
            get
            {
                return DateAdded.ToShortDateString();
            }
        }
        public string DateModifiedDisplay
        {
            get
            {
                return DateModified.HasValue ? DateModified.Value.ToShortDateString() : "-";
            }
        }

        public int? InventoryTypeId { get; set; }
        public int? InventoryStatusId { get; set; }
        public int? CountryId { get; set; }
        public int? UpdateFrequencyId { get; set; }

        public string InventoryTypeDisplay
        {
            get
            {
                return InventoryType == null ? "-" : InventoryType.Name;
            }
        }
        public string InventoryStatusDisplay
        {
            get
            {
                return InventoryStatus == null ? "-" : InventoryStatus.Name;
            }
        }
        public string CountryDisplay
        {
            get
            {
                return Country == null ? "-" : Country.Name;
            }
        }
        public string UpdateFrequencyDisplay
        {
            get
            {
                return UpdateFrequency == null ? "-" : UpdateFrequency.Name;
            }
        }
        public string PublishingChannelDisplay
        {
            get
            {
                return PublishingChannels == null || PublishingChannels.Count == 0 ? "-" : string.Join(",", PublishingChannels.Select(k => k.PublishingChannel == null ? k.PublishingChannelId.ToString() : k.PublishingChannel.Name).ToArray());
            }
        }

        public string SubProjectsDisplay
        {
            get
            {
                return InventorySubProjects == null || InventorySubProjects.Count == 0 ? "-" : string.Join(",", InventorySubProjects.Select(k => k.InventorySubProject == null ? k.InventorySubProjectId.ToString() : k.InventorySubProject.Name).ToArray());
            }
        }


        public string PersonResponsibleId { get; set; }
        public string SecondaryPersonResponsibleId { get; set; }

        public virtual User PersonResponsible { get; set; }
        public virtual User SecondaryPersonResponsible { get; set; }

        public string PersonResponsibleDisplay
        {
            get
            {
                return PersonResponsible == null ? "-" : PersonResponsible.FullName;
            }
        }
        public string SecondaryPersonResponsibleDisplay
        {
            get
            {
                return SecondaryPersonResponsible == null ? "-" : SecondaryPersonResponsible.FullName;
            }
        }

        public virtual InventoryType InventoryType { get; set; }
        public virtual InventoryStatus InventoryStatus { get; set; }
        public virtual Country Country { get; set; }
        public virtual UpdateFrequency UpdateFrequency { get; set; }
        public virtual List<InventoryProjectPublishingChannel> PublishingChannels { get; set; }
        public virtual List<InventoryProjectSubProjects> InventorySubProjects { get; set; }
    }
}