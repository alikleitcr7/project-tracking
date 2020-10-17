using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracking.Models;
using ProjectTracking.Models.TimeSheet;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Profile;

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


        public TimeSheet Save(TimeSheetSaveModel model)
        {
            IQueryable<DataSets.TimeSheet> conflictQuery = db.TimeSheets.Where(c => c.UserId == model.userId
                                                         && ((model.fromDate >= c.FromDate && model.fromDate <= c.ToDate)
                                                         || (model.toDate >= c.FromDate && model.toDate <= c.ToDate)));

            void ensureNoConflict(DataSets.TimeSheet ts)
            {
                if (ts != null)
                {
                    throw new ClientException($"Dates {model.fromDate.ToShortDateString()}/{model.toDate.ToShortDateString()} Fall betweeen user's timesheet dates {ts.FromDate.ToShortDateString()}/{ts.ToDate.ToShortDateString()}");
                }
            }


            if (model.id.HasValue)
            {
                var possibleConflictTimeSheet = conflictQuery.Where(k => k.ID != model.id.Value).FirstOrDefault();

                ensureNoConflict(possibleConflictTimeSheet);

                // # save timeSheet #

                // get timeSheet

                var dbTimeSheet = db.TimeSheets.FirstOrDefault(k => k.ID == model.id.Value);

                if (dbTimeSheet == null)
                {
                    throw new ClientException("record not found");
                }

                // update values

                dbTimeSheet.FromDate = model.fromDate;
                dbTimeSheet.ToDate = model.toDate;

                db.SaveChanges();

                return _mapper.Map<TimeSheet>(dbTimeSheet);
            }
            else
            {
                var possibleConflictTimeSheet = conflictQuery.FirstOrDefault();

                ensureNoConflict(possibleConflictTimeSheet);

                // # new timesheet #

                DataSets.TimeSheet dbTimeSheet = new DataSets.TimeSheet()
                {
                    UserId = model.userId,
                    FromDate = model.fromDate,
                    ToDate = model.toDate,
                    DateAdded = DateTime.Now,
                    AddedByUserId = model.GetAddedByUser()
                };

                // add the timeSheet
                db.TimeSheets.Add(dbTimeSheet);

                // save changes on teamstimeSheets
                db.SaveChanges();

                return _mapper.Map<TimeSheet>(dbTimeSheet);
            }


            //DataSets.TimeSheet dbTimeSheet = new DataSets.TimeSheet()
            //{
            //    FromDate = fromDate,
            //    ToDate = toDate,
            //    DateAdded = DateTime.Now,
            //    UserId = userId
            //};

            //db.TimeSheets.Add(dbTimeSheet);

            //if (db.SaveChanges() > 0)
            //{
            //    return _mapper.Map<TimeSheet>(dbTimeSheet);
            //}
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

        public TimeSheet GetById(int id)
        {
            var record = db.TimeSheets.FirstOrDefault(k => k.ID == id);

            return record != null ? _mapper.Map<TimeSheet>(record) : null;
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

        public TimeSheet GetLatest(string userId)
        {
            var user = db.Users.FirstOrDefault(c => c.Id == userId);
            if (user == null)
                return null;
            var dbTimeSheetLatest = db.TimeSheets.OrderByDescending(c => c.ToDate)
                                                 .FirstOrDefault(c => c.UserId == userId);
            return dbTimeSheetLatest == null ? null : _mapper.Map<TimeSheet>(dbTimeSheetLatest);

        }

        public List<TimeSheet> GetByUser(string userId, int? year = null, bool includeTasks = true)
        {
            IQueryable<DataSets.TimeSheet> dbTimeSheets = db.TimeSheets
                                 .Where(k => k.UserId == userId)
                                 .OrderByDescending(k => k.FromDate);

            if (includeTasks)
            {
                dbTimeSheets = dbTimeSheets.Include(k => k.TimeSheetTasks);
            }

            if (year.HasValue)
            {
                dbTimeSheets = dbTimeSheets.Where(k => k.FromDate.Year == year);
            }

            return dbTimeSheets.Select(k => new TimeSheet()
            {
                ID = k.ID,
                DateAdded = k.DateAdded,
                FromDate = k.FromDate,
                ToDate = k.ToDate,
                UserId = k.UserId,
                TimeSheetTasks = includeTasks ? k.TimeSheetTasks.Select(t => new TimeSheetTask()
                {
                    ID = t.ID,
                    ProjectTaskId = t.ProjectTaskId,
                    TimeSheetId = t.TimeSheetId
                }).ToList() : null
            }).ToList();
        }

        public List<TimeSheetActivity> GetTimeSheetActivities(int timeSheetId, int? taskId, DateTime date)
        {
            var dbTimeSheetActivities = db.TimeSheetActivities
                .Include(k => k.TimeSheetTask)
                .Where(k => k.TimeSheetTask != null && k.TimeSheetTask.ProjectTaskId == taskId
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

        public List<TimeSheetTask> GetTimeSheetTasks(int timeSheetId, bool includeActivities = true)
        {
            IQueryable<DataSets.TimeSheetTask> query = db.TimeSheetTasks.AsNoTracking()
                .Where(k => k.TimeSheetId == timeSheetId);


            if (includeActivities)
            {
                query = query.Include(k => k.Activities);
            }

            // result truncated (when array orders is empty)
            return query.Select(_mapper.Map<TimeSheetTask>).ToList();

            //// without automapper, also truncated
            //return query.Select(k => new TimeSheetTask()
            //{
            //    ID = k.ID,
            //    ProjectTaskId = k.ProjectTaskId,
            //    TimeSheetId = k.TimeSheetId,
            //    Activities = k.Activities.Select(a => new TimeSheetActivity() { ID = a.ID })
            //    .ToList()
            //}).ToList();


            // WORKS, not getting truncated 
            // since i did not include the orders
            //return query.Select(k => new TimeSheetTask()
            //{
            //    ID = k.ID,
            //    ProjectTaskId = k.ProjectTaskId,
            //    TimeSheetId = k.TimeSheetId,
            //}).ToList();
        }

        public List<TimeSheetTasksWithActivityCheck> TimeSheetTasksWithActivityCheck(int timeSheetId)
        {
            return db.TimeSheetTasks
                .AsNoTracking()
                .Where(k => k.TimeSheetId == timeSheetId)
                .Select(k => new TimeSheetTasksWithActivityCheck()
                {
                    TaskId = k.ProjectTaskId,
                    HasActivity = k.Activities.Any(),
                })
                .ToList();
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

        public List<int> GetTimeSheetYears(string userId)
        {
            return db.TimeSheets
                                 .Include(k => k.TimeSheetTasks)
                                 .Where(k => k.UserId == userId)
                                 .Select(k => k.FromDate.Year)
                                 .Distinct()
                                 .ToList();
        }

        public List<Project> GetTimeSheetProjectsWithTasks(int timeSheetId)
        {
            //List<Project> projects = new List<Project>();
            List<Project> projects = new List<Project>();

            // get timesheet tasks
            //List<ProjectTask> tasks = db.ProjectTasks
            //    .AsNoTracking()
            //    .Where(k => k.TimeSheetTasks.Any(t => t.TimeSheetId == timeSheetId))
            //    .Select(_mapper.Map<ProjectTask>)
            //    .ToList();

            List<TimeSheetTask> timeSheetTasks = db.TimeSheetTasks
               .AsNoTracking()
               .Include(k => k.ProjectTask)
               .Where(k => k.TimeSheetId == timeSheetId)
               .Select(_mapper.Map<TimeSheetTask>)
               .ToList();

            // return if no tasks
            if (timeSheetTasks.Count == 0)
            {
                return projects;
            }

            List<int> projectsIds = timeSheetTasks.Select(k => k.ProjectTask.ProjectId).ToList();

            // get tasks' projects
            projects = db.Projects
              .AsNoTracking()
              .Where(k => projectsIds.Contains(k.ID))
              .Select(_mapper.Map<Project>)
              .ToList();

            // connect projects with their tasks
            foreach (Project project in projects)
            {
                project.Tasks = timeSheetTasks.Where(k => k.ProjectTask.ProjectId == project.ID)
                    .Select(k =>
                    {
                        ProjectTask task = k.ProjectTask;

                        task.TimeSheetTaskId = k.ID;

                        return task;
                    })
                    .ToList();
            }

            return projects;
        }
    }
}
