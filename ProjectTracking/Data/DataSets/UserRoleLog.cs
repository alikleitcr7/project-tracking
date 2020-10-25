using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public class UserRoleLog
    {
        [Column(Order = 1), Required]
        public string UserId { get; set; }
        [Column(Order = 2), Required]
        public string AssignedByUserId { get; set; }
        [Column(Order = 3)]
        [Range(0, 2)]
        public short RoleCode { get; set; }

        [Column(Order = 4)]
        public DateTime DateAssigned { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationUser AssignedByUser { get; set; }
    }
}
