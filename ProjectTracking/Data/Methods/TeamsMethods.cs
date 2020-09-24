using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
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

            return team;
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

            return _mapper.Map<Team>(dbRecord);
        }

        public async Task AddRemoveTeamsUsers(int teamId, List<string> userIds)
        {
            var dbTeam = _context.Teams.FirstOrDefault(k => k.ID == teamId);

            if (dbTeam == null)
            {
                throw new ClientException("team dont exist");
            }

            // remove all items not in the model
            var existingUsersUnderTeam = _context.Users.Where(k => k.TeamId == teamId);

            // set null for users that are not in the list and where removed
            await existingUsersUnderTeam.Where(k => !userIds.Contains(k.Id)).ForEachAsync(k => k.TeamId = null);

            // remove all items in the model that are already in db
            userIds.RemoveAll(k => existingUsersUnderTeam.Any(u => u.Id == k));

            // update the rest's team id
            if (userIds.Count > 0)
            {
                await existingUsersUnderTeam.Where(k => userIds.Contains(k.Id)).ForEachAsync(k => k.TeamId = teamId);
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

        public List<Team> GetAll()
        {
            return _context.Teams.ToList()
                .Select(k => _mapper.Map<Team>(k))
                .ToList();
        }

        public Team GetById(int id)
        {
            var dbTeam = _context.Teams.FirstOrDefault(k => k.ID == id);

            return dbTeam != null ? _mapper.Map<Team>(dbTeam) : null;
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
