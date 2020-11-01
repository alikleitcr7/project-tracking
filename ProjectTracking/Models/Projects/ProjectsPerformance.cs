namespace ProjectTracking.Models.Projects
{
    public class ProjectsPerformance
    {
        //Proposed, InProgress, Done, Failed, Terminated
        public int TotalCount { get; set; }
        public int ProposedCount { get; set; }
        public int ProgressCount { get; set; }
        public int DoneCount { get; set; }
        public int FailedOrTerminatedCount { get; set; }
    }
}
