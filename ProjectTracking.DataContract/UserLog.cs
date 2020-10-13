using ProjectTracking.DataContract.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public enum UserLogStatus { Login, Logout, Disconnected }
    //: IUserLog
    public class UserLog
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        //public string ConnectionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public short LogStatusCode { get; set; } // was Comment
        public string FullName { get; set; }
        public string UserName { get; set; }

        public UserLogStatus LogStatus
        {
            get
            {
                return ((UserLogStatus)LogStatusCode);
            }
        }

        public string LogStatusDisplay
        {
            get
            {
                return LogStatus.ToString();
            }
        }

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
