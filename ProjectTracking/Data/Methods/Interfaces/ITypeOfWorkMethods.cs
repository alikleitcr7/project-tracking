using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{

    public interface ITypeOfWorkMethods : IGenericRepository<DataSets.TypeOfWork>
    {
        List<int> GetActiveIds();
    }
}