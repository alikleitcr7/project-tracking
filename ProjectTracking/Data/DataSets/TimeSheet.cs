using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace ProjectTracking.Data.DataSets
{
    public class TimeSheet : DataContract.Entity
    {
        //public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateAdded { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; }
        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<TimeSheetTask> TimeSheetTasks { get; set; }
    }

}