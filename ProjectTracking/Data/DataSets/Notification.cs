using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public abstract class Notification
    {
        public int ID { get; set; }

        public string Message { get; set; }
        public short NotificationTypeCode { get; set; }
        //public bool IsRead { get; set; }
        public DateTime DateSent { get; set; }
    }
}
