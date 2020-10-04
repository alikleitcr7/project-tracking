using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.Projects
{
    public class ProjectSaveModel
    {
        public int? id { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }
        public short statusCode { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? plannedEnd { get; set; }
        public DateTime? actualEnd { get; set; }
        public List<int> teamsIds { get; set; }
    }
}
