using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Controllers
{
    public class MeasurementUnitsController : Controller
    {
        private readonly IMeasurementUnitsMethods _measurementUnitsMethods;
        private readonly IMapper _mapper;

        public MeasurementUnitsController(IMeasurementUnitsMethods countriesMethods, IMapper mapper)
        {
            _measurementUnitsMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.MeasurementUnit Create(string name)
        {
            var dbMeasurementUnit = _measurementUnitsMethods.Create(new Data.DataSets.MeasurementUnit()
            {
                Name = name
            });

            if (dbMeasurementUnit != null)
            {
                return _mapper.Map<DataContract.MeasurementUnit>(dbMeasurementUnit);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbMeasurementUnit = _measurementUnitsMethods.GetByID(id);

            if (dbMeasurementUnit != null)
            {
                return _measurementUnitsMethods.Delete(dbMeasurementUnit);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            var dbMeasurementUnit = _measurementUnitsMethods.GetByID(id);

            if (dbMeasurementUnit != null)
            {
                dbMeasurementUnit.Name = name;
                return _measurementUnitsMethods.Edit(dbMeasurementUnit);
            }

            return false;
        }

        public List<DataContract.MeasurementUnit> GetAll()
        {
            var dbMeasurementUnits = _measurementUnitsMethods.Filter().ToList();

            if (dbMeasurementUnits != null)
            {
                return dbMeasurementUnits.Select(k => _mapper.Map<DataContract.MeasurementUnit>(k)).ToList();
            }

            return new List<DataContract.MeasurementUnit>();
        }

        public List<int> GetActiveIds()
        {
            return _measurementUnitsMethods.GetActiveIds();
        }
    }
}