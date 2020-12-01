using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IRoleKeyMethods
    {
        Dictionary<ApplicationUserRole, string> GetRoleKeys();
    }
}