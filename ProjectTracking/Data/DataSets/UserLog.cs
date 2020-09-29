using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ProjectTracking.DataContract.Interfaces;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.DataSets
{
    public class UserLog : IUserLog
    {
        public int ID { get; set; }
        public string Address { get; set; }
        //public string ConnectionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Comments { get; set; }
        public string UserId { get; set; }

        public  ApplicationUser User { get; set; }
        public IpAddress IpAddress { get; set; }
    }
}
