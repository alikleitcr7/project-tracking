using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models.Statistics
{
   public class ProjectsProgress
    {
        public string ProjectTitle { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Number { get; set; }
        public string MeasurementUnit { get; set; }
        public string UserName { get; set; }
        public int Hours { get; set; }



    }
}
