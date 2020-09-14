using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class IpAddressesMethods : GenericRepository<DataSets.IpAddress>, IIpAddressMethods
    {
        public IpAddressesMethods(ApplicationDbContext dbContext)
          : base(dbContext)
        {

        }

        public List<string> UnListedIps()
        {
            List<string> timeSheetActivitiesIps = _context.TimeSheetActivities.Where(k => k.IpAddress != null && !_context.IpAddresses.Select(c => c.Address).Contains(k.IpAddress))
                .Select(k => k.IpAddress)
                .Distinct()
                .ToList();

            List<string> logsIps = _context.UserLogging.Where(k => k.IPAddress != null && !_context.IpAddresses.Select(c => c.Address).Contains(k.IPAddress))
                .Select(k => k.IPAddress)
                .Distinct()
                .ToList();

            return timeSheetActivitiesIps.Union(logsIps).ToList();
        }
    }
}
