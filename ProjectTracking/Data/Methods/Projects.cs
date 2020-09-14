using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
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
        public List<Project> Get(int departmentId, int companyId, int page, int countPerPage, out int totalPages)
        {
            totalPages = 0;
            var result = db.Projects.Where(k => (departmentId == -1 ? !k.DepartmentId.HasValue : k.DepartmentId == departmentId)


            && (companyId == -1 ? !k.CompanyId.HasValue : k.CompanyId == companyId) && k.ParentId == null)
                                    .OrderByDescending(k => k.DateAdded).Include("Activities")
                                    .Select(_mapper.Map<Project>);

            totalPages = result.Count();


            var records = result.Skip(page * countPerPage)
                                             .Take(countPerPage)
                                             .ToList();

            return records;
        }

        public DateFilterModel GetProjectWithActivities(int projectId, int? year, int? month)
        {

            //List<DataSets.TimeSheetProject> timeSheetProjects = db
            //                        .Projects.AsNoTracking()
            //                        .Include(k => k.Activities)
            //                        .Where(k => k.ProjectId == projectId)
            //                        .ToList();

            DateFilterModel model = new DateFilterModel();

            DataSets.Project project = db.Projects.AsNoTracking()
                               .Include(k => k.Activities)
                               .ThenInclude(k => k.TimeSheetProjects)
                               .ThenInclude(k => k.Activities)
                               .Where(k => k.ID == projectId)
                               .FirstOrDefault();

            if (project == null)
            {
                return model;
            }

            List<DateTime> dates = new List<DateTime>();

            foreach (DataSets.Project item in project.Activities)
            {
                foreach (DataSets.TimeSheetProject timeSheetProject in item.TimeSheetProjects)
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
                              .Include(k => k.Activities)
                              .ThenInclude(k => k.TimeSheetProjects)
                              .ThenInclude(k => k.Activities)
                              .ThenInclude(k => k.MeasurementUnit)
                              .Include(k => k.Activities)
                              .ThenInclude(k => k.TimeSheetProjects)
                              .ThenInclude(k => k.Activities)
                              .ThenInclude(k => k.TypeOfWork)
                              .Where(k => k.ID == projectId).FirstOrDefault();

            return project == null ? null : _mapper.Map<DataContract.Project>(project);
        }

        public List<Project> Get(int departmentId, int companyId, bool includeActivities = true)
        {

            IQueryable<DataSets.Project> projects = db.Projects
                .Include(k => k.Team);


            if (includeActivities)
            {
                projects = projects.Include(k => k.Activities);
            }


            bool hasDepartment = departmentId != 0;
            bool hasCompany = companyId != 0;

            if (hasDepartment || hasCompany)
            {
                projects = projects.Where(k => (departmentId == -1 ? !k.DepartmentId.HasValue : departmentId == 0 ? true : k.DepartmentId == departmentId)
                            && (companyId == -1 ? !k.CompanyId.HasValue : companyId == 0 ? true : k.CompanyId == companyId) && k.ParentId == null);
            }


            var records = projects.ToList().Select(k => _mapper.Map<Project>(k)).ToList();

            return records;
        }

        public List<Project> Get(bool includeActivities = true)
        {
            var dbProjects = db.Projects.Where(k => k.ParentId == null);

            if (includeActivities)
            {
                dbProjects = dbProjects.Include("Activities");
            }

            var records = dbProjects.ToList().Select(_mapper.Map<Project>).ToList();

            return records;
        }

        public Project Add(string title, string description, int departmentId, int companyId, int? parentId = null)
        {
            DataSets.Project dbProject = new DataSets.Project()
            {
                Title = title,
                Description = description,
                DateAdded = DateTime.Now,
                DepartmentId = departmentId,
                ParentId = parentId,
                CompanyId = companyId
            };

            db.Projects.Add(dbProject);
            db.SaveChanges();

            return _mapper.Map<Project>(dbProject);
        }

        public bool Delete(int id)
        {
            //DataSets.Project dbProject = new DataSets.Project() { ID = id };

            var dbProject = db.Projects.Include("Activities").FirstOrDefault(k => k.ID == id);

            if (dbProject != null)
            {
                foreach (var dbActivity in dbProject.Activities)
                {
                    db.Projects.Remove(dbActivity);
                }

                db.Projects.Remove(dbProject);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public bool Update(int id, string title, string description, int companyId, int departmentId)
        {
            var entity = db.Projects.FirstOrDefault(k => k.ID == id);

            if (entity != null)
            {
                entity.Title = title;
                entity.Description = description;
                entity.CompanyId = companyId;
                entity.DepartmentId = departmentId;
                db.Projects.Update(entity);

                return db.SaveChanges() == 1;
            }

            return false;
        }

    }
}
