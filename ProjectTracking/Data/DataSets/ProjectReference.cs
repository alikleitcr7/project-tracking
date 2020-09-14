using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class ProjectReference : Entity
    {
        public string Name { get; set; }
        public int ProjectId { get; set; }

        public  Project Project { get; set; }
        public  ICollection<TimeSheetActivity> TimeSheetActivities { get; set; }
    }
}
