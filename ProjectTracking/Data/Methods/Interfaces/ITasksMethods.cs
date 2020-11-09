using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectTracking.DataContract;
using ProjectTracking.Models.Projects;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITasksMethods
    {
        Task ChangeStatus(int taskId, short statusCode,string byUserId);
        Task ChangeTimeSheetTaskStatus(int timeSheetId, int taskId, short statusCode, string byUserId);
        bool Delete(int id);
        ProjectTask GetById(int id);
        ProjectTask GetByIdWithProjectTitle(int id);
        List<ProjectTask> GetByProject(int projectId);
        Models.Tasks.TaskOverview GetOverview(int taskId);
        List<ProjectTaskStatusModification> GetStatusModifications(int taskId);
        ProjectTask Save(TaskSaveModel model);
        List<ProjectTask> Search(string keyword, int projectId, int page, int countPerPage, out int totalCount);
    }
}