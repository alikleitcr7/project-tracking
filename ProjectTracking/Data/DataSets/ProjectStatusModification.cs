using System;


namespace ProjectTracking.Data.DataSets
{
    public class ProjectStatusModification
    {
        public int ProjectId { get; set; }
        public int? StatusCode { get; set; }
        public DateTime DateModified { get; set; }

        public Project Project { get; set; }
    }

}