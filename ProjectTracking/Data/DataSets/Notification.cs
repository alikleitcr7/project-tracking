using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.DataSets
{
    public abstract class Notification : DataContract.Entity
    {
        //public int ID { get; set; }
        [MaxLength(255)]
        public string Message { get; set; }
        [Range(0,2)]
        public short NotificationTypeCode { get; set; }
        //public bool IsRead { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateSent { get; set; } = DateTime.Now;
    }
}
