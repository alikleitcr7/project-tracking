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

            bool hasActvities = _context.TimeSheetActivities.Any(k => k.Address == ipAddress);

            string deletionFlagMessage = "Cannot delete this ip address, there are {0} with this ip address";

            if (hasActvities)
            {
                throw new ClientException(string.Format(deletionFlagMessage, "activities"));
            }

            var hasTimeSheetLogs = _context.TimeSheetActivityLogs.Any(k => k.Address == ipAddress);

            if (hasTimeSheetLogs)
            {
                throw new ClientException(string.Format(deletionFlagMessage, "activities logs"));
            }

            var hasUserLoggings = _context.UserLogging.Any(k => k.Address == ipAddress);

            if (hasUserLoggings)
            {
                throw new ClientException(string.Format(deletionFlagMessage, "user logs"));
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
            else if (address == "::1")
            {
                address = "127.0.0.1";
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

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress.Address);

            if (dbRecord != null)
            {
                throw new ClientException("already exist");
            }

            // add
            _context.IpAddresses.Add(new DataSets.IpAddress()
            {
                Address = ipAddress.Address,
                Title = ipAddress.Title
            });
            _context.SaveChanges();

            return GetById(ipAddress.Address);
        }

        public IpAddress Save(IpAddress ipAddress)
        {
            EnsureCleanIpAddress(ipAddress);

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress.Address);

            if (dbRecord != null)
            {
                dbRecord.Title = ipAddress.Title;
            }
            else
            {
                _context.IpAddresses.Add(new DataSets.IpAddress()
                {
                    Address = ipAddress.Address,
                    Title = ipAddress.Title
                });
            }

            _context.SaveChanges();

            return GetById(ipAddress.Address);
        }

        public IpAddress Update(IpAddress ipAddress)
        {
            EnsureCleanIpAddress(ipAddress);

            var dbRecord = _context.IpAddresses.FirstOrDefault(k => k.Address == ipAddress.Address);

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
            else
            {
                ipAddress.Address = ipAddress.Address == "::1" ? "127.0.0.1" : ipAddress.Address.Trim();
            }

            if (!System.Net.IPAddress.TryParse(ipAddress.Address, out _))
            {
                throw new ClientException("invalid ip");
            }

            // set title to null to ensure the fetch unlisted
            if (string.IsNullOrEmpty(ipAddress.Title))
            {
                ipAddress.Title = null;
            }
            else
            {
                ipAddress.Title = ipAddress.Title.Trim();
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

        public List<IpAddress> GetListed()
        {
            return _context.IpAddresses.Where(k => k.Title != null).ToList().Select(_mapper.Map<IpAddress>).ToList();
        }
    }
}
