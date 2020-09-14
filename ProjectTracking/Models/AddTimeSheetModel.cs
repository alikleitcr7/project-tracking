using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models
{
  public  class AddTimeSheetModel
    {
        public string userId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }
}
