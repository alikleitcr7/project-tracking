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
    public class UpdateFrequenciesController : Controller
    {
        private readonly IUpdateFrequenciesMethods _updateFrequenciesMethods;
        private readonly IMapper _mapper;

        public UpdateFrequenciesController(IUpdateFrequenciesMethods countriesMethods, IMapper mapper)
        {
            _updateFrequenciesMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.UpdateFrequency Create(string name)
        {
            var dbUpdateFrequency = _updateFrequenciesMethods.Create(new Data.DataSets.UpdateFrequency()
            {
                Name = name
            });
            if (dbUpdateFrequency != null)
            {
                return _mapper.Map<DataContract.UpdateFrequency>(dbUpdateFrequency);
            }
            return default;
        }

        public bool Delete(int id)
        {
            var dbUpdateFrequency = _updateFrequenciesMethods.GetByID(id);
            if (dbUpdateFrequency != null)
            {
                return _updateFrequenciesMethods.Delete(dbUpdateFrequency);
            }

            return false;
        }
        public bool Update(int id, string name)
        {
            var dbUpdateFrequency = _updateFrequenciesMethods.GetByID(id);
            if (dbUpdateFrequency != null)
            {
                dbUpdateFrequency.Name = name;
                return _updateFrequenciesMethods.Edit(dbUpdateFrequency);
            }
            return false;
        }
        public List<DataContract.UpdateFrequency> GetAll()
        {
            var dbUpdateFrequencies = _updateFrequenciesMethods.Filter().ToList();

            if (dbUpdateFrequencies != null)
            {
                return dbUpdateFrequencies.Select(k => _mapper.Map<DataContract.UpdateFrequency>(k)).ToList();
            }

            return new List<DataContract.UpdateFrequency>();
        }
    }
}