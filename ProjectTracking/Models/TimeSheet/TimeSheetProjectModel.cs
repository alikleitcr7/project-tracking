using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{
    public class TimeSheetProjectModel
    {
        public List<Project> Projects { get; set; }

        public class Project
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public List<SubProject> SubProjects { get; set; }
        }

        public class SubProject
        {
            public int ID { get; set; }
            public int ProjectId { get; set; }
            public int ParentId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            //public List<TimeSheetActivity> Activities { get; set; }

        }

        public static TimeSheetProjectModel GenerateModel(DataContract.TimeSheet ts, List<DataContract.Project> parents)
        {
            throw new NotImplementedException();
            //TimeSheetProjectModel model = new TimeSheetProjectModel();

            //model.Projects = parents.Select(k => new Project()
            //{
            //    ID = k.ID,
            //    Title = k.Title,
            //    Description = k.Description,
            //}).ToList();

            //foreach (Project project in model.Projects)
            //{

            //    var projectTasks = ts.TimeSheetTasks
            //        .Where(k => k.Project.ParentId == project.ID)
            //        .ToList();

            //    if (projectTasks != null)
            //    {
            //        project.SubProjects = projectTasks.Select(k => new SubProject()
            //        {
            //            ID = k.ID,
            //            ProjectId = k.Project.ID,
            //            ParentId = project.ID,
            //            Title = k.Project.Title,
            //            Description = k.Project.Description,
            //            //Activities = k.Activities
            //        }).ToList();
            //    }
            //}

            //return model;
        }
    }
}
