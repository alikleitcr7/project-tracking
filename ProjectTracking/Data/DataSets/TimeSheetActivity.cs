using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public abstract class TimeSheetActivityBase : DataContract.Entity
    {
        //public int ID { get; set; }
        [Column(Order = 2)]
        public DateTime FromDate { get; set; }
        [Column(Order = 3)]
        public DateTime? ToDate { get; set; }
        [Column(Order = 4)]
        [MaxLength(150)]
        public string Message { get; set; } // was Comment
        [Column("IpAdd", Order = 5)]
        [MaxLength(15)]
        public string Address { get; set; }

        [Column(Order = 6)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        public virtual IpAddress IpAddress { get; set; }
    }

    public class TimeSheetActivity : TimeSheetActivityBase
    {
        [Column(Order = 1)]
        public int TimeSheetTaskId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual TimeSheetTask TimeSheetTask { get; set; }

        public virtual ICollection<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        //public int ID { get; set; }
        //public int TimeSheetActivityId { get; set; }
        [Column(Order = 1)]
        public int TimeSheetActivityId { get; set; }
        public virtual TimeSheetActivity TimeSheetActivity { get; set; }
    }
}