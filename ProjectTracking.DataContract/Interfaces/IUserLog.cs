using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract.Interfaces
{
    public interface IUserLog
    {
        int ID { get; set; }
        string UserId { get; set; }
        //string ConnectionId { get; set; }
        DateTime FromDate { get; set; }
        DateTime? ToDate { get; set; }
        string Comments { get; set; }
    }
}