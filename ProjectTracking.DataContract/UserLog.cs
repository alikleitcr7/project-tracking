using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class UserLog : IUserLog
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string IPAddressTitle { get; set; }
        //public string ConnectionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Comments { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string DisplayFromDate
        {
            get
            {
                return FromDate.ToString("dd-MM-yyyy HH:MM:ss");
            }
        }
        public string DisplayToDate
        {
            get
            {
                return !ToDate.HasValue ? "-" : ToDate.Value.ToString("dd-MM-yyyy HH:MM:ss");
            }
        }

    }
}
