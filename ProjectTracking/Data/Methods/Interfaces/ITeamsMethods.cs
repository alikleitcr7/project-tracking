using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITeamsMethods
    {
        Team Add(Team department);
        bool Delete(int id);
        List<Team> GetAll();
        Team Update(Team Departmentdto);
        Team GetById(int id);
    }
}