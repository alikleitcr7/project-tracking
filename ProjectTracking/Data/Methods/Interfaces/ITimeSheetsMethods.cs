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
        List<TimeSheetProject> GetTimeSheetProjects(int timeSheetId);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId);
        List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, DateTime date);
        bool AddProjects(int timeSheetId, List<int> projectIds);
        bool AddProjects(int timeSheetId, int projectId);
        bool RemoveProjects(int timeSheetId, List<int> projectIds);
        bool RemoveProjects(int timeSheetId, int projectId);
        bool Delete(int id);
        bool SignTimeSheet(string userId, int timeSheetId);
        bool PermitTimeSheetStatus(PermitModel permitModel);
        TimeSheet GetLatest(string userId);
        List<TimeSheetStatus> GetSubordinatesTimeSheets(string supervisorId, int page, int countPerPage, out int totalPages);
    }
}