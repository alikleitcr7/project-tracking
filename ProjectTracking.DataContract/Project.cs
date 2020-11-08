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
        public int CategoryId { get; set; }
        public short StatusCode { get; set; }

        public Category Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? PlannedEnd { get; set; }
        public DateTime? ActualEnd { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string AddedByUserId { get; set; }
        public string AddedByUserName { get; set; }
        public string StatusByUserId { get; set; }
        public string StatusByUserName { get; set; }

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

        public string LastModifiedDateDisplay
        {
            get
            {
                return LastModifiedDate.ToDisplayDate();
            }
        }

        public string StatusDisplay
        {
            get
            {
                return ((ProjectStatus)StatusCode).ToString();
            }
        }

        public IEnumerable<TeamsProjects> TeamsProjects { get; set; }
        public List<ProjectTask> Tasks { get; set; }
        public List<ProjectStatusModification> ProjectStatusModifications { get; set; }

        public bool? HasTasks { get; set; }

    }
}