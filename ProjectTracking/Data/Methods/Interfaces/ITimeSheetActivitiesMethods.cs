using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using ProjectTracking.Models.TimeSheet;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITimeSheetActivitiesMethods
    {
        //TimeSheetActivity Add(TimeSheetActivity activity, string ipAddress);
        User GetActivityUser(int id);
        ProjectTask GetActivityProjectTask(int id);
        TimeSheetActivity Get(int id);
        List<TimeSheetActivity> GetByTimeSheet(int timesheetId);
        //TimeSheetActivity Update(TimeSheetActivity activity, string ipAddress);
        void Delete(int id);

        TimeSheetActivity Start(int timeSheetTaskId, string ipAddress);
        TimeSheetActivity Stop(TimeSheetActivityStopModel model);
        TimeSheetActivity Update(TimeSheetActivityUpdateModel model);
        TimeSheetActivity GetUserActiveActivity(string userId);
    }
}