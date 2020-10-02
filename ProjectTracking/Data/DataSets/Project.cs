using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class Project
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        //public int? ParentId { get; set; }
        //public int? TeamId { get; set; }
        public int? CategoryId { get; set; }
        public int? StatusCode { get; set; }
        public string AddedByUserId { get; set; }


        public Category Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }


        public ApplicationUser AddedByUser { get; set; }
        public List<TeamsProjects> TeamsProjects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<ProjectStatusModification> ProjectStatusModifications { get; set; }
    }
}