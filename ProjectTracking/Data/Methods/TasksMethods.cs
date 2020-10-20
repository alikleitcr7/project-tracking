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
    }
}
