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
    public class CountriesController : Controller
    {
        private readonly ICountriesMethods _countriesMethods;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesMethods countriesMethods, IMapper mapper)
        {
            _countriesMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.Country Create(string name)
        {
            var dbCountry = _countriesMethods.Create(new Data.DataSets.Country()
            {
                Name = name
            });

            if (dbCountry != null)
            {
                return _mapper.Map<DataContract.Country>(dbCountry);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbCountry = _countriesMethods.GetByID(id);

            if (dbCountry != null)
            {
                return _countriesMethods.Delete(dbCountry);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            var dbCountry = _countriesMethods.GetByID(id);

            if (dbCountry != null)
            {
                dbCountry.Name = name;
                return _countriesMethods.Edit(dbCountry);
            }

            return false;
        }

        public List<DataContract.Country> GetAll()
        {
            var dbCountries = _countriesMethods.Filter().ToList();

            if (dbCountries != null)
            {
                return dbCountries.Select(k => _mapper.Map<DataContract.Country>(k)).ToList();
            }

            return new List<DataContract.Country>();
        }
    }
}