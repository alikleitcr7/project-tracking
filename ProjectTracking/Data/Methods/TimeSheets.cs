using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracking.Models;

namespace ProjectTracking.Data.Methods
{
    public class TimeSheets : ITimeSheetsMethods
    {
        private ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly IUserMethods _users;

        public TimeSheets(IMapper mapper, IUserMethods users, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
            _users = users;
        }
        public TimeSheet Add(string userId, DateTime fromDate, DateTime toDate, out string message)
        {
            message = "";

            var userTimeSheets = db.TimeSheets.Where(c => c.UserId == userId
                                                          && ((fromDate >= c.FromDate && fromDate <= c.ToDate)
                                                          || (toDate >= c.FromDate && toDate <= c.ToDate)))
                                              .FirstOrDefault();

            if (userTimeSheets == null)
            {
                DataSets.TimeSheet dbTimeSheet = new DataSets.TimeSheet()
                {
                    FromDate = fromDate,
                    ToDate = toDate,
                    DateAdded = DateTime.Now,
                    UserId = userId
                };

                db.TimeSheets.Add(dbTimeSheet);

                if (db.SaveChanges() > 0)
                {
                    bool addedTimeSheetStatus = AddTimeSheetStatuses(userId, dbTimeSheet.ID);

                    return _mapper.Map<TimeSheet>(dbTimeSheet);
                    //if (addedTimeSheetStatus)
                    //else
                    //{
                    //    db.TimeSheets.Remove(dbTimeSheet);
                    //    message = "something went wrong please try again ";
                    //    return null;
                    //}
                }
                else
                    throw new InvalidOperationException();
            }
            else
            {
                message = $"Dates {fromDate.ToShortDateString()}/{toDate.ToShortDateString()} Fall betweeen user's timesheet dates {userTimeSheets.FromDate.ToShortDateString()}/{userTimeSheets.ToDate.ToShortDateString()}";
                return null;
            }
        }
        private bool AddTimeSheetStatuses(string userId, int timeSheetId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;
            dynamic superVisorModel = _users.GetSupervisors(userId);
            var timeSheetStatuses =
                new List<DataSets.TimeSheetStatus>();

            foreach (string superVisorId in superVisorModel.SuperVise)
            {
                timeSheetStatuses.Add(_mapper.Map<TimeSheetStatus,
                                                 DataSets.TimeSheetStatus>(new TimeSheetStatus
                                                 {
                                                     TimeSheetId = timeSheetId,
                                                     SuperviserId = superVisorId,
                                                     //IsApproved = RequestedPermissionsStatusCode.Pending
                                                 }));
            }

            db.TimeSheetStatuses.AddRange(timeSheetStatuses);

            return db.SaveChanges() > 0;
        }
        public bool PermitTimeSheetStatus(PermitModel permitModel)
        {
            if (permitModel == null)
            {
                throw new ArgumentNullException(nameof(permitModel));
            }

            var timeSheetStatusInDb = db.TimeSheetStatuses.FirstOrDefault(c => c.ID == int.Parse(permitModel.supervisingPermissionRequestStatusId));

            if (timeSheetStatusInDb == null)
            {
                throw new ArgumentNullException(nameof(permitModel));
            }

            //timeSheetStatusInDb.IsApproved = _mapper.Map<RequestedPermissionsStatusCode,
            //                                            DataSets.RequestedPermissionsStatusCode>(permitModel.Status);
            timeSheetStatusInDb.Comments = permitModel.Comment;

            return db.SaveChanges() > 0;
        }

