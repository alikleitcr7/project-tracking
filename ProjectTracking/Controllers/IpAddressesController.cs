using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Controllers
{
    public class IpAddressesController : Controller
    {
        private readonly IIpAddressMethods _ipAddresses;
        private readonly IMapper _mapper;

        public IpAddressesController(IIpAddressMethods ipAddresses, IMapper mapper)
        {
            _ipAddresses = ipAddresses;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<string> GetUnlistedIps()
        {
            return _ipAddresses.UnListedIps();
        }

        [HttpPost]
        public IpAddress Create(string address, string title)
        {
            var dbAddress = _ipAddresses.Create(new Data.DataSets.IpAddress()
            {
                Address = address,
                Title = title
            });

            if (dbAddress != null)
            {
                return _mapper.Map<IpAddress>(dbAddress);
            }

            return null;
        }

        public bool Delete(int id)
        {
            var dbInventoryType = _ipAddresses.GetByID(id);

            if (dbInventoryType != null)
            {
                return _ipAddresses.Delete(dbInventoryType);
            }

            return false;
        }

        public bool Update(int id, string address, string title)
        {
            var dbInventoryType = _ipAddresses.GetByID(id);

            if (dbInventoryType != null)
            {
                dbInventoryType.Address = address;
                dbInventoryType.Title = title;
                return _ipAddresses.Edit(dbInventoryType);
            }

            return false;
        }

        public List<IpAddress> GetAll()
        {
            var dbIpAddresses = _ipAddresses.Filter().ToList();

            if (dbIpAddresses != null)
            {
                return dbIpAddresses.Select(k => _mapper.Map<IpAddress>(k)).ToList();
            }

            return new List<DataContract.IpAddress>();
        }
    }
}