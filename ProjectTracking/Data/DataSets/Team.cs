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
        public string AddedByUserId { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;


        public ApplicationUser AddedByUser { get; set; }
        public List<Superviser> Supervisers { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<TeamsProjects> TeamsProjects { get; set; }
    }
}