        public List<TimeSheetStatus> GetSubordinatesTimeSheets(string supervisorId, int page, int countPerPage, out int totalPages)
        {
            totalPages = 0;
            //var result = db.TimeSheetStatuses.Include(k => k.TimeSheet)
            //                                      .ThenInclude(k => k.User)
            //                                      .ThenInclude(k => k.Company)
            //                                  .Include(k => k.TimeSheet)
            //                                      .ThenInclude(k => k.User)
            //                                      .ThenInclude(k => k.Department)
            //                                  .Select(_mapper.Map<TimeSheetStatus>)
            //                                   .Where(k => k.SuperviserId == supervisorId)
            //                                  .OrderByDescending(c => c.TimeSheet.FromDate); 

            // removed company
            var result = db.TimeSheetStatuses.Include(k => k.TimeSheet)
                                                  .ThenInclude(k => k.User)
                                              .Include(k => k.TimeSheet)
                                                  .ThenInclude(k => k.User)
                                                  .ThenInclude(k => k.Team)
                                              .Select(_mapper.Map<TimeSheetStatus>)
                                              .Where(k => k.SuperviserId == supervisorId)
                                              .OrderByDescending(c => c.TimeSheet.FromDate);

            totalPages = result.Count();

            var records = result.Skip(page * countPerPage)
                                .Take(countPerPage)
                                .ToList();
            return records;
        }
        public TimeSheet GetLatest(string userId)
        {
            var user = db.Users.FirstOrDefault(c => c.Id == userId);
            if (user == null)
                return null;
            var dbTimeSheetLatest = db.TimeSheets.OrderByDescending(c => c.ToDate)
                                                 .FirstOrDefault(c => c.UserId == userId);
            return dbTimeSheetLatest == null ? null : _mapper.Map<TimeSheet>(dbTimeSheetLatest);

        }
        public TimeSheet Add(string userId, DateTime fromDate, DateTime toDate)
        {
            DataSets.TimeSheet dbTimeSheet = new DataSets.TimeSheet()
            {
                FromDate = fromDate,
                ToDate = toDate,
                DateAdded = DateTime.Now,
                UserId = userId
            };

            db.TimeSheets.Add(dbTimeSheet);

            bool saved = db.SaveChanges() == 1;

            return saved ? _mapper.Map<TimeSheet>(dbTimeSheet) : null;
        }
        public List<TimeSheet> GetByUser(string userId)
        {
            var dbTimeSheets = db.TimeSheets
                                 .Include(k => k.TimeSheetStatuses)
                                 .ThenInclude(k => k.Superviser)
                                 .Include(k => k.TimeSheetProjects)
                                 .Where(k => k.UserId == userId);
            var parsedTimeSheets = dbTimeSheets.Select(_mapper.Map<TimeSheet>)
                                               .ToList();
            return parsedTimeSheets.OrderByDescending(k => k.FromDate).ToList();
        }
        public List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, DateTime date)
        {
            var dbTimeSheetActivities = db.TimeSheetActivities
                .Include(k => k.ProjectFile)
                .Include(k => k.MeasurementUnit)
                .Include(k => k.TypeOfWork)
                .Include(k => k.TimeSheetProject)
                .Where(k => k.TimeSheetProject != null && k.TimeSheetProject.TimeSheetId == timeSheetId
                            && k.FromDate.Month == date.Month
                            && k.FromDate.Day == date.Day
                            && k.FromDate.Year == date.Year).ToList();

            if (dbTimeSheetActivities.Count == 0)
            {
                return new List<TimeSheetActivity>();
            }

            List<TimeSheetActivity> activities = dbTimeSheetActivities.Select(_mapper.Map<TimeSheetActivity>).ToList();

            TimeSheetActivitiesMethods.PopulateIpAddresses(activities, db.IpAddresses.ToList());

            return activities;
        }
        public List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId)
        {
            var dbTimeSheetActivities = db.TimeSheetActivities
                .Include(k => k.TimeSheetProject)
                .Where(k => k.TimeSheetProject != null && k.TimeSheetProject.TimeSheetId == timeSheetId)
                .ToList();

            if (dbTimeSheetActivities.Count == 0)
            {
                return new List<TimeSheetActivity>();
            }

            return dbTimeSheetActivities.Select(_mapper.Map<TimeSheetActivity>).ToList();
        }
        public List<TimeSheetProject> GetTimeSheetProjects(int timeSheetId)
        {
            var dbTimeSheetProjects = db.TimeSheetProjects.Include(k => k.Activities).Where(k => k.TimeSheetId == timeSheetId).ToList();

            if (dbTimeSheetProjects.Count == 0)
            {
                return new List<TimeSheetProject>();
            }

            return dbTimeSheetProjects.Select(_mapper.Map<TimeSheetProject>).ToList();
        }
        public bool AddProjects(int timeSheetId, int projectId)
        {
            return AddProjects(timeSheetId, new List<int>() { projectId });
        }
        public bool AddProjects(int timeSheetId, List<int> projectIds)
        {
            DataSets.TimeSheet dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == timeSheetId);

            if (dbTimeSheet == null)
                return false;
            List<DataSets.TimeSheetProject> existingProjects = db.TimeSheetProjects.Where(k => k.TimeSheetId == timeSheetId).ToList();
            projectIds = projectIds.Where(p => !existingProjects.Any(k => k.ProjectId == p))
                                   .ToList();
            foreach (int id in projectIds)
            {
                db.TimeSheetProjects.Add(new DataSets.TimeSheetProject()
                {
                    ProjectId = id,
                    TimeSheetId = timeSheetId
                });
            }

            return db.SaveChanges() > 0;
        }
        public bool RemoveProjects(int timeSheetId, int projectId)
        {
            return RemoveProjects(timeSheetId, new List<int>() { projectId });
        }
        public bool RemoveProjects(int timeSheetId, List<int> projectIds)
        {
            DataSets.TimeSheet dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == timeSheetId);

            if (dbTimeSheet == null)
                return false;

            List<DataSets.TimeSheetProject> dbTsProjects = db.TimeSheetProjects.Include(k => k.Activities)
                .Where(k => projectIds.Contains(k.ProjectId) && k.TimeSheetId == timeSheetId && k.Activities.Count == 0)
                .ToList();

            db.TimeSheetProjects.RemoveRange(dbTsProjects);

            return db.SaveChanges() > 0;
        }
        public bool SignTimeSheet(string userId, int timeSheetId)
        {
            DataSets.TimeSheet timesheet = db.TimeSheets.FirstOrDefault(k => k.UserId == userId && k.ID == timeSheetId);

            if (timesheet == null)
            {
                return false;
            }

            timesheet.IsSigned = true;

            try
            {
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public TimeSheet Get(int id, out List<Project> projects, bool includeActivites = true)
        {
            projects = new List<Project>();

            DataSets.TimeSheet dbTimeSheet = null;

            if (includeActivites)
            {
                dbTimeSheet = db.TimeSheets.Include(k => k.TimeSheetStatuses)
                                .Include(k => k.TimeSheetProjects)
                                .ThenInclude(x => x.Activities)
                                .Include(k => k.TimeSheetProjects)
                                .ThenInclude(x => x.Project)
                                .ThenInclude(x => x.Parent)
                                .FirstOrDefault(k => k.ID == id);
            }
            else
            {
                dbTimeSheet = db.TimeSheets.Include(k => k.TimeSheetStatuses)
                               .Include(k => k.TimeSheetProjects)
                               .ThenInclude(x => x.Project)
                               .ThenInclude(x => x.Parent)
                               .FirstOrDefault(k => k.ID == id);
            }


            if (dbTimeSheet == null)
                return null;

            foreach (var tsProject in dbTimeSheet.TimeSheetProjects)
            {
                var parent = tsProject.Project.Parent;

                if (!projects.Any(k => k.ID == parent.ID))
                {
                    projects.Add(_mapper.Map<Project>(parent));
                }
            }

            return _mapper.Map<TimeSheet>(dbTimeSheet);
        }
        public bool Delete(int id)
        {
            var dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == id);

            if (dbTimeSheet != null)
            {
                var timesheets = db.TimeSheetStatuses.Where(k => k.TimeSheetId == id);
                var timesheetProjects = db.TimeSheetProjects.Where(k => k.TimeSheetId == id);

                db.TimeSheetStatuses.RemoveRange(timesheets);
                db.TimeSheetProjects.RemoveRange(timesheetProjects);

                foreach (var item in timesheetProjects)
                {
                    var tsActivities = db.TimeSheetActivities.Where(k => k.TimeSheetProjectId == item.ID);
                    db.TimeSheetActivities.RemoveRange(tsActivities);
                }

                db.TimeSheets.Remove(dbTimeSheet);

                return db.SaveChanges() > 0;
            }


            return false;
        }


    }
}
