using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
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
            var Categories = _context.Categories.ToList().Select(k => _mapper.Map<Data.DataSets.Category, Category>(k)).ToList(); ;
            return Categories;
        }
        public Category Edit(int id, Category Company)
        {
            if (Company == null)
            {
                throw new ArgumentNullException();
            }
            var CompanyInDb = _context.Categories.FirstOrDefault(c => c.ID == id);
            if (CompanyInDb == null)
            {
                throw new NullReferenceException();
            }
            _mapper.Map(Company, CompanyInDb);

            _context.SaveChanges();
            return Company;
        }
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
            var CompanyinDb = _context.Categories.FirstOrDefault(c => c.ID == id);
            _context.Remove(CompanyinDb);
            _context.SaveChanges();
            return (_mapper.Map<Category>(CompanyinDb) == null);

        }
    }
}
