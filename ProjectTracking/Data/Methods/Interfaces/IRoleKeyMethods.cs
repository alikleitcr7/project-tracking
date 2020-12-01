using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IRoleKeyMethods
    {
        void ChangeKey(ApplicationUserRole role, string key);
        Dictionary<ApplicationUserRole, string> GetRoleKeys();
    }
}