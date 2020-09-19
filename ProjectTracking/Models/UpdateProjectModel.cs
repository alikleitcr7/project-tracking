using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
    public class UpdateProjectModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public int teamId{ get; set; }

    }
}
