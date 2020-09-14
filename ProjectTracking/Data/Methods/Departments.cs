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
    public class TeamsMethods : IDepartments
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
    
        public TeamsMethods ()
        {

        }
        public Team Edit(int id, Team Department)
        {
            if (Department == null)
            {
                throw new ArgumentNullException();
            }

            var DepartmentInDb = _context.Teams.FirstOrDefault(c => c.ID == id);

            if (DepartmentInDb == null)
            {
                throw new NullReferenceException();
            }

            _mapper.Map(Department, DepartmentInDb);
            _context.SaveChanges();

            return Department;
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
                                                  .Select(k => _mapper.Map< DataSets.Team, Team>(k))
                                                  .ToList();
            return Departments;
        }

        public Team Add(Team departmentdto)
        {
            if (departmentdto != null )
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
        public Team EditDepartment(int id , Team department)
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
            var departmentinDb = _context.Teams.FirstOrDefault(c => c.ID == id);
            _context.Remove(departmentinDb);
            _context.SaveChanges();
            return (departmentinDb == null);
        }
    }
}
