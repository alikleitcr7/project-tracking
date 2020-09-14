using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.Models.Statistics
{
   public class PermissionRequestsStats
    {
        public double Ticks { get; set; }
        public string PermissionTitle { get; set; }
        public string DurationSpan
        {
            get
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(Ticks);
                string totalTimeDisplay = "";
                totalTimeDisplay += timeSpan.Days > 0 ? $"{timeSpan.Days} d " : "";
                totalTimeDisplay += timeSpan.Hours > 0 ? $"{timeSpan.Hours} hr " : "";
                totalTimeDisplay += timeSpan.Minutes > 0 ? $"{timeSpan.Minutes} min " : "";
                return totalTimeDisplay;
            }
        }
    }
}
