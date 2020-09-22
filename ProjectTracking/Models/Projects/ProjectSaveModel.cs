using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Projects
{
    public class ProjectSaveModel
    {
        public int? id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public List<int> teamsIds { get; set; }
    }
}
