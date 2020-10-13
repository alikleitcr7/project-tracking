using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITimeSheetActivitiesMethods
    {
        TimeSheetActivity Add(TimeSheetActivity activity, string ipAddress);
        User GetActivityUser(int id);
        ProjectTask GetActivityProjectTask(int id);
        TimeSheetActivity Get(int id);
        List<TimeSheetActivity> GetByTimeSheet(int timesheetId);
        TimeSheetActivity Update(TimeSheetActivity activity, string ipAddress);
        bool Delete(int id);
    }
}