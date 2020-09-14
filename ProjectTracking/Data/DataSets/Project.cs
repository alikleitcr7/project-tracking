using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class Project
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public int? ParentId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CompanyId { get; set; }


        public Team Team { get; set; }
        public Category Category { get; set; }

        public Project Parent { get; set; }

        public IEnumerable<Project> Activities { get; set; }
        public IEnumerable<TimeSheetProject> TimeSheetProjects { get; set; }
        public IEnumerable<ProjectReference> ProjectFiles { get; set; }

    }

}