using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.DataContract;
using ProjectTracking.Data.DataAccess;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Models;
using ProjectTracking.Models.Statistics;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ProjectTracking.Data.Methods
{
    public class RequestedPermissionsMethods : IRequestedPermissions
    {
        private ApplicationDbContext _db;
        private IMapper _mapper;
        private IUserMethods _users;
        private INotificationMethods _notification;
        private readonly IDataAccess dataAccess;

        public RequestedPermissionsMethods(ApplicationDbContext db, IMapper mapper, IUserMethods users, INotificationMethods notification, IDataAccess dataAccess)
        {
            _db = db;
            _mapper = mapper;
            _users = users;
            _notification = notification;
            this.dataAccess = dataAccess;
        }

        public int Add(RequestedPermission requestedPermission)
        {
            bool datesExist = _db.RequestedPermissions.Any(k => k.ApplicationUserId == requestedPermission.ApplicationUserId
            &&
            (
                (requestedPermission.FromDate >= k.FromDate && requestedPermission.FromDate <= k.ToDate) ||
                (requestedPermission.ToDate >= k.FromDate && requestedPermission.ToDate <= k.ToDate)
            ));

            if (datesExist)
            {
                return -1;
            }

            int requestPermissionId = AddRequestPermission(requestedPermission);

            bool addedRequestedPermissionStatus =
                AddRequestedPermissionsStatus(requestedPermission.ApplicationUserId, requestPermissionId);

            if (addedRequestedPermissionStatus)
                return requestPermissionId;

            return addedRequestedPermissionStatus == true ? requestPermissionId : 0;

        }

        public List<RequestedPermission> GetPermissionRequests(string userId, int page, int countPerPage, out int totalPages)
        {
            totalPages = 0;


            var result = _db.RequestedPermissions.Include(k => k.Permission).Include(k => k.RequestedPermissionsStatuses)
                                             .ThenInclude(k => k.Superviser).Where(c => c.ApplicationUserId == userId)
                                             .Select(_mapper.Map<RequestedPermission>).OrderByDescending(c => c.FromDate);

            totalPages = result.Count();

            var records = result.Skip(page * countPerPage)
                                             .Take(countPerPage)
                                             .ToList();

            return records;

        }
        public List<RequestedPermissionsStatus> GetSupervisingPermissionRequests(string supervisorId, int page, int countPerPage, out int totalPages)
        {
            totalPages = 0;


            var result = _db.RequestedPermissionsStatuses.Include(k => k.RequestedPermission)
                                                        .ThenInclude(k => k.Permission)
                                                     .Include(k => k.RequestedPermission)
                                                        .ThenInclude(k => k.ApplicationUser).ThenInclude(k => k.Company)

                                                     .Include(k => k.RequestedPermission)
                                                        .ThenInclude(k => k.ApplicationUser).ThenInclude(k => k.Department)
                                                     .Select(_mapper.Map<RequestedPermissionsStatus>)
                                                     .Where(k => k.SuperviserId == supervisorId).OrderByDescending(c => c.RequestedPermission.FromDate);

            totalPages = result.Count();
            var records = result.Skip(page * countPerPage)
                                            .Take(countPerPage)
                                            .ToList();
            return records;

        }
        private bool AddRequestedPermissionsStatus(string userId, int requestPermissionId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            List<string> supervisorsIds = _users.GetSupervisorsIdsIncludingParents(userId, 4);

            List<DataSets.RequestedPermissionsStatus> requestedPermissionsStatuses =
                new List<DataSets.RequestedPermissionsStatus>();

            foreach (string superVisorId in supervisorsIds)
            {

                requestedPermissionsStatuses.Add(_mapper.Map<DataContract.RequestedPermissionsStatus,
                                                 DataSets.RequestedPermissionsStatus>(new DataContract.RequestedPermissionsStatus
                                                 {
                                                     RequestedPermissionId = requestPermissionId,
                                                     SuperviserId = superVisorId,
                                                     IsApproved = RequestedPermissionsStatusCode.Pending
                                                 }));

            }

            _db.RequestedPermissionsStatuses.AddRange(requestedPermissionsStatuses);

            try
            {
                _db.SaveChanges();

                foreach (DataSets.RequestedPermissionsStatus status in requestedPermissionsStatuses)
                {
                    Notification sent = _notification.Send(userId, status.SuperviserId, $"A leave request permission has been requested", NotificationType.Information, true).Result;
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private int AddRequestPermission(RequestedPermission requestedPermission)
        {
            var requestedPersmissionSet = _mapper.Map<RequestedPermission,
                                         DataSets.RequestedPermission>(requestedPermission);
            _db.RequestedPermissions.Add(requestedPersmissionSet);
            _db.SaveChanges();
            requestedPermission.ID = requestedPersmissionSet.ID;
            return requestedPersmissionSet.ID;
        }
        public bool PermitRequestedPermission(PermitModel permitModel)
        {
            if (permitModel == null)
            {
                throw new ArgumentNullException(nameof(permitModel));
            }
            var requestPermissionStatusInDb = _db.RequestedPermissionsStatuses.FirstOrDefault(c => c.Id == permitModel.supervisingPermissionRequestStatusId);
            if (requestPermissionStatusInDb == null)
            {
                throw new ArgumentNullException(nameof(permitModel));
            }
            requestPermissionStatusInDb.IsApproved = _mapper.Map<DataSets.RequestedPermissionsStatusCode>(permitModel.Status);
            requestPermissionStatusInDb.Comments = permitModel.Comment;
            return _db.SaveChanges() > 0;
        }
        public List<RequestedPermission> GetApprovedRequestedPermission(string userId, int page, int countPerPage, out int totalCount, int? year, int? month)
        {
            var userInDb = _db.Users.FirstOrDefault(c => c.Id == userId);
            if (userInDb == null)
                throw new InvalidOperationException();
            string sql = @"select distinct rp.*,p.Title 'PermissionTitle' from RequestedPermissions rp inner join RequestedPermissionsStatuses rps
                            on RequestedPermissionId = rp.ID inner join AspNetUsers users on users.Id = rp.ApplicationUserId
                            inner join [Permissions] p on PermissionId=p.ID                           
                            where rp.id not in (select distinct RequestedPermissionId from RequestedPermissionsStatuses where IsApproved <> 1)
                            and users.Id =@userId ";
            //append year filter
            if (year.HasValue)
                sql += " and Year(FromDate)=@year ";
            if (month.HasValue)
                sql += " and month(FromDate)=@month ";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@userId", userId);
            if (year.HasValue)
                cmd.Parameters.AddWithValue("@year", year);
            if (month.HasValue)
                cmd.Parameters.AddWithValue("@month", month);
            var result = dataAccess.ToObjectList<RequestedPermission>(cmd);
            totalCount = result.Count();


            var records = result.Skip(page * countPerPage)
                                             .Take(countPerPage)
                                             .ToList();

            return records;
        }
        public List<PermissionRequestsStats> GetPermissionRequestsTotals(string userId, int? year, int? month)
        {
            var userInDb = _db.Users.FirstOrDefault(c => c.Id == userId);
            if (userInDb == null)
                throw new InvalidOperationException();
            string sql = @"select sum(DATEDIFF(SECOND,rp.FromDate,rp.ToDate))'Ticks',p.Title 'PermissionTitle' from RequestedPermissions rp inner join RequestedPermissionsStatuses rps
                            on RequestedPermissionId = rp.ID inner join AspNetUsers users on users.Id = rp.ApplicationUserId
                            inner join [Permissions] p on PermissionId=p.ID                           
                            where rp.id not in (select distinct RequestedPermissionId from RequestedPermissionsStatuses where IsApproved <> 1)
                            and users.Id =@userId ";
            //append year filter
            if (year.HasValue)
                sql += " and Year(FromDate)=@year ";
            if (month.HasValue)
                sql += " and month(FromDate)=@month ";
            sql += " group by p.Title";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@userId", userId);
            if (year.HasValue)
                cmd.Parameters.AddWithValue("@year", year);
            if (month.HasValue)
                cmd.Parameters.AddWithValue("@month", month);
            return dataAccess.ToObjectList<PermissionRequestsStats>(cmd);
        }
        public List<int> GetPermissionRequestsYearsOrMonthsByUser(string userId, int? year)
        {
            var userInDb = _db.Users.FirstOrDefault(c => c.Id == userId);
            if (userInDb == null)
                return null;
            string column = year.HasValue == true ? "Month(FromDate)" : "Year(FromDate)";
            string sql = @"select distinct " + column + @" from RequestedPermissions rp inner join RequestedPermissionsStatuses rps
                            on RequestedPermissionId = rp.ID inner join AspNetUsers users on users.Id = rp.ApplicationUserId
                            where rp.id not in (select distinct RequestedPermissionId from RequestedPermissionsStatuses where IsApproved <> 1)
                            and users.Id =@userId ";
            //append year filter
            if (year.HasValue)
                sql += " and Year(FromDate)=@year ";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@userId", userId);
            if (year.HasValue)
                cmd.Parameters.AddWithValue("@year", year);

            DataTable dt = dataAccess.GetDataTable(cmd);

            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows.Cast<DataRow>().Select(k => k[0].ToInt().Value).ToList();
            }

            return new List<int>();

        }
    }
}
