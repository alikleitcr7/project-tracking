using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Controllers
{
    public class SubProjectsController : Controller
    {
        private readonly ISubProjectsMethods _subProjectsMethods;
        private readonly IMapper _mapper;

        public SubProjectsController(ISubProjectsMethods countriesMethods, IMapper mapper)
        {
            _subProjectsMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.InventorySubProject Create(string name)
        {
            var dbInventorySubProject = _subProjectsMethods.Create(new Data.DataSets.InventorySubProject()
            {
                Name = name
            });

            if (dbInventorySubProject != null)
            {
                return _mapper.Map<DataContract.InventorySubProject>(dbInventorySubProject);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbInventorySubProject = _subProjectsMethods.GetByID(id);

            if (dbInventorySubProject != null)
            {
                return _subProjectsMethods.Delete(dbInventorySubProject);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            var dbInventorySubProject = _subProjectsMethods.GetByID(id);

            if (dbInventorySubProject != null)
            {
                dbInventorySubProject.Name = name;
                return _subProjectsMethods.Edit(dbInventorySubProject);
            }

            return false;
        }

        public List<DataContract.InventorySubProject> GetAll()
        {
            var dbSubProjects = _subProjectsMethods.Filter().ToList();

            if (dbSubProjects != null)
            {
                return dbSubProjects.Select(k => _mapper.Map<DataContract.InventorySubProject>(k)).ToList();
            }

            return new List<DataContract.InventorySubProject>();
        }
    }
}