using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class Team
    {
        public int ID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30), MinLength(2)]
        public string Name { get; set; }


        public ICollection<ApplicationUser> Members { get; set; }
        public ICollection<TeamsProjects> TeamsProjects { get; set; }
    }
}
