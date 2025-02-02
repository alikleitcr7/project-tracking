using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class TimeSheetActivityBase
    {
        public int ID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime DateAdded { get; set; }

        public string Message { get; set; }
        public string Address { get; set; }
        public string IpAddressDisplay
        {
            get
            {
                return IPAddress == null ? "-" : (IPAddress.Title ?? IPAddress.Address);
            }
        }

        public virtual IpAddress IPAddress { get; set; }

        public string DateAddedDisplay
        {
            get
            {
                return DateAdded.ToDisplayDateTime();
            }
        }

        public string FromDateDisplay
        {
            get
            {
                return FromDate.ToDisplayDateTime();
            }
        }

        public string ToDateDisplay
        {
            get
            {
                return ToDate.ToDisplayDateTime();
            }
        }

        public string TagDisplay
        {
            get
            {
                return FromDate.ToString("H:mm") + " - " + (ToDate.HasValue ? ToDate.Value.ToString("H:mm") : "*");
            }
        }

        public string TagDisplayHours
        {
            get
            {
                DateTime toDate = ToDate.HasValue ? ToDate.Value : DateTime.Now;

                string display = "";

                TimeSpan diff = toDate - FromDate;

                if (diff.Days > 0)
                {
                    display = $"{diff.Days}d";
                }

                if (diff.Hours < 1)
                {
                    if (diff.Minutes < 1)
                    {
                        display += $"{diff.Seconds}s";
                    }
                    else
                    {
                        display += $"{diff.Minutes}m";
                    }
                }
                else
                {
                    display += $"{diff.Hours}h";
                }

                return display;
            }
        }
    }

    public class TimeSheetActivity : TimeSheetActivityBase
    {
        public int TimeSheetTaskId { get; set; }
        public DateTime? DeletedAt { get; set; }

        public string DeletedAtDisplay => DeletedAt.ToDisplayDate();

        public TimeSheetTask TimeSheetTask { get; set; }

        public ProjectTask ProjectTask { get; set; }
        public User User { get; set; }

        public List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        public int TimeSheetActivityId { get; set; }
        public virtual TimeSheetActivity TimeSheetActivity { get; set; }
    }

}