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

        public Dictionary<ApplicationUserRole, string> GetRoleKeys()
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "rolekeys.txt");

            using (var sr = new StreamReader(file))
            {
                string content = sr.ReadToEnd();

                string[] keys = content.Split('\n');

                return new Dictionary<ApplicationUserRole, string>()
                {
                    { ApplicationUserRole.TeamMember, keys[role_indexes[ApplicationUserRole.TeamMember]]},
                    { ApplicationUserRole.Supervisor, keys[role_indexes[ApplicationUserRole.Supervisor]]},
                    { ApplicationUserRole.Admin, keys[role_indexes[ApplicationUserRole.Admin]]},
                };
            }
        }

        public void ChangeKey(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }
    }
}
