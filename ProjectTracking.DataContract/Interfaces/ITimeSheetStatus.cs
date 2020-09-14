using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract.Interfaces
{
    public interface ITimeSheetStatus
    {
        int ID { get; set; }
         bool? IsApproved { get; set; }
        string Comments { get; set; }

        int TimeSheetId { get; set; }
        int SupervisorId { get; set; }

    }

}