using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITimeSheetActivitiesMethods
    {
        TimeSheetActivity Add(TimeSheetActivity activity);
        User GetActivityUser(int id);
        Project GetActivityProject(int id);
        TimeSheetActivity Get(int id);
        List<TimeSheetActivity> GetByTimeSheet(int timesheetId);
        TimeSheetActivity Update(TimeSheetActivity activity);
        bool Delete(int id);
    }
}