using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Range(0, 4)]
        public short StatusCode { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string DateAddedDisplay
        {
            get
            {
                return DateAdded.ToDisplayDate();
            }
        }

        public string StartDateDisplay
        {
            get
            {
                return StartDate.ToDisplayDate();
            }
        }

        public string PlannedEndDisplay
        {
            get
            {
                return PlannedEnd.ToDisplayDate();
            }
        }

        public string ActualEndDisplay
        {
            get
            {
                return ActualEnd.ToDisplayDate();
            }
        }


        public string StatusDisplay
        {
            get
            {
                return ((ProjectTaskStatus)StatusCode).ToString();
            }
        }


        public string StatusByUserId { get; set; }
        public string StatusByUserName { get; set; }
        public string ProjectTitle { get; set; }

        public string LastModifiedDateDisplay
        {
            get
            {
                return LastModifiedDate.ToDisplayDate();
            }
        }

        public Project Project { get; set; }
        public IEnumerable<TimeSheetTask> TimeSheetTasks { get; set; }
        public List<ProjectTaskStatusModification> ProjectTaskStatusModifications { get; set; }


        /// VIEW EXTENSIONS
        public int? TimeSheetTaskId { get; set; }
        public int? NumberOfActivities { get; set; }
    }

}