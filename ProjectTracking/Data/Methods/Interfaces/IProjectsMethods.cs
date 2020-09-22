using ProjectTracking.DataContract;
using ProjectTracking.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods.Interfaces
{
    public interface IProjectsMethods
    {
        Project GetById(int id);
        List<Project> Search(int? categoryId, string keyword, int page, int countPerPage, out int totalCount);
        Project Save(Models.Projects.ProjectSaveModel model);

        List<Project> Get(int departmentId, int companyId, bool includeActivities = true);
        List<Project> Get(bool includeActivities = true);
        Project Add(string title, string description, int departmentId, int companyId, int? parentId = null);
        Project GetProjectWithActivities(int projectId);
        bool Delete(int id);
        bool Update(int id, string title, string description, int companyId, int departmentId);
        DateFilterModel GetProjectWithActivities(int projectId, int? year, int? month);

        List<Project> Get(int departmentId, int companyId, int page, int countPerPage, out int totalCount);
    }
}
