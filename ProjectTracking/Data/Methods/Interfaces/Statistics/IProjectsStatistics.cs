using ProjectTracking.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods.Interfaces.Statistics
{
    public interface IProjectsStatistics
    {
        List<ProjectsProgress> GetProjectsProgress(bool byYear, bool byYearAndMonth,string userId=null);
        List<MeasurementUnitsProgressTotal> GetMeasurementUnitsTotalProgress(string userId, string year, string month, string day);
        List<int> GetProjectProgressYears();
        List<int> GetProjectProgressYearsByUser(string userId);
        List<int> GetProjectProgressMonthsByYear(int year);
        List<int> GetProjectProgressMonthsByYearAndUser(string userId, int year);
        List<int> GetProjectProgressDaysByMonthAndYear(int year, int month);
        List<int> GetProjectProgressDaysByMonthAndYearAndUser(string userId, int year, int month);
    }
}
