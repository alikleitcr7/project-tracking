using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ProjectTracking.DataContract.Interfaces;
using ProjectTracking.DataContract;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTracking.Data.DataSets
{
    public class UserLog : DataContract.Entity
    {
        //public int ID { get; set; }
        [Column("IpAdd")]
        [MaxLength(15)]
        public string Address { get; set; }
        //public string ConnectionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        [Range(0, 2)]
        public short LogStatusCode { get; set; }
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
        public IpAddress IpAddress { get; set; }
    }
}
