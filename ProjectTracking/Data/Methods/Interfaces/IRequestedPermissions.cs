using Microsoft.AspNetCore.Identity;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using ProjectTracking.Models;
using ProjectTracking.Models.Statistics;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IRequestedPermissions
    {
        List<PermissionRequestsStats> GetPermissionRequestsTotals(string userId, int? year, int? month);
        List<int> GetPermissionRequestsYearsOrMonthsByUser(string userId, int? year);
        int Add(RequestedPermission requestedPermission);
        List<RequestedPermission> GetPermissionRequests(string userId, int page, int countPerPage, out int totalCount);
        List<RequestedPermissionsStatus> GetSupervisingPermissionRequests(string supervisorId, int page, int countPerPage, out int totalCount);
        bool PermitRequestedPermission(PermitModel permitModel);
        List<RequestedPermission> GetApprovedRequestedPermission(string userId,  int page,int countPerPage, out int totalCount, int? year, int? month);
    }
}
