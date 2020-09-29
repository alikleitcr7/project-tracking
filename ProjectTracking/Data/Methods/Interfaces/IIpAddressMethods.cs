using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{

    public interface IIpAddressMethods 
    {
        List<string> UnListedIps();
        IpAddress Add(IpAddress ipAddress);
        IpAddress Update(IpAddress ipAddress);
        void Delete(string ipAddress);
        List<IpAddress> GetAll();
        bool AddIfNotExist(string address);
    }
}