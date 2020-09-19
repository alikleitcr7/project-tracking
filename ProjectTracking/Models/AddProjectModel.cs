using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.Models
{
    public class AddProjectModel
    {
        public int teamId { get; set; }
        public int? parentId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Project Title can not be less than 2 letters")]
        public string title { get; set; }
        public string description { get; set; }
        public int categoryId { get; set; }

    }
}
