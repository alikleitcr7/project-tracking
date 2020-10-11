using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class SupervisorLog : DataContract.Entity
    {
        [Column(Order = 1), Required]
        public string UserId { get; set; }
        [Column(Order = 2), Required]
        public string AssignedByUserId { get; set; }
        [Column(Order = 3)]
        public int TeamId { get; set; }

        [Column(Order = 4)]
        public DateTime DateAssigned { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationUser AssignedByUser { get; set; }
        public Team Team { get; set; }
    }
}
