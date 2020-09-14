using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
   public class AssignSuperVisorModel
    {
        public string UserId { get; set; }
        public List<string> SuperViseIds { get; set; }
    }
}
