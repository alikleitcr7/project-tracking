using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class RequestedPermission
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Notes { get; set; }
        public string ApplicationUserId { get; set; }
        public string PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
        public virtual User ApplicationUser { get; set; }
        public string PermissionTitle { get; set; }
        public virtual List<RequestedPermissionsStatus> RequestedPermissionsStatuses { get; set; }
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
                return ToDate.ToString("dd-MM-yyyy HH:MM:ss");
            }
        }
        public string TimeSpan
        {
            get
            {
                long totalTicks = (ToDate - FromDate).Ticks;
                TimeSpan timeSpan = new TimeSpan(totalTicks);
                string totalTimeDisplay = "";
                totalTimeDisplay += timeSpan.Days > 0 ? $"{timeSpan.Days} d " : "";
                totalTimeDisplay += timeSpan.Hours > 0 ? $"{timeSpan.Hours} hr " : "";
                totalTimeDisplay += timeSpan.Minutes > 0 ? $"{timeSpan.Minutes} min " : "";
                return totalTimeDisplay;
            }
        }
    }
}