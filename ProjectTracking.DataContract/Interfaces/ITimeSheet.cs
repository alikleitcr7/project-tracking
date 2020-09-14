using System;
using System.Collections.Generic;
using System.Text;


namespace ProjectTracking.DataContract.Interfaces
{
    public interface ITimeSheet
    {
        int ID { get; set; }
        string Title { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        DateTime DateAdded { get; set; }

        string UserId { get; set; }
    }
   
}