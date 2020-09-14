using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class Project
    {
        public int ID { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Project Title can not be less than 2 letters")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string DisplayDate
        {
            get
            {
                return DateAdded.ToString("dd-MM-yyyy");
            }
        }
        public int? ParentId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CompanyId{ get; set; }

        public virtual Team Department { get; set; }
        public  Project Parent { get; set; }
        public virtual Team Company { get; set; }

        public virtual List<Project> Activities { get; set; }
        public virtual List<TimeSheetProject> TimeSheetProjects { get; set; }
    }

}