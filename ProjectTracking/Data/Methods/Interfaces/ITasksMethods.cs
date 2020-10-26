using System.Collections.Generic;
using ProjectTracking.DataContract;
using ProjectTracking.Models.Projects;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITasksMethods
    {
        void ChangeStatus(int taskId, short statusCode,string byUserId);
        bool Delete(int id);
        ProjectTask GetById(int id);
        ProjectTask GetByIdWithProjectTitle(int id);
        Models.Tasks.TaskOverview GetOverview(int taskId);
        List<ProjectTaskStatusModification> GetStatusModifications(int taskId);
        ProjectTask Save(TaskSaveModel model);
        List<ProjectTask> Search(string keyword, int projectId, int page, int countPerPage, out int totalCount);
    }
}