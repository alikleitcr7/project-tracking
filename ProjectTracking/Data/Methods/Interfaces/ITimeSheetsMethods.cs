using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using ProjectTracking.Models;


namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITimeSheetsMethods
    {
        TimeSheet Add(string userId, DateTime fromDate, DateTime toDate, out string message);
        TimeSheet Add(string userId, DateTime fromDate, DateTime toDate);
        TimeSheet Get(int id, out List<Project> projects, bool includeActivities = true);
        TimeSheet GetLatest(string userId);
        List<int> GetTimeSheetYears(string userId);
        List<TimeSheet> GetByUser(string userId, int? year = null, bool includeTasks = true);
        List<TimeSheetTask> GetTimeSheetTasks(int timeSheetId, bool includeActivitie = true);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, int? taskId, DateTime date, bool includeDeleted);
        bool AddTasks(int timeSheetId, List<int> projectIds);
        bool AddTasks(int timeSheetId, int projectId);
        bool RemoveTasks(int timeSheetId, List<int> projectIds);
        bool RemoveTasks(int timeSheetId, int projectId);
        bool Delete(int id);
        TimeSheet Save(Models.TimeSheet.TimeSheetSaveModel model);
        TimeSheet GetById(int id);
        List<Models.Profile.TimeSheetTasksWithActivityCheck> TimeSheetTasksWithActivityCheck(int timeSheetId);
        List<Project> GetTimeSheetProjectsWithTasks(int timeSheetId);
        bool UserHasTimeSheet(string userId);
    }
}