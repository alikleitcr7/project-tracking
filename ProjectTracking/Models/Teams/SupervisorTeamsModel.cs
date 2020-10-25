using ProjectTracking.DataContract;
using ProjectTracking.Models.Projects;
using ProjectTracking.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Teams
{
    public class SupervisorTeamsModel
    {
        public List<SupervisingTeamModel> SupervisingTeams { get; set; }
        public List<SupervisedTeamModel> SupervisedTeams { get; set; }
    }

    public class SupervisorTeamsViewModel
    {
        public string SupervisorId { get; set; }
        public string SupervisorName { get; set; }
        public bool IncludeTitle { get; set; } = true;
    }

    public class SupervisorTeamBaseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public string DateAddedDisplay => DateAdded.ToDisplayDate();
    }

    public class SupervisingTeamModel : SupervisorTeamBaseModel
    {
        public int ProjectsCount { get; set; }
        public int MembersCount { get; set; }
        public TasksPerformance TasksPerformance { get; set; }

        public List<KeyValuePair<string, string>> Members { get; set; }
        public List<KeyValuePair<DateTime, int>> ActivitiesFrequency { get; set; }
    }


    public class TasksWorkload
    {
        public int DoneCount { get; set; }
        public int ProgressCount { get; set; }
        public int PendingCount { get; set; }
    }

    public class SupervisedTeamModel : SupervisorTeamBaseModel
    {
        public string AssignedByName { get; set; }
        public string AssignedById { get; set; }
        public DateTime DateAssigned;
        public string DateAssignedDisplay => DateAssigned.ToDisplayDate();
    }
}
