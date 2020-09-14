using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITimeSheetActivityLogsMethods
    {
        TimeSheetActivityLog Add(TimeSheetActivityLog activity);
        TimeSheetActivityLog Get(int id);
        TimeSheetActivity GetActivity(int id);
        List<TimeSheetActivityLog> GetByActivity(int activityId);
        bool Delete(int id);
        bool Clear(int activityId);
    }
}