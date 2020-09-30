using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Teams
{
    public class TeamsViewModel
    {
        public Team Team { get; set; }
        public int NumberOfActiveProjects { get; set; }
    }
}
