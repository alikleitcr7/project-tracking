using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;
using ProjectTracking.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Users;

namespace ProjectTracking.Data.Methods
{
    public class TasksMethods : ITasksMethods
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;

        public TasksMethods(IMapper mapper, ApplicationDbContext context)
        {
            db = context;
            _mapper = mapper;
        }

        public ProjectTask Save(TaskSaveModel model)
        {
            if (string.IsNullOrEmpty(model.title))
            {
                throw new Exception("title required");
            }

            if (model.startDate.HasValue)
            {
                if (model.plannedEnd.HasValue && model.plannedEnd.Value <= model.startDate.Value)
                {
                    throw new ClientException("Planned Date must be greater than Start Date");
                }

                if (model.actualEnd.HasValue && model.actualEnd.Value <= model.startDate.Value)
                {
                    throw new ClientException("Actual Date must be greater than Start Date");
                }
            }
            else if (model.plannedEnd.HasValue || model.actualEnd.HasValue)
            {
                throw new Exception("Cannot insert Planned or Actual end date without a Start date");
            }

            if (model.id.HasValue)
            {
                // # save task #

                // get project

                var dbTask = db.ProjectTasks.FirstOrDefault(k => k.ID == model.id.Value);

                if (dbTask == null)
                {
                    throw new ClientException("record not found");
                }

                // check if status changed
                bool statusChanged = dbTask.StatusCode != model.statusCode;

                if (statusChanged)
                {
                    // append project's status modification
                    db.ProjectTaskStatusModifications.Add(new DataSets.ProjectTaskStatusModification()
                    {
                        ProjectTaskId = dbTask.ID,
                        StatusCode = dbTask.StatusCode,
                        ModifiedByUserId = dbTask.StatusByUserId,
                        DateModified = DateTime.Now
                    });
                }

                // update the task
                dbTask.Title = model.title;
                dbTask.Description = model.description;
                dbTask.StartDate = model.startDate;
                dbTask.PlannedEnd = model.plannedEnd;
                dbTask.ActualEnd = model.actualEnd;
                dbTask.LastModifiedDate = DateTime.Now;
                dbTask.StatusCode = model.statusCode;
                dbTask.StatusByUserId = model.GetStatusByUserId();

                db.SaveChanges();

                return GetById(dbTask.ID);
            }
            else
            {
                // # new task #

                // check title if exist
                bool nameExist = db.ProjectTasks.Any(k => k.Title == model.title);

                if (nameExist)
                {
                    throw new ClientException($"task exist under title {model.title}");
                }

                DataSets.ProjectTask dbTask = new DataSets.ProjectTask()
                {
                    Title = model.title,
                    ProjectId = model.projectId,
                    Description = model.description,
                    DateAdded = DateTime.Now,
                    StartDate = model.startDate,
                    PlannedEnd = model.plannedEnd,
                    ActualEnd = model.actualEnd,
                    StatusCode = model.statusCode,
                    StatusByUserId = model.GetStatusByUserId()
                };

                // add the task 
                db.ProjectTasks.Add(dbTask);

                // save changes on task
                db.SaveChanges();


                return GetById(dbTask.ID);
            }
        }

        public List<ProjectTaskStatusModification> GetStatusModifications(int taskId)
        {
            var dbTask = db.ProjectTasks
                .Include(k => k.ProjectTaskStatusModifications)
                .ThenInclude(k => k.ModifiedByUser)
                .FirstOrDefault(k => k.ID == taskId);

            if (dbTask == null)
            {
                throw new ClientException("task dont exist");
            }

            if (dbTask.ProjectTaskStatusModifications == null)
            {
                return null;
            }

            return dbTask.ProjectTaskStatusModifications
                .OrderByDescending(k => k.DateModified)
                .Select(k => new ProjectTaskStatusModification()
                {
                    DateModified = k.DateModified,
                    ProjectTaskId = k.ProjectTaskId,
                    StatusCode = k.StatusCode,
                    ModifiedByUserId = k.ModifiedByUserId,
                    ModifiedByUserName = k.ModifiedByUser.FirstName + " " + k.ModifiedByUser.LastName,
                })
                .ToList();
        }

