using System.Collections.Generic;
using ProjectTracking.DataContract;
using ProjectTracking.Models.Projects;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface ITasksMethods
    {
        void ChangeStatus(int taskId, short statusCode);
        bool Delete(int id);
        ProjectTask GetById(int id);
        List<ProjectTaskStatusModification> GetStatusModifications(int taskId);
        ProjectTask Save(TaskSaveModel model);
        List<ProjectTask> Search(string keyword, int projectId, int page, int countPerPage, out int totalCount);
    }
}