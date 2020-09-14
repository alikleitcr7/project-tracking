using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IDepartments
    {
        Team Add(Team department);
        bool Delete(int id);
        List<Team> GetAll();
        Team Edit(int id, Team Departmentdto);
    }
}