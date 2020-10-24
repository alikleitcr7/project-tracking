using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Projects
{
    public class ProjectInclude
    {
        public bool TeamsProjects { get; set; }
        public bool Category { get; set; }
        public bool AddedByUser { get; set; }
    }
}
