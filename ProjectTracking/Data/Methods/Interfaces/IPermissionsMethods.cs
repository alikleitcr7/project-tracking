using System.Collections.Generic;
using ProjectTracking.DataContract;

namespace ProjectTracking.Data.Methods
{
    public interface IPermissionsMethods
    {
        Permission Add(Permission permission);
        bool Delete(int id);
        List<Permission> Get();
        bool Update(Permission permission);
    }
}