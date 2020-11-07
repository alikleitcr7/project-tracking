using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class CategoriesMethods : ICategoriesMethods
    {

        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesMethods(IMapper mapper, ApplicationDbContext dbContext)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //_context = new ApplicationDbContext(optionsBuilder.Options);
            _context = dbContext;
            _mapper = mapper;
        }
        public CategoriesMethods()
        {
        }

        public int ID { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30), MinLength(2)]

        public string Name { get; set; }

        public List<Category> GetAll()
        {
            return _context.Categories
                .Select(k => new Category()
                {
                    ID = k.ID,
                    Name = k.Name,
                    ProjectsCount = k.Projects.Count()
                })
                .ToList();
        }
        public Category GetById(int id)
        {
            var record = _context.Categories.FirstOrDefault(k => k.ID == id);

            return record != null ? _mapper.Map<Category>(record) : null;
        }

        //public Category Edit(int id, Category Company)
        //{
        //    if (Company == null)
        //    {
        //        throw new ArgumentNullException();
        //    }
        //    var CompanyInDb = _context.Categories.FirstOrDefault(c => c.ID == id);
        //    if (CompanyInDb == null)
        //    {
        //        throw new NullReferenceException();
        //    }
        //    _mapper.Map(Company, CompanyInDb);

        //    _context.SaveChanges();
        //    return Company;
        //}
        public Category Add(Category company)
        {
            if (company != null)
            {

                var checkName = _context.Categories.FirstOrDefault(c => c.Name == company.Name);

                if (checkName == null)
                {
                    var Company = _context.Categories.Add(_mapper.Map<Category, Data.DataSets.Category>(company));
                    _context.SaveChanges();
                    return _mapper.Map<Category>(Company.Entity);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        public bool Delete(int id)
        {
            var record = _context.Categories.FirstOrDefault(c => c.ID == id);

            if (record == null)
            {
                throw new KeyNotFoundException();
            }

            if (_context.Projects.Any(k => k.CategoryId == id))
            {
                throw new ClientException("HAS_PROJECTS");
            }

            _context.Categories.Remove(record);

            return _context.SaveChanges() > 0;
        }

        public Category Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            var dbCategory = _context.Categories.FirstOrDefault(c => c.ID == category.ID);

            if (dbCategory == null)
            {
                throw new NullReferenceException();
            }

            dbCategory.Name = category.Name;

            _context.SaveChanges();

            return _mapper.Map<Category>(dbCategory);
        }
    }
}
