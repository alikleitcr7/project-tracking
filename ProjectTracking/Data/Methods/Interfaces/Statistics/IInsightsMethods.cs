using ProjectTracking.Models.Statistics;
using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces.Statistics
{
    public interface IInsightsMethods
    {
        List<UserMonthlyActivities> GetUserMonthlyActivities(string userId, bool includeMonth = true, bool includeDay = false);
    }   
}