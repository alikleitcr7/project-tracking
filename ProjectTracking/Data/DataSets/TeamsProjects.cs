using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class TeamsProjects
    {
        public int TeamId { get; set; }
        public int ProjectId { get; set; }


        public Team Team { get; set; }
        public Project Project { get; set; }
    }
}
