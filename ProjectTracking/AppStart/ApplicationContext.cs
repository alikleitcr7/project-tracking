using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.AppStart
{
    public static class ApplicationContext
    {
        public static DateTime? LogsLastUpdatedDate;
        public static List<UserLog> ActiveLogs = new List<UserLog>();
    }
}
