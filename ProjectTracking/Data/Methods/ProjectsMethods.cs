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
    public class ProjectsMethods : IProjectsMethods
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;

        public ProjectsMethods(IMapper mapper, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
        }

        public Project Save(ProjectSaveModel model, string addedByUserId)
        {
            if (model.categoryId == 0 || string.IsNullOrEmpty(model.title))
            {
                throw new ClientException("title and category are required");
            }


            if (model.id.HasValue)
            {
                // check if title already exist under the selected category
                bool nameExist = db.Projects.Any(k => k.ID != model.id.Value && k.Title == model.title && k.CategoryId == model.categoryId);

                if (nameExist)
                {
                    throw new ClientException($"project exist under title {model.title} and the selected category");
                }

                // # save project #

                // get project

                var dbProject = db.Projects
                    .Include(k => k.TeamsProjects)
                    .Include(k => k.ProjectStatusModifications)
                    .FirstOrDefault(k => k.ID == model.id.Value);

                if (dbProject == null)
                {
                    throw new ClientException("record not found");
                }

                // check if status changed
                bool statusChanged = dbProject.StatusCode != model.statusCode;

                if (statusChanged)
                {
                    // append project's status modification
                    dbProject.ProjectStatusModifications.Add(new DataSets.ProjectStatusModification()
                    {
                        ProjectId = dbProject.ID,
                        StatusCode = dbProject.StatusCode,
                        DateModified = DateTime.Now
                    });
                }

                // update values

                dbProject.Title = model.title;
                dbProject.Description = model.description;
                dbProject.CategoryId = model.categoryId;
                dbProject.StartDate = model.startDate;
                dbProject.PlannedEnd = model.plannedEnd;
                dbProject.ActualEnd = model.actualEnd;
                dbProject.StatusCode = model.statusCode;

                AddRemoveTeamsProjects(model, dbProject);

                db.SaveChanges();

                return _mapper.Map<Project>(dbProject);
            }
            else
            {
                // # new task #

                // check if title already exist under the selected category
                bool nameExist = db.Projects.Any(k => k.Title == model.title && k.CategoryId == model.categoryId);

                if (nameExist)
                {
                    throw new ClientException($"project exist under title {model.title} and the selected category");
                }

                DataSets.Project dbProject = new DataSets.Project()
                {
                    Title = model.title,
                    Description = model.description,
                    DateAdded = DateTime.Now,
                    CategoryId = model.categoryId,
                    StartDate = model.startDate,
                    PlannedEnd = model.plannedEnd,
                    ActualEnd = model.actualEnd,
                    StatusCode = model.statusCode,
                    AddedByUserId = addedByUserId
                };

                // add the project
                db.Projects.Add(dbProject);

                // save changes on project
                db.SaveChanges();

                // add the teams to the saved project
                db.TeamsProjects.AddRange(model.teamsIds.Select(k => new DataSets.TeamsProjects()
                {
                    ProjectId = dbProject.ID,
                    TeamId = k
                }));

                // save changes on teamsprojects
                db.SaveChanges();

                return _mapper.Map<Project>(dbProject);
            }
        }

        private void AddRemoveTeamsProjects(ProjectSaveModel model, DataSets.Project dbProject)
        {
            // remove all items not in the model
            dbProject.TeamsProjects.RemoveAll(k => !model.teamsIds.Contains(k.TeamId));

            // remove all items in the model that are already in db
            model.teamsIds.RemoveAll(k => dbProject.TeamsProjects.Any(t => t.TeamId == k));

            // add the rest
            if (model.teamsIds.Count > 0)
            {
                dbProject.TeamsProjects.AddRange(model.teamsIds.Select(k => new DataSets.TeamsProjects()
                {
                    ProjectId = dbProject.ID,
                    TeamId = k
                }));
            }
        }

        public List<Project> Search(int? categoryId, string keyword, int page, int countPerPage, out int totalCount)
        {
            IQueryable<DataSets.Project> query = db.Projects.Include(k => k.TeamsProjects);

            if (categoryId.HasValue)
            {
                query = query.Where(k => k.CategoryId.HasValue && k.CategoryId.Value == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            }

            totalCount = query.Count();

            return query.OrderByDescending(k => k.ID)
                .Skip(page * countPerPage)
                .Take(countPerPage)
                .ToList()
                .Select(_mapper.Map<Project>)
                .ToList();
        }

        public List<ProjectStatusModification> GetStatusModifications(int projectId)
        {
            var dbProject = db.Projects.Include(k => k.ProjectStatusModifications)
                .FirstOrDefault(k => k.ID == projectId);

            if (dbProject == null)
            {
                throw new ClientException("project dont exist");
            }

            if (dbProject.ProjectStatusModifications == null)
            {
                return null;
            }

            return dbProject.ProjectStatusModifications.OrderByDescending(k => k.DateModified).Select(_mapper.Map<ProjectStatusModification>).ToList();
        }

        public List<Project> GetByTeam(int teamId)
        {
            IQueryable<DataSets.Project> query = db.Projects
                .Include(k => k.Tasks)
                .Where(k => k.TeamsProjects.Any(t => t.TeamId == teamId));

            return query.OrderByDescending(k => k.ID)
                .ToList()
                .Select(_mapper.Map<Project>)
                .ToList();
        }

        public bool Delete(int id)
        {
            //DataSets.Project dbProject = new DataSets.Project() { ID = id };

            var dbProject = db.Projects.Include(k => k.Tasks).FirstOrDefault(k => k.ID == id);

            if (dbProject != null)
            {
                foreach (var dbTask in dbProject.Tasks)
                {
                    db.ProjectTasks.Remove(dbTask);
                }

                db.Projects.Remove(dbProject);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public Project GetById(int id)
        {
            var record = db.Projects.Include(k => k.TeamsProjects).FirstOrDefault(k => k.ID == id);

            return record != null ? _mapper.Map<Project>(record) : null;
        }


        // OTHER

        public List<Project> Get(int? teamId, int? categoryId, int page, int countPerPage, out int totalPages)
        {
            totalPages = 0;

            IQueryable<DataSets.Project> query = db.Projects.Include(k => k.Tasks);

            if (teamId.HasValue)
            {
                query = query.Include(k => k.TeamsProjects)
                           .Where(k => k.TeamsProjects.Any(tp => tp.TeamId == teamId.Value));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(k => k.CategoryId.HasValue && k.CategoryId == categoryId.Value);
            }

            query = query.OrderByDescending(k => k.DateAdded);

            totalPages = query.Count();

            return query.Skip(page * countPerPage)
                               .Take(countPerPage)
                               .ToList()
                               .Select(_mapper.Map<Project>).ToList();
        }

        public DateFilterModel GetProjectWithActivities(int projectId, int? year, int? month)
        {
            DateFilterModel model = new DateFilterModel();

            DataSets.Project project = db.Projects.AsNoTracking()
                               .Include(k => k.Tasks)
                               .ThenInclude(k => k.TimeSheetTasks)
                               .ThenInclude(k => k.Activities)
                               .Where(k => k.ID == projectId)
                               .FirstOrDefault();

            if (project == null)
            {
                return model;
            }

            List<DateTime> dates = new List<DateTime>();

            foreach (DataSets.ProjectTask item in project.Tasks)
            {
                foreach (DataSets.TimeSheetTask timeSheetProject in item.TimeSheetTasks)
                {
                    foreach (DataSets.TimeSheetActivity timeSheetActivity in timeSheetProject.Activities)
                    {
                        if (timeSheetActivity.ToDate.HasValue)
                        {
                            dates.Add(timeSheetActivity.FromDate);
                        }
                    }
                }
            }

            model = ParseDatesToFilterModel(dates, year, month);

            return model;
        }

        private DateFilterModel ParseDatesToFilterModel(List<DateTime> dates, int? year, int? month)
        {
            DateFilterModel model = new DateFilterModel
            {
                Years = dates.Select(k => k.Year).Distinct().ToList()
            };

            if (year.HasValue)
            {
                model.Months = dates.Where(k => k.Year == year.Value).Select(k => k.Month).Distinct().ToList();

                if (month.HasValue)
                {
                    model.Days = dates.Where(k => k.Year == year && k.Month == month.Value).Select(k => k.Day).Distinct().ToList();
                }
            }

            return model;
        }

        public Project GetProjectWithActivities(int projectId)
        {
            DataSets.Project project = db.Projects.AsNoTracking()
                              .Include(k => k.Tasks)
                              .ThenInclude(k => k.TimeSheetTasks)
                              .ThenInclude(k => k.Activities)
                              .Include(k => k.Tasks)
                              .ThenInclude(k => k.TimeSheetTasks)
                              .ThenInclude(k => k.Activities)
                              .Where(k => k.ID == projectId).FirstOrDefault();

            return project == null ? null : _mapper.Map<DataContract.Project>(project);
        }

        //public List<Project> Get(int teamId, int categoryId, bool includeActivities = true)
        //{

        //    IQueryable<DataSets.Project> projects = db.Projects
        //        .Include(k => k.TeamsProjects);


        //    if (includeActivities)
        //    {
        //        projects = projects.Include(k => k.Tasks);
        //    }


        //    bool hasDepartment = teamId != 0;
        //    bool hasCompany = categoryId != 0;

        //    if (hasDepartment || hasCompany)
        //    {
        //        projects = projects.Where(k => (teamId == -1 ? !k.TeamId.HasValue : teamId == 0 ? true : k.TeamId == teamId)
        //                    && (categoryId == -1 ? !k.CategoryId.HasValue : categoryId == 0 ? true : k.CategoryId == categoryId) && k.ParentId == null);
        //    }


        //    var records = projects.ToList().Select(k => _mapper.Map<Project>(k)).ToList();

        //    return records;
        //}

        //public List<Project> Get(bool includeActivities = true)
        //{
        //    var dbProjects = db.Projects.Where(k => k.ParentId == null);

        //    if (includeActivities)
        //    {
        //        dbProjects = dbProjects.Include("Activities");
        //    }

        //    var records = dbProjects.ToList().Select(_mapper.Map<Project>).ToList();

        //    return records;
        //}

        public List<Project> Get(int departmentId, int companyId, bool includeActivities = true)
        {
            throw new NotImplementedException();
        }

        public List<Project> Get(bool includeActivities = true)
        {
            throw new NotImplementedException();
        }

        public Project Add(string title, string description, int departmentId, int companyId, int? parentId = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, string title, string description, int companyId, int departmentId)
        {
            throw new NotImplementedException();
        }

        public List<Project> Get(int departmentId, int companyId, int page, int countPerPage, out int totalCount)
        {
            throw new NotImplementedException();
        }

        //public bool Update(int id, string title, string description, int companyId, int departmentId)
        //{
        //    var entity = db.Projects.FirstOrDefault(k => k.ID == id);

        //    if (entity != null)
        //    {
        //        entity.Title = title;
        //        entity.Description = description;
        //        entity.CategoryId = companyId;
        //        entity.TeamId = departmentId;
        //        db.Projects.Update(entity);

        //        return db.SaveChanges() == 1;
        //    }

        //    return false;
        //}

        // add
        // delete
        // getbycategory
        // 

    }
}
