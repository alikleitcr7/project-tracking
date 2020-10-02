using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    //: IUserLog
    public class UserLog
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        //public string ConnectionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Comments { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string IpAddressDisplay
        {
            get
            {
                return IPAddress == null ? "-" : (IPAddress.Title ?? IPAddress.Address);
            }
        }

        public IpAddress IPAddress { get; set; }
        public string DisplayFromDate
        {
            get
            {
                return FromDate.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
        public string DisplayToDate
        {
            get
            {
                return !ToDate.HasValue ? "-" : ToDate.Value.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
    }
}
