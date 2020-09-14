using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Models.TimeSheet
{
    public class TimeSheetModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string EmployeeName { get; set; } = "";
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<HeaderMonth> Months { get; set; }
        public int NumberOfDays { get; set; }

        public List<Project> Projects { get; set; }

        public class HeaderMonth
        {
            public int MonthDay { get; set; }
            public int Month { get; set; }
            public bool IsHighlighted { get; set; }
        }

        public class Project
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public List<Activity> Activities { get; set; }
        }

        public class Activity
        {
            public int ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }

            public List<ActivityByMonth> MonthActivities { get; set; }
        }

        public class ActivityByMonth
        {
            public int MonthIndex { get; set; }
            public string NumberOfHours { get; set; }
        }

        public static IEnumerable<HeaderMonth> MonthsDays(
           DateTime startDate,
           DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            iterator = new DateTime(startDate.Year, startDate.Month, startDate.Day);

            if (endDate > startDate)
            {
                limit = endDate;
            }
            else
            {
                limit = startDate;
            }


            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

            while (iterator <= limit)
            {
                //yield return Tuple.Create(
                //    dateTimeFormat.GetMonthName(iterator.Month),
                //    iterator.Year);

                var headerMonth = new HeaderMonth()
                {
                    IsHighlighted = iterator.DayOfWeek == DayOfWeek.Saturday || iterator.DayOfWeek == DayOfWeek.Sunday,
                    Month = iterator.Month,
                    MonthDay = iterator.Day
                };

                yield return headerMonth;
                //yield return iterator.ToString("dd/MM");

                iterator = iterator.AddDays(1);
            }
        }

        public static TimeSheetModel GenerateModel(DataContract.TimeSheet ts, List<DataContract.Project> parents)
        {
            TimeSheetModel model = new TimeSheetModel()
            {
                FromDate = ts.FromDate.ToShortDateString(),
                ToDate = ts.ToDate.ToShortDateString(),
                ID = ts.ID,
                NumberOfDays = (ts.ToDate - ts.FromDate).Days,
                EmployeeName = "",
                Title = ts.ToDate.ToString("MMMM yyyy"),
                Months = MonthsDays(ts.FromDate, ts.ToDate).ToList()
            };

            model.Projects = new List<Project>();

            foreach (DataContract.Project parent in parents)
            {
                //  ##### PROJECT PARSER ##### //

                Project parsedProject = new Project()
                {
                    ID = parent.ID,
                    Description = parent.Description,
                    Title = parent.Title
                };

                parsedProject.Activities = new List<Activity>();

                List<DataContract.TimeSheetProject> timeSheetProjects = ts.TimeSheetProjects.Where(k => k.Project.ParentId == parent.ID).ToList();

                foreach (DataContract.TimeSheetProject tsproject in timeSheetProjects)
                {
                    //  ##### ACTIVITY PARSER ##### //

                    Activity parsedActivity = new Activity()
                    {
                        ID = tsproject.Project.ID,
                        Title = tsproject.Project.Title,
                        Description = tsproject.Project.Description,
                        MonthActivities = new List<ActivityByMonth>()
                    };

                    List<DataContract.TimeSheetActivity> sheetActivities = tsproject.Activities.Where(k => k.ToDate.HasValue).ToList();


                    //  ##### MONTH ACTIVITIES ##### //

                    // accumulated days
                    DateTime accDate = ts.FromDate;

                    int startMonthDay = accDate.Day;
                    int startMonth = accDate.Month;
                    int startYear = accDate.Year;

                    for (int i = 0; i < model.NumberOfDays + 1; i++)
                    {
                        List<DataContract.TimeSheetActivity> monthActivities = sheetActivities.Where(k => k.FromDate.Day == accDate.Day
                             && k.FromDate.Month == accDate.Month
                             && k.FromDate.Year == accDate.Year)
                            .ToList();

                        string nbHoursDisplay = "";
                        double nbHours = monthActivities.Select(k => (k.ToDate.Value - k.FromDate).TotalHours).ToList().Sum();
                        nbHoursDisplay = nbHours.ToString("0.##");

                        //if (nbHours < 1)
                        //{
                        //    int nbMins = monthActivities.Select(k => (k.ToDate.Value - k.FromDate).Minutes).ToList().Sum();

                        //    if (nbMins < 1)
                        //    {
                        //        int nbSec = monthActivities.Select(k => (k.ToDate.Value - k.FromDate).Seconds).ToList().Sum();
                        //        nbHoursDisplay = nbSec == 0 ? "0" : nbSec.ToString() + "s";
                        //    }
                        //    else
                        //    {
                        //        nbHoursDisplay = nbMins.ToString() + "m";
                        //    }
                        //}
                        //else
                        //{
                        //    nbHoursDisplay = nbHours.ToString();
                        //}




                        // remove from ts activities 
                        foreach (var item in monthActivities)
                        {
                            sheetActivities.Remove(item);
                        }

                        ActivityByMonth monthActivity = new ActivityByMonth()
                        {
                            MonthIndex = i,
                            NumberOfHours = nbHoursDisplay
                        };

                        accDate = accDate.AddDays(1);
                        parsedActivity.MonthActivities.Add(monthActivity);
                    }

                    parsedProject.Activities.Add(parsedActivity);
                }

                model.Projects.Add(parsedProject);
            }

            return model;
        }
    }
}
