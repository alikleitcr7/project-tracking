using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{

    public class TimeSheetActivityStopModel
    {
        public int activityId { get; set; }
        public string message { get; set; }
    }
    public class TimeSheetActivityUpdateModel
    {
        public int id { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string message { get; set; }

        private string ipAddress;
        internal int taskId;

        public void SetIpAddress(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public string GetIpAddress()
        {
            return ipAddress;
        }
    }
}
