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
    public class Projects : IProjectsMethods
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;

        public Projects(IMapper mapper, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
        }

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
                               .ThenInclude(k => k.TimeSheetProjects)
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
                foreach (DataSets.TimeSheetTask timeSheetProject in item.TimeSheetProjects)
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
                              .ThenInclude(k => k.TimeSheetProjects)
                              .ThenInclude(k => k.Activities)
                              .Include(k => k.Tasks)
                              .ThenInclude(k => k.TimeSheetProjects)
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

        public Project Save(ProjectSaveModel model)
        {
            if (model.id.HasValue)
            {
                // save project

                // get project

                var dbProject = db.Projects.FirstOrDefault(k => k.ID == model.id.Value);

                if (dbProject == null)
                {
                    throw new KeyNotFoundException("record not found");
                }

                dbProject.Title = model.title;
                dbProject.Description = model.description;
                dbProject.CategoryId = model.categoryId;

                if (db.SaveChanges() == 0)
                {
                    throw new Exception("record not saved");
                }

                var teamProjects = db.TeamsProjects.Where(k => k.TeamId == dbProject.ID);

                db.TeamsProjects.RemoveRange(teamProjects);

                db.TeamsProjects.AddRange(model.teamsIds.Select(k => new DataSets.TeamsProjects()
                {
                    ProjectId = dbProject.ID,
                    TeamId = k
                }));

                if (db.SaveChanges() == 0)
                {
                    throw new Exception("teams were not saved");
                }

                return _mapper.Map<Project>(dbProject);
            }
            else
            {
                // check title if exist

                bool nameExist = db.Projects.Any(k => k.Title == model.title);

                if (nameExist)
                {
                    throw new ClientException($"project exist under title {model.title}");
                }

                DataSets.Project dbProject = new DataSets.Project()
                {
                    Title = model.title,
                    Description = model.description,
                    DateAdded = DateTime.Now,
                    CategoryId = model.categoryId,
                };

                db.Projects.Add(dbProject);

                if (db.SaveChanges() == 0)
                {
                    throw new Exception("record not saved");
                }

                db.TeamsProjects.AddRange(model.teamsIds.Select(k => new DataSets.TeamsProjects()
                {
                    ProjectId = dbProject.ID,
                    TeamId = k
                }));

                if (db.SaveChanges() == 0)
                {
                    throw new Exception("teams were not saved");
                }

                return _mapper.Map<Project>(dbProject);
            }
        }

        public List<Project> Search(int? categoryId, string keyword, int page, int countPerPage, out int totalCount )
        {
            IQueryable<DataSets.Project> query = db.Projects;

            if (categoryId.HasValue)
            {
                query = query.Where(k => k.CategoryId.HasValue && k.CategoryId.Value == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            }

            totalCount = query.Count();

            return query.Skip(page * countPerPage)
                .Take(countPerPage)
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
            var record = db.Projects.FirstOrDefault(k => k.ID == id);

            return record != null ? _mapper.Map<Project>(record) : null;
        }

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
