using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract.Interfaces
{
    public interface ITimeSheetActivity
    {
        int ID { get; set; }
        DateTime FromDate { get; set; }
        DateTime? ToDate { get; set; }
        string Comment { get; set; }

        int TimeSheetId { get; set; }
        int ActivityId { get; set; }
        string IpAddress { get; set; }
    
    }

}