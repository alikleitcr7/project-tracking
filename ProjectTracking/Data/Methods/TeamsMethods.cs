using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
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
        private readonly IMapper _mapper;

        //public TeamsMethods()
        //{
        //}

        public TeamsMethods(IMapper mapper, ApplicationDbContext dbContext)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //_context = new ApplicationDbContext(optionsBuilder.Options);
            _context = dbContext;
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
            string supervisorId = model.GetSupervisorId();
            string assignedById = model.GetAssignedByUserId();
            string addedByUserId = model.GetAddedByUserId();

            if (supervisorId == null || assignedById == null)
            {
                throw new Exception("supervisor and assigned by user claims were not provided");
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

                    // move current to logs
                    _context.Supervisers.Add(new DataSets.Superviser()
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
                await AddRemoveTeamsUsersFromContext(dbTeam.ID, model.userIds);

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

                // add the team
                _context.Teams.Add(dbTeam);

                // save changes on team
                _context.SaveChanges();

                // set team members
                await AddRemoveTeamsUsersFromContext(dbTeam.ID, model.userIds);

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
        private async Task AddRemoveTeamsUsersFromContext(int teamId, List<string> userIds)
        {
            // remove all items not in the model
            var existingUsersUnderTeam = _context.Users.Where(k => k.TeamId == teamId);

            // set null for users that are not in the list and where removed
            await existingUsersUnderTeam.Where(k => !userIds.Contains(k.Id)).ForEachAsync(k => k.TeamId = null);

            // remove all items in the model that are already in db
            userIds.RemoveAll(k => existingUsersUnderTeam.Any(u => u.Id == k));

            // update the rest's team id
            if (userIds.Count > 0)
            {
                await _context.Users.Where(k => userIds.Contains(k.Id)).ForEachAsync(k => k.TeamId = teamId);
            }

            _context.SaveChanges();
        }


        public bool Delete(int id)
        {
            var record = _context.Teams.FirstOrDefault(c => c.ID == id);

            if (record == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Teams.Remove(record);

            return _context.SaveChanges() > 0;
        }

        public List<int> GetAllSupervisingTeamIds(string userId)
        {
            return _context.Supervisers.Where(k => k.UserId == userId).Select(k => k.TeamId).ToList();
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
                               .Select(k => new Team()
                               {
                                   ID = k.ID,
                                   Name = k.Name,
                                   MembersCount = k.Members.Count()
                               })
                               .ToList();
            }

            return _context.Teams
                .ToList()
                .Select(k => _mapper.Map<Team>(k))
                .ToList();
        }

        public Team GetById(int id)
        {
            var dbTeam = _context.Teams.Include(k => k.Members).FirstOrDefault(k => k.ID == id);

            if (dbTeam == null)
            {
                return null;
            }

            Team pendingRecord = _mapper.Map<Team>(dbTeam);
            pendingRecord.MembersCount = dbTeam.Members.Count;

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
