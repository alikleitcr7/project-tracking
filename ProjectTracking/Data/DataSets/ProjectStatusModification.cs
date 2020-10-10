using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracking.Data.DataSets
{
    public class ProjectStatusModification
    {
        [Column(Order = 0)]
        public int ProjectId { get; set; }
        [Column(Order = 1)]
        public DateTime DateModified { get; set; }
        public short StatusCode { get; set; }
        [Column(Order = 2)]

        public Project Project { get; set; }
    }

}