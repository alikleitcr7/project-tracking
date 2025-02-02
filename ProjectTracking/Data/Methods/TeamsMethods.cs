﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.Projects;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class TeamsMethods : ITeamsMethods
    {
        private ApplicationDbContext _context;
        private readonly IHubContext<Hubs.ObserverHub> observerHub;
        private readonly INotificationMethods notificationMethods;
        private readonly IMapper _mapper;

        //public TeamsMethods()
        //{
        //}

        public TeamsMethods(IMapper mapper, ApplicationDbContext dbContext, IHubContext<Hubs.ObserverHub> observerHub, INotificationMethods notificationMethods)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //_context = new ApplicationDbContext(optionsBuilder.Options);
            _context = dbContext;
            this.observerHub = observerHub;
            this.notificationMethods = notificationMethods;
            _mapper = mapper;
        }

        public Team Add(Team team)
        {
            if (team == null)
            {
                throw new NullReferenceException();
            }

            var dbTeam = _context.Teams.Any(c => c.Name == team.Name);

            if (dbTeam)
            {
                throw new ClientException($"record exist under name {team.Name}");
            }

            var savedRecord = _context.Teams.Add(_mapper.Map<DataSets.Team>(team));

            _context.SaveChanges();

            team.ID = savedRecord.Entity.ID;

            return GetById(savedRecord.Entity.ID);
        }

        public Team Update(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException();
            }

            var dbRecord = _context.Teams.FirstOrDefault(c => c.ID == team.ID);

            if (dbRecord == null)
            {
                throw new NullReferenceException();
            }

            dbRecord.Name = team.Name;

            _context.SaveChanges();

            return GetById(dbRecord.ID);
        }


        //public Superviser GetTeamSupervisorLogs(TeamSaveModel model)
        //{

        //}

        public async Task<Team> Save(TeamSaveModel model)
        {
            string supervisorId = model.supervisorId;
            string assignedById = model.GetAssignedByUserId();
            string addedByUserId = model.GetAddedByUserId();

            if (supervisorId == null || assignedById == null)
            {
                throw new Exception("supervisor and assigned by user are required");
            }

            if (model.userIds == null || model.userIds.Count == 0)
            {
                throw new ClientException("at least one team member is required");
            }

            if (model.id.HasValue)
            {
                // check if name already exist 
                bool nameExist = _context.Teams.Any(k => k.ID != model.id.Value && k.Name == model.name);

                if (nameExist)
                {
                    throw new ClientException($"team exist under name {model.name}");
                }

                // # save team #

                // get team

                var dbTeam = _context.Teams.FirstOrDefault(k => k.ID == model.id.Value);

                if (dbTeam == null)
                {
                    throw new ClientException("record not found");
                }

                if (dbTeam.SupervisorId != supervisorId)
                {
                    // supervisor has changed
                    string removedSupervisorId = dbTeam.SupervisorId;
                    string addedSupervisorId = supervisorId;

                    // end session and notify removed supervisor
                    await observerHub.Clients.User(removedSupervisorId).SendAsync("SessionEnd", $"You have been removed from supervising a team");
                    await notificationMethods.Send(assignedById, removedSupervisorId, $"You have been removed from supervising team \"{dbTeam.Name}\"", NotificationType.Important);

                    var dbRemovedSupervisor = _context.Users.First(k => k.Id == removedSupervisorId);

                    dbRemovedSupervisor.NotificationFlag = true;
                    dbRemovedSupervisor.SecurityStamp = Guid.NewGuid().ToString("D");

                    // end session and notify new supervisor
                    await observerHub.Clients.User(addedSupervisorId).SendAsync("SessionEnd", "You have been assigned as a supervisor to a team");
                    await notificationMethods.Send(assignedById, addedSupervisorId, $"You have been assigned as a supervisor to team \"{dbTeam.Name}\"", NotificationType.Important);

                    var dbNewSupervisor = _context.Users.First(k => k.Id == addedSupervisorId);

                    dbNewSupervisor.NotificationFlag = true;
                    dbNewSupervisor.SecurityStamp = Guid.NewGuid().ToString("D");

                    // move current to logs
                    _context.SupervisorLogs.Add(new DataSets.SupervisorLog()
                    {
                        TeamId = dbTeam.ID,
                        UserId = dbTeam.SupervisorId,
                        DateAssigned = dbTeam.DateAssigned,
                        AssignedByUserId = dbTeam.AssignedByUserId,
                    });

                    // set new values
                    dbTeam.SupervisorId = supervisorId;
                    dbTeam.DateAssigned = DateTime.Now;
                    dbTeam.AssignedByUserId = assignedById;
                }

                // set model values
                dbTeam.Name = model.name;

                // add/remove team members
                await AddRemoveTeamsUsersFromContext(dbTeam, model.userIds, assignedById);


                // save changes
                _context.SaveChanges();

                // return the record
                return GetById(dbTeam.ID);
            }
            else
            {
                // # new team #

                // check if name already exist 
                bool nameExist = _context.Teams.Any(k => k.Name == model.name);

                // validation
                if (nameExist)
                {
                    throw new ClientException($"team exist under name {model.name}");
                }

                if (addedByUserId == null)
                {
                    throw new Exception("added user claim was not provided");
                }

                DataSets.Team dbTeam = new DataSets.Team()
                {
                    Name = model.name,
                    AddedByUserId = addedByUserId,
                    AssignedByUserId = assignedById,
                    DateAssigned = DateTime.Now,
                    DateAdded = DateTime.Now,
                    SupervisorId = supervisorId,
                };

                // end session and notify new supervisor
                await observerHub.Clients.User(supervisorId).SendAsync("SessionEnd", "You have been assigned as a supervisor to a team");
                await notificationMethods.Send(addedByUserId, supervisorId, $"You have been assigned as a supervisor to team \"{dbTeam.Name}\"", NotificationType.Important);


                var dbSupervisor = _context.Users.First(k => k.Id == supervisorId);

                dbSupervisor.NotificationFlag = true;
                dbSupervisor.SecurityStamp = Guid.NewGuid().ToString("D");

                // add the team
                _context.Teams.Add(dbTeam);

                // save changes
                _context.SaveChanges();

                // set team members
                await AddRemoveTeamsUsersFromContext(dbTeam, model.userIds, addedByUserId);

                // save changes on teamsteams
                _context.SaveChanges();

                return GetById(dbTeam.ID);
            }
        }

        public async Task AddRemoveTeamsUsers(int teamId, List<string> userIds)
        {
            var dbTeam = _context.Teams.FirstOrDefault(k => k.ID == teamId);

            if (dbTeam == null)
            {
                throw new ClientException("team dont exist");
            }

            await AddRemoveTeamsUsers(teamId, userIds);

            _context.SaveChanges();
        }

        /// <summary>
        /// no commit is made to the db
        /// </summary>
        private async Task AddRemoveTeamsUsersFromContext(DataSets.Team dbTeam, List<string> userIds, string byUserId)
        {
            string notifyMessage = "your team has been changed, you are required to login again";

            int teamId = dbTeam.ID;

            //var dbTeam = _context.Teams.First(k => k.ID == teamId);

            //if (dbTeam == null)
            //{
            //    throw new ClientException("team not found");
            //}

            // get existing users under the team
            var existingUsersUnderTeam = _context.Users.Where(k => k.TeamId == teamId);

            // set null for users that are not in the list and where removed

            IQueryable<ApplicationUser> usersRemovedFromTeam = existingUsersUnderTeam.Where(k => !userIds.Contains(k.Id));

            foreach (ApplicationUser user in usersRemovedFromTeam)
            {
                user.TeamId = null;
                user.SecurityStamp = Guid.NewGuid().ToString("D");
                user.NotificationFlag = true;

                await observerHub.Clients.User(user.Id).SendAsync("SessionEnd", notifyMessage);
                await notificationMethods.Send(byUserId, user.Id, $"You have been removed from team \"{dbTeam.Name}\"", NotificationType.Important);
            }


            // remove all items in the model that are already in db
            userIds.RemoveAll(k => existingUsersUnderTeam.Any(u => u.Id == k));

            // update/change the rest's team id
            if (userIds.Count > 0)
            {
                await _context.Users.Where(k => userIds.Contains(k.Id)).ForEachAsync(k =>
                {
                    k.TeamId = teamId;
                    k.NotificationFlag = true;
                    k.SecurityStamp = Guid.NewGuid().ToString("D");
                });

                foreach (string userId in userIds)
                {
                    await observerHub.Clients.User(userId).SendAsync("SessionEnd", notifyMessage);
                    await notificationMethods.Send(byUserId, userId, $"You are assigned to team \"{dbTeam.Name}\"", NotificationType.Important);
                }
            }


            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var record = _context.Teams
                .Include(k => k.TeamsProjects)
                .FirstOrDefault(c => c.ID == id);

            if (record == null)
            {
                throw new KeyNotFoundException();
            }

            if (record.TeamsProjects.Count > 0)
            {
                throw new ClientException("HAS_PROJECTS");
            }

            _context.Teams.Remove(record);

            return _context.SaveChanges() > 0;
        }

        public List<int> GetAllSupervisingTeamIds(string userId)
        {
            return _context.SupervisorLogs.Where(k => k.UserId == userId).Select(k => k.TeamId).ToList();
        }

        public List<Team> GetAllSupervisableTeams(string userId)
        {
            // teams where the USER is NOT a member in that team
            return _context.Teams
              .Where(k => !k.Members.Any(s => s.TeamId == k.ID && s.Id == userId))
              .Select(k => _mapper.Map<Team>(k))
              .ToList();
        }

        public List<Team> GetAll(bool includeMembersCount = false)
        {
            if (includeMembersCount)
            {
                return _context.Teams
                               .Include(k => k.AddedByUser)
                               .Include(k => k.Supervisor)
                               .Include(k => k.AssignedByUser)
                               .Select(k => new Team()
                               {
                                   ID = k.ID,
                                   Name = k.Name,
                                   MembersCount = k.Members.Count(),
                                   HasProjects = k.TeamsProjects.Any(),
                                   DateAdded = k.DateAdded,
                                   DateAssigned = k.DateAssigned,
                                   AddedByUserId = k.AddedByUserId,
                                   SupervisorId = k.SupervisorId,
                                   AssignedByUserId = k.AssignedByUserId,
                                   AddedByUser = new User()
                                   {
                                       Id = k.AddedByUser.Id,
                                       FirstName = k.AddedByUser.FirstName,
                                       LastName = k.AddedByUser.LastName,
                                   },
                                   Supervisor = new User()
                                   {
                                       Id = k.Supervisor.Id,
                                       FirstName = k.Supervisor.FirstName,
                                       LastName = k.Supervisor.LastName,
                                   },
                                   AssignedByUser = new User()
                                   {
                                       Id = k.AssignedByUser.Id,
                                       FirstName = k.AssignedByUser.FirstName,
                                       LastName = k.AssignedByUser.LastName,
                                   }
                               })
                               .ToList();
            }

            return _context.Teams
                 .Select(k => new Team()
                 {
                     ID = k.ID,
                     Name = k.Name,
                     HasProjects = k.TeamsProjects.Any(),
                     DateAdded = k.DateAdded,
                     DateAssigned = k.DateAssigned,
                     AddedByUserId = k.AddedByUserId,
                     SupervisorId = k.SupervisorId,
                     AssignedByUserId = k.AssignedByUserId,
                     AddedByUser = new User()
                     {
                         Id = k.AddedByUser.Id,
                         FirstName = k.AddedByUser.FirstName,
                         LastName = k.AddedByUser.LastName,
                     },
                     Supervisor = new User()
                     {
                         Id = k.Supervisor.Id,
                         FirstName = k.Supervisor.FirstName,
                         LastName = k.Supervisor.LastName,
                     },
                     AssignedByUser = new User()
                     {
                         Id = k.AssignedByUser.Id,
                         FirstName = k.AssignedByUser.FirstName,
                         LastName = k.AssignedByUser.LastName,
                     }
                 }).ToList();

            //.Include(k => k.AddedByUser)
            //.Include(k => k.Supervisor)
            //.Include(k => k.AssignedByUser)
            //.ToList()
            //.Select(k => _mapper.Map<Team>(k))
            //.ToList();
        }

        public List<SupervisorLog> GetSupervisorLog(int teamId)
        {
            var logs = _context.SupervisorLogs
                .AsNoTracking()
                .Include(k => k.User)
                .Include(k => k.AssignedByUser)
                .Where(k => k.TeamId == teamId)
                .Select(k => new SupervisorLog()
                {
                    ID = k.ID,
                    TeamId = k.TeamId,
                    UserId = k.UserId,
                    AssignedByUserId = k.AssignedByUserId,
                    DateAssigned = k.DateAssigned,
                    User = new User()
                    {
                        Id = k.User.Id,
                        FirstName = k.User.FirstName,
                        LastName = k.User.LastName,
                    },
                    AssignedByUser = new User()
                    {
                        Id = k.AssignedByUser.Id,
                        FirstName = k.AssignedByUser.FirstName,
                        LastName = k.AssignedByUser.LastName,
                    }
                });

            //var list = logs.ProjectTo<SupervisorLog>(_mapper.ConfigurationProvider);

            return logs.ToList();
        }

        public Team GetById(int id, bool includeMembers = true)
        {
            IQueryable<DataSets.Team> query = _context.Teams
                .Include(k => k.TeamsProjects)
                .Include(k => k.AddedByUser)
                .Include(k => k.Supervisor)
                .Include(k => k.AssignedByUser);

            if (includeMembers)
            {
                query = query.Include(k => k.Members);
            }

            DataSets.Team dbTeam = query.FirstOrDefault(k => k.ID == id);

            if (dbTeam == null)
            {
                return null;
            }

            Team pendingRecord = _mapper.Map<Team>(dbTeam);
            pendingRecord.MembersCount = dbTeam.Members?.Count;
            pendingRecord.HasProjects = dbTeam.TeamsProjects?.Count > 0;

            return pendingRecord;
        }

        public List<string> GetTeamUsers(int teamId)
        {
            return _context.Users
                .Where(k => k.TeamId == teamId)
                .Select(k => k.Id)
                .ToList();
        }

        public Team GetSupervisingTeamId(string userId)
        {
            throw new NotImplementedException();
        }

        public SupervisorTeamsModel GetSupervisorTeamsModel(string userId)
        {
            //List<SupervisingTeamModel> supervisingTeamModels = new List<SupervisingTeamModel>();

            // supervising
            List<SupervisingTeamModel> supervisingTeamModels = _context.Teams
                .Where(k => k.SupervisorId == userId)
                .Select(k => new SupervisingTeamModel()
                {
                    DateAdded = k.DateAdded,
                    ID = k.ID,
                    Name = k.Name,
                    ProjectsCount = k.TeamsProjects.Count(),
                    MembersCount = k.Members.Count()
                }).ToList();


            // set projects and members count
            //foreach (DataSets.Team dbTeam in dbSupervisingTeams)
            //{
            //    supervisingTeamModels.Add(new SupervisingTeamModel()
            //    {
            //        DateAdded = dbTeam.DateAdded,
            //        ID = dbTeam.ID,
            //        Name = dbTeam.Name,
            //        ProjectsCount = dbTeam.TeamsProjects.Count(),
            //        MembersCount = dbTeam.Members.Count()
            //    });
            //}

            // supervised
            //List<SupervisedTeamModel> supervisedTeamModels = new List<SupervisedTeamModel>();

            //IQueryable<DataSets.SupervisorLog> supervisorLogs = _context.SupervisorLogs
            //    .Include(k => k.Team)
            //    .Include(k => k.AssignedByUser)
            //    .Where(s => s.UserId == userId);

            List<SupervisedTeamModel> supervisedTeamModels = _context.SupervisorLogs
             .Where(s => s.UserId == userId)
             .Select(k => new SupervisedTeamModel()
             {
                 DateAdded = k.Team.DateAdded,
                 ID = k.Team.ID,
                 Name = k.Team.Name,
                 DateAssigned = k.DateAssigned,
                 AssignedByName = k.AssignedByUser.FirstName + " " + k.AssignedByUser.LastName,
                 AssignedById = k.AssignedByUser.Id
             }).ToList();


            //foreach (DataSets.SupervisorLog dbLog in supervisorLogs)
            //{
            //    supervisedTeamModels.Add(new SupervisedTeamModel()
            //    {
            //        DateAdded = dbLog.Team.DateAdded,
            //        ID = dbLog.Team.ID,
            //        Name = dbLog.Team.Name,
            //        DateAssigned = dbLog.DateAssigned,
            //        AssignedByName = dbLog.AssignedByUser.FirstName + " " + dbLog.AssignedByUser.LastName,
            //        AssignedById = dbLog.AssignedByUser.Id
            //    });
            //}

            return new SupervisorTeamsModel()
            {
                SupervisingTeams = supervisingTeamModels,
                SupervisedTeams = supervisedTeamModels
            };
        }

        public List<SupervisingTeamModel> GetSupervisingTeamsModel(string userId)
        {
            // supervising teams
            IQueryable<DataSets.Team> query = _context.Teams;

            if (userId != null)
            {
                query = query.Where(k => k.SupervisorId == userId);
            }

            var model = query.Select(k => new SupervisingTeamModel()
            {
                DateAdded = k.DateAdded,
                ID = k.ID,
                Name = k.Name,
                ProjectsCount = k.TeamsProjects.Count(),
                MembersCount = k.Members.Count(),
                Members = k.Members.Select(m => new KeyValuePair<string, string>(m.Id, m.FirstName + " " + m.LastName)).ToList(),
            }).ToList();


            // teams stats
            foreach (SupervisingTeamModel team in model)
            {
                FillTeamTaskPerformance(team);

                // timesheet activities
                if (team.Members.Count > 0)
                {
                    List<string> membersIds = team.Members.Select(k => k.Key).ToList();

                    int fromDateTargetDays = 15;
                    DateTime dateNow = DateTime.Now.Date;
                    DateTime fromDateTarget = dateNow.AddDays(-fromDateTargetDays);

                    // fill dates
                    List<DateTime> dates = Enumerable.Range(1, fromDateTargetDays)
                              .Select(x => fromDateTarget.AddDays(x))
                              .ToList();

                    DateTime minDate = dates.Min();

                    // group activities under supervising teams
                    var enum_teamsActivities = _context.TimeSheetActivities
                        .Where(k => !k.DeletedAt.HasValue && membersIds.Contains(k.TimeSheetTask.TimeSheet.UserId) && k.FromDate.Date >= minDate)
                        .OrderByDescending(k => k.FromDate)
                        .GroupBy(k => k.FromDate.Date)
                        //.Take(30)
                        .AsEnumerable();

                    team.ActivitiesFrequency =
                        dates.Select(date =>
                        {
                            var foundKey = enum_teamsActivities.FirstOrDefault(a => a.Key == date.Date);

                            return new KeyValuePair<DateTime, int>(date, foundKey == null ? 0 : foundKey.Count());
                        })
                        .ToList();

                    //team.ActivitiesFrequency = _context.TimeSheetActivities
                    //    .Where(k => !k.DeletedAt.HasValue && membersIds.Contains(k.TimeSheetTask.TimeSheet.UserId))
                    //    .OrderByDescending(k => k.FromDate)
                    //    .GroupBy(k => k.FromDate.Date)
                    //    .Take(30)
                    //    .AsEnumerable()
                    //    .Select((key) => new KeyValuePair<DateTime, int>(key.Key, key.Count()))
                    //    .ToList();

                }
            }

            return model;
        }

        private void FillTeamTaskPerformance(SupervisingTeamModel team)
        {
            IQueryable<DataSets.ProjectTask> q_tasks = _context.ProjectTasks.Where(t => t.Project.TeamsProjects.Any(tp => tp.TeamId == team.ID));

            //var taskList = q_tasks.ToList();

            // tasks performance
            team.TasksPerformance = new TasksPerformance()
            {
                TotalCount = q_tasks.Count(),
                DoneCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Done),
                ProgressCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.InProgress),
                PendingCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Pending),
                FailedOrTerminatedCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Failed || k.StatusCode == (short)ProjectTaskStatus.Terminated),
            };

        }



        /// <summary>
        ///// returns number of tasks assigned to a member on a team
        ///// </summary>
        //public List<KeyValuePair<Models.Users.UserKeyValue, int>> TeamWorkload(int teamId, short? statusCode)
        //{
        //    List<string> membersIds = _context.Users
        //        .Where(k => k.TeamId == teamId)
        //        .Select(k => k.Id)
        //        .ToList();

        //    return _context.TimeSheetTasks
        //           .Where(k => membersIds.Contains(k.TimeSheet.UserId) && k.ProjectTask.StatusCode == statusCode)
        //           .GroupBy(k => new { k.TimeSheet.UserId, Name = k.TimeSheet.User.FirstName + " " + k.TimeSheet.User.LastName })
        //           .AsEnumerable()
        //           .Select(k => new KeyValuePair<Models.Users.UserKeyValue, int>(new Models.Users.UserKeyValue(k.Key.UserId, k.Key.Name), k.Count()))
        //           .ToList();
        //}

        //public List<KeyValuePair<Models.Users.UserKeyValue, int>> ActiveActivities(int teamId, int page, int countPerPage)
        //{
        //    return q_tsActivities
        //            .Where(k => !k.ToDate.HasValue)
        //            .Select(_mapper.Map<TimeSheetActivity>)
        //            .ToList();
        //}

        public TeamViewModel GetTeamViewModel(int teamId)
        {
            // supervising teams
            var model = _context.Teams
                .Select(k => new TeamViewModel()
                {
                    DateAdded = k.DateAdded,
                    ID = k.ID,
                    Name = k.Name,
                    ProjectsCount = k.TeamsProjects.Count(),
                    MembersCount = k.Members.Count(),
                    Members = k.Members.Select(m => new KeyValuePair<string, string>(m.Id, m.FirstName + " " + m.LastName)).ToList(),
                })
                .FirstOrDefault(k => k.ID == teamId);

            if (model == null)
            {
                throw new ClientException("Team dont exist");
            }

            FillTeamTaskPerformance(model);

            // timesheet activities
            if (model.Members.Count > 0)
            {
                List<string> membersIds = model.Members.Select(k => k.Key).ToList();

                var q_tsActivities = _context.TimeSheetActivities
                    .Where(k => !k.DeletedAt.HasValue && membersIds.Contains(k.TimeSheetTask.TimeSheet.UserId));

                model.ActivitiesFrequency = q_tsActivities
                    .OrderByDescending(k => k.FromDate)
                    .GroupBy(k => k.FromDate.Date)
                    .Take(30)
                    .AsEnumerable()
                    .Select((key) => new KeyValuePair<DateTime, int>(key.Key, key.Count()))
                    .ToList();

                // # 
                model.ActiveActivities = q_tsActivities
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

                model.Workload = _context.TimeSheetTasks.Where(k => membersIds.Contains(k.TimeSheet.UserId))
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
                var memebresWithNoTimeSheets = model.Members.Where(k => !model.Workload.Any(w => w.Key.Id == k.Key)).ToList();

                if (memebresWithNoTimeSheets.Count > 0)
                {
                    model.Workload.AddRange(memebresWithNoTimeSheets.Select(k =>
                    new KeyValuePair<Models.Users.UserKeyValue, TasksWorkload>(new Models.Users.UserKeyValue(k.Key, k.Value), new TasksWorkload() { })));
                }

            }

            return model;
        }



        //public Team EditDepartment(int id, Team department)
        //{
        //    if (department == null)
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    var departmentInDb = _context.Teams.FirstOrDefault(c => c.ID == id);
        //    if (departmentInDb == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    _mapper.Map(department, departmentInDb);

        //    _context.SaveChanges();
        //    return department;
        //}
    }
}
