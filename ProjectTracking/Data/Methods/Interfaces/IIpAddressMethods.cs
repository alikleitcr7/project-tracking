using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{

    public interface IIpAddressMethods : IGenericRepository<DataSets.IpAddress>
    {
        List<string> UnListedIps();
    }
}