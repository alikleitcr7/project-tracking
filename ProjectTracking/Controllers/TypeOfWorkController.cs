
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
    public class TypeOfWorkController : Controller
    {
        private readonly ITypeOfWorkMethods _typeOfWorkMethods;
        private readonly IMapper _mapper;

        public TypeOfWorkController(ITypeOfWorkMethods countriesMethods, IMapper mapper)
        {
            _typeOfWorkMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.TypeOfWork Create(string name)
        {
            var dbTypeOfWork = _typeOfWorkMethods.Create(new Data.DataSets.TypeOfWork()
            {
                Name = name
            });
            if (dbTypeOfWork != null)
            {
                return _mapper.Map<DataContract.TypeOfWork>(dbTypeOfWork);
            }
            return default;
        }

        public bool Delete(int id)
        {
            var dbTypeOfWork = _typeOfWorkMethods.GetByID(id);
            if (dbTypeOfWork != null)
            {
                return _typeOfWorkMethods.Delete(dbTypeOfWork);
            }

            return false;
        }

        public bool Update(int id, string name)
        {
            var dbTypeOfWork = _typeOfWorkMethods.GetByID(id);
            if (dbTypeOfWork != null)
            {
                dbTypeOfWork.Name = name;
                return _typeOfWorkMethods.Edit(dbTypeOfWork);
            }
            return false;
        }

        public List<DataContract.TypeOfWork> GetAll()
        {
            var dbTypeOfWork = _typeOfWorkMethods.Filter().ToList();

            if (dbTypeOfWork != null)
            {
                return dbTypeOfWork.Select(k => _mapper.Map<DataContract.TypeOfWork>(k)).ToList();
            }

            return new List<DataContract.TypeOfWork>();
        }

        public List<int> GetActiveIds()
        {
            return _typeOfWorkMethods.GetActiveIds();
        }
    }
}