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
    public class InventoryStatusesController : Controller
    {
        private readonly IInventoryStatusesMethods _inventoryStatusesMethods;
        private readonly IMapper _mapper;

        public InventoryStatusesController(IInventoryStatusesMethods countriesMethods, IMapper mapper)
        {
            _inventoryStatusesMethods = countriesMethods;
            _mapper = mapper;
        }

        [HttpPost]
        public DataContract.InventoryStatus Create(string name)
        {
            var dbInventoryStatus = _inventoryStatusesMethods.Create(new Data.DataSets.InventoryStatus()
            {
                Name = name
            });

            if (dbInventoryStatus != null)
            {
                return _mapper.Map<DataContract.InventoryStatus>(dbInventoryStatus);
            }

            return default;
        }

        public bool Delete(int id)
        {
            var dbInventoryStatus = _inventoryStatusesMethods.GetByID(id);

            if (dbInventoryStatus != null)
            {
                return _inventoryStatusesMethods.Delete(dbInventoryStatus);
            }

            return false;
        }


        public bool Update(int id, string name)
        {
            var dbInventoryStatus = _inventoryStatusesMethods.GetByID(id);

            if (dbInventoryStatus != null)
            {
                dbInventoryStatus.Name = name;
                return _inventoryStatusesMethods.Edit(dbInventoryStatus);
            }

            return false;
        }

        public List<DataContract.InventoryStatus> GetAll()
        {
            var dbInventoryStatuses = _inventoryStatusesMethods.Filter().ToList();

            if (dbInventoryStatuses != null)
            {
                return dbInventoryStatuses.Select(k => _mapper.Map<DataContract.InventoryStatus>(k)).ToList();
            }

            return new List<DataContract.InventoryStatus>();
        }
    }
}