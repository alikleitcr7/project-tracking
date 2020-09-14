using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTracking.DataContract
{
    public class TimeSheetActivityBase
    {
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Comment { get; set; }
        public int? Number { get; set; }
        public string IpAddress { get; set; }
        public string IpAddressTitle { get; set; }


        public string FromDateDisplay
        {
            get
            {
                return FromDate.ToString();
            }
        }

        public string ToDateDisplay
        {
            get
            {
                return ToDate.HasValue ? ToDate.Value.ToString() : null;
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

        public string TypeOfWorkDisplay
        {
            get
            {
                return TypeOfWork == null ? "-" : TypeOfWork.Name;
            }
        }
        public string MeasurementUnitDisplay
        {
            get
            {
                return MeasurementUnit == null ? "-" : MeasurementUnit.Name;
            }
        }

        public string ProjectFileDisplay
        {
            get
            {
                return ProjectFile == null ? "-" : ProjectFile.Name;
            }
        }

        //public int TimeSheetId { get; set; }
        public int TimeSheetProjectId { get; set; }
        public int? TypeOfWorkId { get; set; }
        public int? MeasurementUnitId { get; set; }
        public int? ProjectFileId { get; set; }



        public virtual ProjectFile ProjectFile { get; set; }
        public virtual TypeOfWork TypeOfWork { get; set; }
        public virtual MeasurementUnit MeasurementUnit { get; set; }


    }
    public class TimeSheetActivity : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public virtual TimeSheetProject TimeSheetProject { get; set; }

        public virtual List<TimeSheetActivityLog> TimeSheetActivityLogs { get; set; }
    }

    public class TimeSheetActivityLog : TimeSheetActivityBase
    {
        public int ID { get; set; }
        public int TimeSheetActivityId { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual TimeSheetActivity TimeSheetActivity { get; set; }
    }

}