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
    public class TimeSheetsMethods : ITimeSheetsMethods
    {
        private ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly IUserMethods _users;

        public TimeSheetsMethods(IMapper mapper, IUserMethods users, ApplicationDbContext context)
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
                    //bool addedTimeSheetStatus = AddTimeSheetStatuses(userId, dbTimeSheet.ID);

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
            throw new NotImplementedException();

        }
        public bool PermitTimeSheetStatus(PermitModel permitModel)
        {
            throw new NotImplementedException();
        }

        public List<TimeSheetTask> GetSubordinatesTimeSheets(string supervisorId, int page, int countPerPage, out int totalPages)
        {
            throw new NotImplementedException();
            //totalPages = 0;
            ////var result = db.TimeSheetStatuses.Include(k => k.TimeSheet)
            ////                                      .ThenInclude(k => k.User)
            ////                                      .ThenInclude(k => k.Company)
            ////                                  .Include(k => k.TimeSheet)
            ////                                      .ThenInclude(k => k.User)
            ////                                      .ThenInclude(k => k.Department)
            ////                                  .Select(_mapper.Map<TimeSheetStatus>)
            ////                                   .Where(k => k.SuperviserId == supervisorId)
            ////                                  .OrderByDescending(c => c.TimeSheet.FromDate); 

            //// removed company
            //var result = db.TimeSheetStatuses.Include(k => k.TimeSheet)
            //                                      .ThenInclude(k => k.User)
            //                                  .Include(k => k.TimeSheet)
            //                                      .ThenInclude(k => k.User)
            //                                      .ThenInclude(k => k.Team)
            //                                  .Select(_mapper.Map<TimeSheetStatus>)
            //                                  .Where(k => k.SuperviserId == supervisorId)
            //                                  .OrderByDescending(c => c.TimeSheet.FromDate);

            //totalPages = result.Count();

            //var records = result.Skip(page * countPerPage)
            //                    .Take(countPerPage)
            //                    .ToList();
            //return records;
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
                                 .Include(k => k.TimeSheetTasks)
                                 .Where(k => k.UserId == userId);

            var parsedTimeSheets = dbTimeSheets.Select(_mapper.Map<TimeSheet>)
                                               .ToList();

            return parsedTimeSheets.OrderByDescending(k => k.FromDate).ToList();
        }
        public List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, DateTime date)
        {
            var dbTimeSheetActivities = db.TimeSheetActivities
                .Include(k => k.TimeSheetTask)
                .Where(k => k.TimeSheetTask != null && k.TimeSheetTask.TimeSheetId == timeSheetId
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
                .Include(k => k.TimeSheetTask)
                .Where(k => k.TimeSheetTask != null && k.TimeSheetTask.TimeSheetId == timeSheetId)
                .ToList();

            if (dbTimeSheetActivities.Count == 0)
            {
                return new List<TimeSheetActivity>();
            }

            return dbTimeSheetActivities.Select(_mapper.Map<TimeSheetActivity>).ToList();
        }

        public List<TimeSheetTask> GetTimeSheetTasks(int timeSheetId)
        {
            var dbTimeSheetProjects = db.TimeSheetTasks.Include(k => k.Activities).Where(k => k.TimeSheetId == timeSheetId).ToList();

            if (dbTimeSheetProjects.Count == 0)
            {
                return new List<TimeSheetTask>();
            }

            return dbTimeSheetProjects.Select(_mapper.Map<TimeSheetTask>).ToList();
        }

        public bool AddTasks(int timeSheetId, int projectId)
        {
            return AddTasks(timeSheetId, new List<int>() { projectId });
        }

        public bool AddTasks(int timeSheetId, List<int> tasksIds)
        {
            DataSets.TimeSheet dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == timeSheetId);

            // get timesheet
            if (dbTimeSheet == null)
                return false;

            // get existing projects
            List<DataSets.TimeSheetTask> existingTasks = db.TimeSheetTasks
                .Where(k => k.TimeSheetId == timeSheetId)
                .ToList();

            // remove already exist
            tasksIds = tasksIds.Where(p => !existingTasks.Any(k => k.ProjectTaskId == p))
                                   .ToList();

            // add 
            foreach (int id in tasksIds)
            {
                db.TimeSheetTasks.Add(new DataSets.TimeSheetTask()
                {
                    ProjectTaskId = id,
                    TimeSheetId = timeSheetId
                });
            }

            return db.SaveChanges() > 0;
        }
        public bool RemoveTasks(int timeSheetId, int projectId)
        {
            return RemoveTasks(timeSheetId, new List<int>() { projectId });
        }
        public bool RemoveTasks(int timeSheetId, List<int> projectIds)
        {
            DataSets.TimeSheet dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == timeSheetId);

            if (dbTimeSheet == null)
                return false;

            List<DataSets.TimeSheetTask> dbTsProjects = db.TimeSheetTasks.Include(k => k.Activities)
                .Where(k => projectIds.Contains(k.ProjectTaskId) && k.TimeSheetId == timeSheetId && k.Activities.Count == 0)
                .ToList();

            db.TimeSheetTasks.RemoveRange(dbTsProjects);

            return db.SaveChanges() > 0;
        }
        
        public TimeSheet Get(int id, out List<Project> projects, bool includeActivites = true)
        {
            projects = new List<Project>();

            DataSets.TimeSheet dbTimeSheet = null;

            if (includeActivites)
            {
                dbTimeSheet = db.TimeSheets
                                .Include(k => k.TimeSheetTasks)
                                .ThenInclude(x => x.Activities)
                                .Include(k => k.TimeSheetTasks)
                                .ThenInclude(x => x.ProjectTask)
                                .ThenInclude(x => x.Project)
                                .FirstOrDefault(k => k.ID == id);
            }
            else
            {
                dbTimeSheet = db.TimeSheets
                               .Include(k => k.TimeSheetTasks)
                               .ThenInclude(x => x.ProjectTask)
                               .ThenInclude(x => x.Project)
                               .FirstOrDefault(k => k.ID == id);
            }


            if (dbTimeSheet == null)
                return null;

            foreach (var tsProject in dbTimeSheet.TimeSheetTasks)
            {
                var parent = tsProject.ProjectTask.Project;

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
                var tasks = db.TimeSheetTasks.Where(k => k.TimeSheetId == id);

                db.TimeSheetTasks.RemoveRange(tasks);

                foreach (var task in tasks)
                {
                    var taskActivities = db.TimeSheetActivities.Where(k => k.TimeSheetTaskId == task.ID);
                    db.TimeSheetActivities.RemoveRange(taskActivities);
                }

                db.TimeSheets.Remove(dbTimeSheet);

                return db.SaveChanges() > 0;
            }


            return false;
        }

        public bool SignTimeSheet(string userId, int timeSheetId)
        {
            throw new NotImplementedException();
        }
    }
}
