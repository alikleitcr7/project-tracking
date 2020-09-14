using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    //InventoryType,InventoryStatus,Country,UpdateFrequency
    //PublishingChannel,InventorySubProject

    public class InventoryType : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProject> InventoryProjects { get; set; }
    }

    public class InventoryStatus : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProject> InventoryProjects { get; set; }
    }

    public class Country : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProject> InventoryProjects { get; set; }
    }

    public class UpdateFrequency : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProject> InventoryProjects { get; set; }

    }

    public class PublishingChannel : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProjectPublishingChannel> InventoryProjectPublishingChannels { get; set; }
    }

    public class InventorySubProject : Entity
    {
        public string Name { get; set; }
        public  ICollection<InventoryProjectSubProjects> InventoryProjectSubProjects { get; set; }
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

        public int? InventoryTypeId { get; set; }
        public int? InventoryStatusId { get; set; }
        public int? CountryId { get; set; }
        public int? UpdateFrequencyId { get; set; }


        public string PersonResponsibleId { get; set; }
        public string SecondaryPersonResponsibleId { get; set; }

        public  ApplicationUser PersonResponsible { get; set; }
        public  ApplicationUser SecondaryPersonResponsible { get; set; }


        public  InventoryType InventoryType { get; set; }
        public  InventoryStatus InventoryStatus { get; set; }
        public  Country Country { get; set; }
        public  UpdateFrequency UpdateFrequency { get; set; }
        public  ICollection<InventoryProjectPublishingChannel> PublishingChannels { get; set; }
        public  ICollection<InventoryProjectSubProjects> InventorySubProjects { get; set; }
    }
}