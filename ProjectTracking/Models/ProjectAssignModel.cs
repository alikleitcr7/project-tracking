using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
   public class ProjectAssignModel
    {
        public int timeSheetId { get; set; }
        public List<int> projectIds { get; set; }
    }
}
