using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{
    public class TimeSheetSaveModel
    {
        public int? id { get; set; }

        [Required]
        public string userId { get; set; }

        [Required]
        public DateTime fromDate { get; set; }
        [Required]
        public DateTime toDate { get; set; }

        [Required]
        private string addedByUser;

        public string GetAddedByUser()
        {
            return addedByUser;
        }
        public void SetAddedByUser(string user)
        {
            addedByUser = user;
        }
    }
}