        public void ChangeStatus(int taskId, short statusCode, string byUserId)
        {
            var dbTask = db.ProjectTasks.FirstOrDefault(k => k.ID == taskId);

            if (dbTask == null)
            {
                throw new ClientException("record not found");
            }

            bool hasChange = dbTask.StatusCode != statusCode;

            if (!hasChange)
            {
                return;
            }

            // append project's status modification
            db.ProjectTaskStatusModifications.Add(new DataSets.ProjectTaskStatusModification()
            {
                ProjectTaskId = dbTask.ID,
                StatusCode = dbTask.StatusCode,
                ModifiedByUserId = dbTask.StatusByUserId,
                DateModified = DateTime.Now
            });

            // set new status by user
            dbTask.StatusCode = statusCode;
            dbTask.StatusByUserId = byUserId;

            db.SaveChanges();
        }

        public static Expression<Func<DataSets.ProjectTask, ProjectTask>> MapProjectTaskWithUser =>
            k => new ProjectTask()
            {
                ID = k.ID,
                ActualEnd = k.ActualEnd,
                DateAdded = k.DateAdded,
                Description = k.Description,
                PlannedEnd = k.PlannedEnd,
                ProjectId = k.ProjectId,
                StartDate = k.StartDate,
                StatusByUserId = k.StatusByUserId,
                StatusCode = k.StatusCode,
                LastModifiedDate = k.LastModifiedDate,
                StatusByUserName = k.StatusByUser.FirstName + " " + k.StatusByUser.LastName,
                Title = k.Title,
            };

        public static Expression<Func<DataSets.ProjectTask, ProjectTask>> MapProjectTaskWithUserAndProjectTitle =>
            k => new ProjectTask()
            {
                ID = k.ID,
                ActualEnd = k.ActualEnd,
                DateAdded = k.DateAdded,
                Description = k.Description,
                PlannedEnd = k.PlannedEnd,
                ProjectId = k.ProjectId,
                StartDate = k.StartDate,
                StatusByUserId = k.StatusByUserId,
                StatusCode = k.StatusCode,
                LastModifiedDate = k.LastModifiedDate,
                StatusByUserName = k.StatusByUser.FirstName + " " + k.StatusByUser.LastName,
                ProjectTitle = k.Project.Title,
                Title = k.Title,
            };

        public List<ProjectTask> Search(string keyword, int projectId, int page, int countPerPage, out int totalCount)
        {
            IQueryable<DataSets.ProjectTask> query = db.ProjectTasks;

            query = query.Where(k => k.ProjectId == projectId).Include(k => k.StatusByUser);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            }

            totalCount = query.Count();

