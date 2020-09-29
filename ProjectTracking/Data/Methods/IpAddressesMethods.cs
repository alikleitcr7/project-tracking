using AutoMapper;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class IpAddressesMethods : IIpAddressMethods
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IpAddressesMethods(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        public void Delete(string ipAddress)
        {
            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress);

            if (dbRecord == null)
            {
                throw new ClientException("not found, might be already deleted");
            }

            _context.IpAddresses.Remove(dbRecord);
            _context.SaveChanges();
        }

        public List<IpAddress> GetAll()
        {
            var dbRecords = _context.IpAddresses.ToList();

            return dbRecords.Select(_mapper.Map<IpAddress>).ToList();
        }

        public bool AddIfNotExist(string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return false;
            }

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == address);

            if (dbRecord != null)
            {
                return true;
            }

            _context.IpAddresses.Add(new DataSets.IpAddress()
            {
                Address = address,
                Title = null
            });

            return _context.SaveChanges() > 0;
        }

        public IpAddress Add(IpAddress ipAddress)
        {
            EnsureCleanIpAddress(ipAddress);

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress.Title);

            // add
            if (dbRecord == null)
            {
                _context.IpAddresses.Add(new DataSets.IpAddress()
                {
                    Address = ipAddress.Address,
                    Title = ipAddress.Title
                });

                return GetById(ipAddress.Address);
            }

            return _mapper.Map<IpAddress>(dbRecord);
        }

        public IpAddress Update(IpAddress ipAddress)
        {
            EnsureCleanIpAddress(ipAddress);

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress.Title);

            // update
            if (dbRecord != null)
            {
                dbRecord.Title = ipAddress.Title;

                _context.SaveChanges();

                return _mapper.Map<IpAddress>(dbRecord);
            }

            return null;
        }

        private void EnsureCleanIpAddress(IpAddress ipAddress)
        {
            if (ipAddress == null)
            {
                throw new Exception("ip address is null");
            }

            if (string.IsNullOrEmpty(ipAddress.Address))
            {
                throw new Exception("no address provided");
            }

            // set title to null to ensure the fetch unlisted
            if (string.IsNullOrEmpty(ipAddress.Title))
            {
                ipAddress.Title = null;
            }
        }

        public IpAddress GetById(string ipAddress)
        {
            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress);

            if (dbRecord == null)
            {
                throw new ClientException("ip not found");
            }

            return _mapper.Map<IpAddress>(dbRecord);
        }

        public List<string> UnListedIps()
        {
            return _context.IpAddresses.Where(k => k.Title == null).Select(k => k.Address).ToList();
            //List<string> timeSheetActivitiesIps = _context.TimeSheetActivities.Where(k => k.IpAddress != null && !_context.IpAddresses.Select(c => c.Address).Contains(k.IpAddress))
            //    .Select(k => k.IpAddress)
            //    .Distinct()
            //    .ToList();

            //List<string> logsIps = _context.UserLogging.Where(k => k.IPAddress != null && !_context.IpAddresses.Select(c => c.Address).Contains(k.IPAddress))
            //    .Select(k => k.IPAddress)
            //    .Distinct()
            //    .ToList();

            //return timeSheetActivitiesIps.Union(logsIps).ToList();
        }
    }
}
