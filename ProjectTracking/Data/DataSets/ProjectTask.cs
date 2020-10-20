using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class ProjectTask : DataContract.Entity
    {
        //public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int ProjectId { get; set; }
        [Range(0, 4)]
        public short StatusCode { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public string StatusByUserId { get; set; }


        public ApplicationUser StatusByUser { get; set; }
        public Project Project { get; set; }
        public List<TimeSheetTask> TimeSheetTasks { get; set; }
        public List<ProjectTaskStatusModification> ProjectTaskStatusModifications { get; set; }
    }

}