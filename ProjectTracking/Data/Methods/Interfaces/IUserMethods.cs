using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IUserMethods
    {
        List<DataContract.UserLog> GetUsersLogs(List<string> userIds, int page, int countPerPage, DateTime? fromDate, DateTime? toDate, out int totalCount);
        void EndLog(string userId, UserLogStatus status);


        List<ApplicationIdentityRole> GetAllRoles();
        //UserLog AddStartLog(string userId, string ipAddress, string comments = null);
        List<UserLog> GetActiveLogs();
        List<User> GetEmployees();
        object GetRoles(string Id);
        User GetEmployee(string id);
        User GetProfile(string id);
        List<User> GetTeamMembers(int departmentId);
        List<User> GetAllUsers();
        List<User> GetSubordinatesWithTimeSheets(string superVisorId);
        bool CheckIfEmployeeHasSubordinates(string userId);
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
        User GetById(string id);
        void Delete(string id);
        List<User> Search(string keyword, int page, int countPerPage, out int totalCount);
        User Save(Models.Users.UserSaveModel model);
        void AssignTeamSupervisor(string assignedById, string userId, int teamId);
        bool IsSupervisor(string userId);
        List<string> SupervisingUsers(string supervisorId);
        List<User> GetUsersByRole(int roleCode);
        List<KeyValuePair<string, string>> GetUsersByRoleKeyValue(int roleCode);
        void SetRole(string userId, short roleCode);
        #endregion
    }
}