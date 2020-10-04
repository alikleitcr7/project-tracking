using System;


namespace ProjectTracking.Data.DataSets
{
    public class ProjectTaskStatusModification
    {
        public int ProjectTaskId { get; set; }
        public short StatusCode { get; set; }
        public DateTime DateModified { get; set; }

        public ProjectTask ProjectTask { get; set; }
    }

}