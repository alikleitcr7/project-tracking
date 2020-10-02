using System;


namespace ProjectTracking.DataContract
{
    public class ProjectStatusModification
    {
        public int ProjectId { get; set; }
        public int? StatusCode { get; set; }
        public DateTime DateModified { get; set; }

        public Project Project { get; set; }
    }

}