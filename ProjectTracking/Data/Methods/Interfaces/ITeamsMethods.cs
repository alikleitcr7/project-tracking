using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITeamsMethods
    {
        Team Add(Team department);
        bool Delete(int id);
        List<Team> GetAll(bool includeMembersCount = true);
        Team Update(Team Departmentdto);
        Team GetById(int id, bool includeMembers = true);
        System.Threading.Tasks.Task AddRemoveTeamsUsers(int teamId, List<string> userIds);
        List<string> GetTeamUsers(int teamId);
        //List<Team> GetAllExecludingSupervising(string userId);
        List<int> GetAllSupervisingTeamIds(string userId);
        List<Team> GetAllSupervisableTeams(string userId);
        Team GetSupervisingTeamId(string userId);
        System.Threading.Tasks.Task<Team> Save(Models.Teams.TeamSaveModel model);
    }
}