            return query.OrderByDescending(k => k.ID)
                .Skip(page * countPerPage)
                .Take(countPerPage)
                .Select(MapProjectTaskWithUser)
                .ToList();
        }
        
        public List<ProjectTask> GetByProject(int projectId)
        {
            IQueryable<DataSets.ProjectTask> query = db.ProjectTasks;

            query = query.Where(k => k.ProjectId == projectId).Include(k => k.StatusByUser);

            return query.OrderByDescending(k => k.ID)
                .Select(MapProjectTaskWithUser)
                .ToList();
        }


        public bool Delete(int id)
        {
            //DataSets.Project dbProject = new DataSets.Project() { ID = id };

            var dbTask = db.ProjectTasks.Include(k => k.TimeSheetTasks).FirstOrDefault(k => k.ID == id);

            if (dbTask != null)
            {
                dbTask.TimeSheetTasks.Clear();

                db.ProjectTasks.Remove(dbTask);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public ProjectTask GetById(int id)
        {
            return db.ProjectTasks
                .Select(MapProjectTaskWithUser)
                .FirstOrDefault(k => k.ID == id);
        }

        public ProjectTask GetByIdWithProjectTitle(int id)
        {
            return db.ProjectTasks
                .Select(MapProjectTaskWithUserAndProjectTitle)
                .FirstOrDefault(k => k.ID == id);
        }

        public TaskOverview GetOverview(int taskId)
        {
            if (!db.ProjectTasks.Any(k => k.ID == taskId))
            {
                throw new ClientException("Team dont exist");
            }

            //// supervising teams

            //List<TeamKeyValue> teams = db.TeamsProjects
            //    .Where(k => k.ProjectId == taskId)
            //    .Select(k => new TeamKeyValue(k.Team.ID, k.Team.Name))
            //    .ToList();

            //List<int> teamsId = teams.Select(k => k.Id).ToList();

            var q_tsActivities = db.TimeSheetActivities
                   .Where(k => !k.DeletedAt.HasValue &&
                   k.TimeSheetTask.ProjectTask.ID == taskId);

            TaskOverview overview = new TaskOverview();

            //IQueryable<DataSets.ProjectTask> q_tasks = db.ProjectTasks
            //    .Where(t => t.ProjectId == taskId);

            // timesheet activities
            //overview.Members.Count > 0

            //if (true)
            //{
            //List<string> membersIds = overview.Members.Select(k => k.Id).ToList();

            // ActivitiesFrequency
            overview.ActivitiesFrequency = q_tsActivities
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.FromDate.Date)
                .Take(30)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<DateTime, int>(key.Key, key.Count()))
                .ToList();

            // ActivitiesMinuts
            overview.ActivitiesMinutes = q_tsActivities
                .Where(k => k.ToDate.HasValue)
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.FromDate.Date)
                .Take(30)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<DateTime, int>(key.Key, (int)Math.Floor(key.Sum(a => (a.ToDate.Value - a.FromDate).TotalMinutes))))
                .ToList();

            // UserActivitiesFrequency
            overview.UserActivitiesFrequency = q_tsActivities
                .Select(k => new { k.FromDate, k.TimeSheetTask.TimeSheet.UserId, Name = k.TimeSheetTask.TimeSheet.User.FirstName + " " + k.TimeSheetTask.TimeSheet.User.LastName })
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.UserId)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<UserKeyValue, int>(new UserKeyValue(key.Key, key.First().Name), key.Count()))
                .ToList();

            // UserActivitiesMinuts
            overview.UserActivitiesMinutes = q_tsActivities
                //.Where(k => k.ToDate.HasValue)
                .Select(k => new { k.FromDate, k.ToDate, k.TimeSheetTask.TimeSheet.UserId, Name = k.TimeSheetTask.TimeSheet.User.FirstName + " " + k.TimeSheetTask.TimeSheet.User.LastName })
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.UserId)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<UserKeyValue, int>(new UserKeyValue(key.Key, key.First().Name), (int)Math.Floor(key.Sum(a => ((a.ToDate ?? DateTime.Now) - a.FromDate).TotalMinutes))))
                .ToList();

            //ActiveActivities
            overview.ActiveActivities = q_tsActivities
                .OrderByDescending(k => k.FromDate)
                .Where(k => !k.ToDate.HasValue)
                .Select(k => new TimeSheetActivity()
                {
                    ID = k.ID,
                    FromDate = k.FromDate,
                    User = new User()
                    {
                        Id = k.TimeSheetTask.TimeSheet.User.Id,
                        FirstName = k.TimeSheetTask.TimeSheet.User.FirstName,
                        LastName = k.TimeSheetTask.TimeSheet.User.LastName,
                    },
                    ProjectTask = new ProjectTask()
                    {
                        ID = k.TimeSheetTask.ProjectTask.ID,
                        Title = k.TimeSheetTask.ProjectTask.Title,
                    }
                })
                .ToList();

            //// add the users that are not in workloads that have no timesheets
            //var memebresWithNoTimeSheets = overview.Members.Where(k => !overview.Workload.Any(w => w.Key.Id == k.Id)).ToList();

            //if (memebresWithNoTimeSheets.Count > 0)
            //{
            //    overview.Workload.AddRange(memebresWithNoTimeSheets.Select(k =>
            //    new KeyValuePair<Models.Users.UserKeyValue, TasksWorkload>(k, new TasksWorkload() { })));
            //}
            //}

            return overview;
        }
    }
}
