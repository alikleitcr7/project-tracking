using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class ProjectTask
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int ProjectId { get; set; }
        public int? StatusCode { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }


        public Project Project { get; set; }
        public List<TimeSheetTask> TimeSheetTasks { get; set; }
        public List<ProjectTaskStatusModification> ProjectTaskStatusModifications { get; set; }
    }

}