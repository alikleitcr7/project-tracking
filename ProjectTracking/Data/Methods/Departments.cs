using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
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

        public TeamsMethods()
        {

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


        public TeamsMethods(IMapper mapper, ApplicationDbContext dbContext)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //_context = new ApplicationDbContext(optionsBuilder.Options);
            _context = dbContext;
            _mapper = mapper;
        }

        public List<Team> GetAll()
        {
            var Departments = _context.Teams.ToList()
                                                  .Select(k => _mapper.Map<DataSets.Team, Team>(k))
                                                  .ToList();
            return Departments;
        }

        public Team GetById(int id)
        {
            var dbTeam = _context.Teams.FirstOrDefault(k => k.ID == id);

            return dbTeam != null ? _mapper.Map<Team>(dbTeam) : null;
        }

        public Team Add(Team departmentdto)
        {
            if (departmentdto != null)
            {
                var nameExists = _context.Teams.FirstOrDefault(c => c.Name == departmentdto.Name);

                if (nameExists == null)
                {
                    var departmentinDb = _context.Teams.Add(_mapper.Map<DataSets.Team>(departmentdto));
                    _context.SaveChanges();
                    departmentdto.ID = departmentinDb.Entity.ID;
                    return departmentdto;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new NullReferenceException();
            }

        }
        public Team EditDepartment(int id, Team department)
        {
            if (department == null)
            {
                throw new ArgumentNullException();
            }
            var departmentInDb = _context.Teams.FirstOrDefault(c => c.ID == id);
            if (departmentInDb == null)
            {
                throw new NullReferenceException();
            }
            _mapper.Map(department, departmentInDb);

            _context.SaveChanges();
            return department;
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
    }
}
