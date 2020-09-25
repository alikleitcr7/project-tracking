using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IUserMethods
    {
        List<DataContract.UserLog> GetUsersLogs(int page, int countPerPage, string fromDate, string toDate, out int totalPages);
        List<IdentityRole<string>> GetAllRoles();
        UserLog AddStartLog(string userId, string ipAddress, string comments = null);
        List<UserLog> GetActiveLogs();
        List<User> GetEmployees();
        object GetRoles(string Id);
        User GetEmployee(string id);
        User GetProfile(string id);
        List<User> GetTeamMembers(int departmentId);
        List<User> GetAllUsers();
        List<User> GetSubordinatesWithTimeSheets(string superVisorId);
        bool CheckIfEmployeeHasSubordinates(string userId);
        void EndLog(string userId, string comments = null);
        List<User> UsersNotRegisteredTimeSheetActivityToday();
        List<User> UsersNotHavingTimeSheetThisMonthYet();


        List<string> GetSupervisorsIds(string forUserId);
        List<string> GetSupervisorsIdsIncludingParents(string forUserId, int levels);
        List<int> GetSupervisingIds(string forUserId);
        List<string> GetUsersInRole(string role);

        #region SupervisingOperations
        bool AddSupervising(string userId, string superVisedId);
        bool AddSupervising(string userId, List<string> superVisedIds);
        bool RemoveSuperVised(string userId, string superVisedId);
        bool RemoveSuperVised(string userId, List<string> superVisedIds);
        Object GetSupervising(string Id);
        #endregion

        #region SupervisorOperations
        bool AddSupervisors(string userId, List<string> superVisedIds);
        bool RemoveSuperVisors(string userId, List<string> superVisedIds);
        Object GetSupervisors(string Id);
        List<KeyValuePair<string, string>> GetAllUsersKeyValues();
        List<KeyValuePair<string, string>> GetAllUsersExecludeTeamSupervisors(int teamId);
        #endregion
    }
}