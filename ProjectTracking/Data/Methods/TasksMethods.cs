using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;
using ProjectTracking.Models.Statistics;
using System;
using System.Collections.Generic;
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
                    dbTask.ProjectTaskStatusModifications.Add(new DataSets.ProjectTaskStatusModification()
                    {
                        ProjectTaskId = dbTask.ID,
                        StatusCode = dbTask.StatusCode,
                        DateModified = DateTime.Now
                    });
                }


                // update the task
                dbTask.Title = model.title;
                dbTask.Description = model.description;
                dbTask.StartDate = model.startDate;
                dbTask.PlannedEnd = model.plannedEnd;
                dbTask.ActualEnd = model.actualEnd;
                dbTask.StatusCode = model.statusCode;

                db.SaveChanges();

                return _mapper.Map<ProjectTask>(dbTask);
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
                    StatusCode = model.statusCode
                };

                // add the task 
                db.ProjectTasks.Add(dbTask);

                // save changes on task
                db.SaveChanges();


                return _mapper.Map<ProjectTask>(dbTask);
            }
        }

        public List<ProjectTaskStatusModification> GetStatusModifications(int taskId)
        {
            var dbTask = db.ProjectTasks.Include(k => k.ProjectTaskStatusModifications)
                .FirstOrDefault(k => k.ID == taskId);

            if (dbTask == null)
            {
                throw new ClientException("project dont exist");
            }

            if (dbTask.ProjectTaskStatusModifications == null)
            {
                return null;
            }

            return dbTask.ProjectTaskStatusModifications.OrderByDescending(k => k.DateModified).Select(_mapper.Map<ProjectTaskStatusModification>).ToList();
        }

        public void ChangeStatus(int taskId, short statusCode)
        {
            var dbTask = db.ProjectTasks.FirstOrDefault(k => k.ID == taskId);

            if (dbTask == null)
            {
                throw new ClientException("record not found");
            }

            dbTask.StatusCode = statusCode;

            db.SaveChanges();
        }

        public List<ProjectTask> Search(string keyword, int projectId, int page, int countPerPage, out int totalCount)
        {
            IQueryable<DataSets.ProjectTask> query = db.ProjectTasks;

            query = query.Where(k => k.ProjectId == projectId);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            }

            totalCount = query.Count();

            return query.OrderByDescending(k => k.ID)
                .Skip(page * countPerPage)
                .Take(countPerPage)
                .ToList()
                .Select(_mapper.Map<ProjectTask>)
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
            var record = db.ProjectTasks.FirstOrDefault(k => k.ID == id);

            return record != null ? _mapper.Map<ProjectTask>(record) : null;
        }
    }
}
