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
        public int companyId { get; set; }
        public int departmentId{ get; set; }

    }
}
