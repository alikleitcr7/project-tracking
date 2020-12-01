using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{
    public class RoleKeyMethods : IRoleKeyMethods
    {
        private Dictionary<ApplicationUserRole, int> role_indexes = new Dictionary<ApplicationUserRole, int>()
        {
            {ApplicationUserRole.TeamMember , 0 },
            {ApplicationUserRole.Supervisor , 1 },
            {ApplicationUserRole.Admin , 2 },
        };

        private string GetRoleKeyFile()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "rolekeys.txt"); ;
        }

        public Dictionary<ApplicationUserRole, string> GetRoleKeys()
        {
            string file = GetRoleKeyFile();

            using (var sr = new StreamReader(file))
            {
                string content = sr.ReadToEnd();

                string[] keys = content.Split('\n').Select(k => k.Trim('\r')).ToArray();

                return new Dictionary<ApplicationUserRole, string>()
                {
                    { ApplicationUserRole.TeamMember, keys[role_indexes[ApplicationUserRole.TeamMember]]},
                    { ApplicationUserRole.Supervisor, keys[role_indexes[ApplicationUserRole.Supervisor]]},
                    { ApplicationUserRole.Admin, keys[role_indexes[ApplicationUserRole.Admin]]},
                };
            }
        }

        public void ChangeKey(ApplicationUserRole role, string key)
        {
            string file = GetRoleKeyFile();

            string[] arrLine = File.ReadAllLines(file);

            arrLine[role_indexes[role]] = key;

            File.WriteAllLines(file, arrLine);
        }
    }
}
