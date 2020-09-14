using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{

    public interface IProjectFilesMethods : IGenericRepository<DataSets.ProjectReference>
    {
        List<DataSets.ProjectReference> Search(string keyword, int? projectId);
        ProjectFile GetFileWithActivities(int fileId);
    }
}