using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.DataContract
{
    public enum ProjectStatus { Proposed, InProgress, Done, Failed, Terminated }
    public class Project
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public int? ParentId { get; set; }
        //public int? TeamId { get; set; }
        public int? CategoryId { get; set; }
        public short StatusCode { get; set; }

        public Category Category { get; set; }
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
                return ((ProjectStatus)StatusCode).ToString();
            }
        }

        public List<TeamsProjects> TeamsProjects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<ProjectStatusModification> ProjectStatusModifications { get; set; }
    }
}