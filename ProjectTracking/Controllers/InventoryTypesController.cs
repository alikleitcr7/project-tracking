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
    public class InventoryTypesController : Controller
    {
        private readonly IInventoryTypesMethods _inventoryTypesMethods;
        private readonly IMapper _mapper;

        public InventoryTypesController(IInventoryTypesMethods countriesMethods, IMapper mapper)
        {
            _inventoryTypesMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.InventoryType Create(string name)
        {
            var dbInventoryType = _inventoryTypesMethods.Create(new Data.DataSets.InventoryType()
            {
                Name = name
            });

            if (dbInventoryType != null)
            {
                return _mapper.Map<DataContract.InventoryType>(dbInventoryType);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbInventoryType = _inventoryTypesMethods.GetByID(id);

            if (dbInventoryType != null)
            {
                return _inventoryTypesMethods.Delete(dbInventoryType);
            }

            return false;
        }

        public bool Update(int id, string name)
        {
            var dbInventoryType = _inventoryTypesMethods.GetByID(id);

            if (dbInventoryType != null)
            {
                dbInventoryType.Name = name;
                return _inventoryTypesMethods.Edit(dbInventoryType);
            }

            return false;
        }

        public List<DataContract.InventoryType> GetAll()
        {
            var dbInventoryTypes = _inventoryTypesMethods.Filter().ToList();

            if (dbInventoryTypes != null)
            {
                return dbInventoryTypes.Select(k => _mapper.Map<DataContract.InventoryType>(k)).ToList();
            }

            return new List<DataContract.InventoryType>();
        }
    }
}