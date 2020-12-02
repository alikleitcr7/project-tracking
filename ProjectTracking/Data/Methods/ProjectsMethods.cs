using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;
using ProjectTracking.Models.Statistics;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Teams;
using ProjectTracking.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class ProjectsMethods : IProjectsMethods
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly INotificationMethods notificationMethods;

        public ProjectsMethods(IMapper mapper, INotificationMethods notificationMethods, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
            this.notificationMethods = notificationMethods;
        }

        public async Task<Project> Save(ProjectSaveModel model)
        {
            if (model.categoryId == 0 || string.IsNullOrEmpty(model.title))
            {
                throw new ClientException("title and category are required");
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
                        DateModified = DateTime.Now,
                        ModifiedByUserId = dbProject.StatusByUserId
                    });

                    await NotifyAdmins(model.GetStatusByUserId(), $"project status was changed", dbProject.ID);
                }

                // update values

                dbProject.Title = model.title;
                dbProject.Description = model.description;
                dbProject.CategoryId = model.categoryId;
                dbProject.StartDate = model.startDate;
                dbProject.PlannedEnd = model.plannedEnd;
                dbProject.ActualEnd = model.actualEnd;
                dbProject.LastModifiedDate = DateTime.Now;
                dbProject.StatusCode = model.statusCode;
                dbProject.StatusByUserId = model.GetStatusByUserId();

                AddRemoveTeamsProjects(model, dbProject);

                db.SaveChanges();

                return GetById(dbProject.ID);
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
                    StatusByUserId = model.GetStatusByUserId(),
                    AddedByUserId = model.GetStatusByUserId()
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

                // notify admin of new project

                await NotifyAdmins(model.GetStatusByUserId(), $"new project was added", dbProject.ID);


                return GetById(dbProject.ID);
            }
        }


        private async Task NotifyAdmins(string byUserId, string message, int projectId, NotificationType notificationType = NotificationType.Information)
        {
            List<string> adminIds = db.Users.Where(k => k.Id != byUserId && k.RoleCode == (short)ApplicationUserRole.Admin).Select(k => k.Id).ToList();

            foreach (string toUserId in adminIds)
            {
                await notificationMethods.Send(byUserId, toUserId, message, notificationType, true, null, projectId);
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
                query = query.Where(k => k.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            }

            totalCount = query.Count();

            return query.OrderByDescending(k => k.ID)
                .Skip(page * countPerPage)
                .Take(countPerPage)
                .Select(MapProjectWithUserAndCheckTasks)
                .ToList();
        }

        public static Expression<Func<DataSets.Project, Project>> MapProjectWithUserAndCheckTasks =>
           k => new Project()
           {
               ID = k.ID,
               ActualEnd = k.ActualEnd,
               DateAdded = k.DateAdded,
               Description = k.Description,
               PlannedEnd = k.PlannedEnd,
               StartDate = k.StartDate,
               AddedByUserId = k.AddedByUserId,
               StatusCode = k.StatusCode,
               LastModifiedDate = k.LastModifiedDate,
               AddedByUserName = k.AddedByUser.FirstName + " " + k.AddedByUser.LastName,
               StatusByUserName = k.StatusByUser.FirstName + " " + k.StatusByUser.LastName,
               StatusByUserId = k.StatusByUserId,
               CategoryId = k.CategoryId,
               Title = k.Title,
               HasTasks = k.Tasks.Any()
           };

        public static Expression<Func<DataSets.Project, Project>> MapProjectForGet =>
           k => new Project()
           {
               ID = k.ID,
               ActualEnd = k.ActualEnd,
               DateAdded = k.DateAdded,
               Description = k.Description,
               PlannedEnd = k.PlannedEnd,
               StartDate = k.StartDate,
               AddedByUserId = k.AddedByUserId,
               StatusCode = k.StatusCode,
               LastModifiedDate = k.LastModifiedDate,
               AddedByUserName = k.AddedByUser.FirstName + " " + k.AddedByUser.LastName,
               StatusByUserName = k.StatusByUser.FirstName + " " + k.StatusByUser.LastName,
               StatusByUserId = k.StatusByUserId,
               Title = k.Title,
               CategoryId = k.CategoryId,
               Category = new Category()
               {
                   ID = k.Category.ID,
                   Name = k.Category.Name,
               },
               TeamsProjects = k.TeamsProjects.Select(t => new TeamsProjects()
               {
                   ProjectId = t.ProjectId,
                   TeamId = t.TeamId
               })
           };

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

            if (dbProject == null)
            {
                throw new ClientException("project doesn't exist");
            }


            if (dbProject.Tasks.Count > 0)
            {
                throw new ClientException("HAS_TASKS");
            }

            db.Projects.Remove(dbProject);

            return db.SaveChanges() > 0;
        }

        public Project GetById(int id)
        {
            var record = db.Projects
                .Select(MapProjectForGet)
                .FirstOrDefault(k => k.ID == id);

            return record;
        }


        public bool IsSupervisorOfProject(string supervisorId, int projectId)
        {
            // get project's team
            List<int> projectTeamsIds = db.TeamsProjects
                .Where(k => k.ProjectId == projectId)
                .Select(k => k.TeamId)
                .ToList();

            if (projectTeamsIds.Count == 0)
            {
                return false;
            }

            // check if at least one of those teams are supervised by that user
            return db.Teams.Any(k => projectTeamsIds.Contains(k.ID) && k.SupervisorId == supervisorId);
        }

        public bool MemberCanAccessTask(string memberId, int taskId)
        {
            return db.TimeSheetTasks.Any(k => k.TimeSheet.UserId == memberId && k.ProjectTaskId == taskId);
        }
        public bool MemberCanAccessProject(string memberId, int projectId)
        {
            // get project's team
            List<int> projectTeamsIds = db.TeamsProjects
                .Where(k => k.ProjectId == projectId)
                .Select(k => k.TeamId)
                .ToList();

            if (projectTeamsIds.Count == 0)
            {
                return false;
            }

            // check if the member is a part of at least one of those teams 
            bool hasTeam = db.Users.Any(k => k.Id == memberId && projectTeamsIds.Contains(k.TeamId.Value));

            if (hasTeam)
            {
                return true;
            }

            // check tasks if some exist in prev. schedule
            List<int> taskIds = db.ProjectTasks.Where(k => k.ProjectId == projectId).Select(k => k.ID).ToList();

            if (taskIds.Count == 0)
            {
                return false;
            }

            return db.TimeSheetTasks.Any(k => k.TimeSheet.UserId == memberId && taskIds.Contains(k.ProjectTaskId));
        }

        public ProjectOverview GetOverview(int projectId)
        {
            if (!db.Projects.Any(k => k.ID == projectId))
            {
                throw new ClientException("Team dont exist");
            }

            // supervising teams

            List<TeamKeyValue> teams = db.TeamsProjects
                .Where(k => k.ProjectId == projectId)
                .Select(k => new TeamKeyValue(k.Team.ID, k.Team.Name))
                .ToList();

            List<int> teamsId = teams.Select(k => k.Id).ToList();

            ProjectOverview overview = new ProjectOverview()
            {
                Teams = teams,
                Members = teams.Count > 0 ? db.Users.Where(k => teamsId.Contains(k.TeamId.Value))
                .Select(k => new Models.Users.UserKeyValue(k.Id, k.FirstName + " " + k.LastName)
                {
                    TeamId = k.TeamId
                })
                .ToList() : new List<Models.Users.UserKeyValue>()
            };

            IQueryable<DataSets.ProjectTask> q_tasks = db.ProjectTasks
                .Where(t => t.ProjectId == projectId);

            overview.TasksPerformance = GetTasksPerformance(q_tasks);

            // timesheet activities
            if (overview.Members.Count > 0)
            {
                //List<string> membersIds = overview.Members.Select(k => k.Id).ToList();

                var q_tsActivities = db.TimeSheetActivities
                    .Where(k => !k.DeletedAt.HasValue &&
                    k.TimeSheetTask.ProjectTask.ProjectId == projectId);

                overview.ActivitiesFrequency = q_tsActivities
                    .OrderByDescending(k => k.FromDate)
                    .GroupBy(k => k.FromDate.Date)
                    .Take(30)
                    .AsEnumerable()
                    .Select((key) => new KeyValuePair<DateTime, int>(key.Key, key.Count()))
                    .ToList();

                // # 
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

                overview.Workload = db.TimeSheetTasks.Where(k => k.ProjectTask.ProjectId == projectId)
                    .Select(k => new { k.TimeSheet.UserId, Name = k.TimeSheet.User.FirstName + " " + k.TimeSheet.User.LastName, k.ProjectTask.StatusCode, })
                    //.Where(k => k.TimeSheetTask.ProjectTask.StatusCode == (short)ProjectTaskStatus.Pending)
                    .GroupBy(k => new { k.UserId, k.Name, })
                    .AsEnumerable()
                    .Select(k => new KeyValuePair<Models.Users.UserKeyValue, TasksWorkload>(new Models.Users.UserKeyValue(k.Key.UserId, k.Key.Name), new TasksWorkload()
                    {
                        DoneCount = k.Count(t => t.StatusCode == (short)ProjectTaskStatus.Done),
                        PendingCount = k.Count(t => t.StatusCode == (short)ProjectTaskStatus.Pending),
                        ProgressCount = k.Count(t => t.StatusCode == (short)ProjectTaskStatus.InProgress)
                    }))
                    .ToList();

                // add the users that are not in workloads that have no timesheets
                var memebresWithNoTimeSheets = overview.Members.Where(k => !overview.Workload.Any(w => w.Key.Id == k.Id)).ToList();

                if (memebresWithNoTimeSheets.Count > 0)
                {
                    overview.Workload.AddRange(memebresWithNoTimeSheets.Select(k =>
                    new KeyValuePair<Models.Users.UserKeyValue, TasksWorkload>(k, new TasksWorkload() { })));
                }

            }

            return overview;
        }

        private TasksPerformance GetTasksPerformance(IQueryable<DataSets.ProjectTask> q_tasks)
        {
            return new TasksPerformance()
            {
                TotalCount = q_tasks.Count(),
                DoneCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Done),
                ProgressCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.InProgress),
                PendingCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Pending),
                FailedOrTerminatedCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Failed || k.StatusCode == (short)ProjectTaskStatus.Terminated),
            };
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
                query = query.Where(k => k.CategoryId == categoryId.Value);
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
