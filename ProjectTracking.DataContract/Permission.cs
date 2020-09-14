using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjectTracking.DataContract
{
    public class Permission
    {

        public int ID { get; set; }
        [Required]
        [MinLength(2,ErrorMessage ="Permission Title cannot be less than 2 letters")]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}