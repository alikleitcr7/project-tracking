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
        List<TimeSheet> GetByUser(string userId);
        List<TimeSheetTask> GetTimeSheetTasks(int timeSheetId);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, DateTime date);
        bool AddTasks(int timeSheetId, List<int> projectIds);
        bool AddTasks(int timeSheetId, int projectId);
        bool RemoveTasks(int timeSheetId, List<int> projectIds);
        bool RemoveTasks(int timeSheetId, int projectId);
        bool Delete(int id);
        bool SignTimeSheet(string userId, int timeSheetId);
        bool PermitTimeSheetStatus(PermitModel permitModel);
        TimeSheet GetLatest(string userId);
        List<TimeSheetTask> GetSubordinatesTimeSheets(string supervisorId, int page, int countPerPage, out int totalPages);
    }
}