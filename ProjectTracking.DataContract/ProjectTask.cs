using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.DataContract
{
    public enum ProjectTaskStatus { Pending, InProgress, Done, Failed, Terminated }

    public class ProjectTask
    {
        public int ID { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public int ProjectId { get; set; }
        public short  StatusCode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }

        public string DisplayDate
        {
            get
            {
                return DateAdded.ToString("dd-MM-yyyy");
            }
        }

        public string StartDateDisplay
        {
            get
            {
                return StartDate.HasValue ? StartDate.Value.ToString("dd-MM-yyyy") : "-";
            }
        }

        public string PlannedEndDisplay
        {
            get
            {
                return PlannedEnd.HasValue ? PlannedEnd.Value.ToString("dd-MM-yyyy") : "-";
            }
        }

        public string ActualEndDisplay
        {
            get
            {
                return ActualEnd.HasValue ? ActualEnd.Value.ToString("dd-MM-yyyy") : "-";
            }
        }

        public string StatusDisplay
        {
            get
            {
                return  ((ProjectTaskStatus)StatusCode).ToString();
            }
        }

        public Project Project { get; set; }
        public IEnumerable<TimeSheetTask> TimeSheetTasks { get; set; }
        public List<ProjectTaskStatusModification> ProjectTaskStatusModifications { get; set; }
    }

}