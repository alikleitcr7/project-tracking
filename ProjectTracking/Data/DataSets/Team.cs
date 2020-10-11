using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class Team : DataContract.Entity
    {
        //public int ID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30), MinLength(2)]
        public string Name { get; set; }
        [Required]
        public string AddedByUserId { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; }

        // another admin can change supervisor
        // so we need to keep who added the record
        // each update will append the supervisors ta
        public string SupervisorId { get; set; }
        public string AssignedByUserId { get; set; }
        public DateTime DateAssigned { get; set; }

        public ApplicationUser Supervisor { get; set; }
        public ApplicationUser AddedByUser { get; set; }
        public ApplicationUser AssignedByUser { get; set; }

        public List<SupervisorLog> Supervisers { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public List<TeamsProjects> TeamsProjects { get; set; }
    }
}
