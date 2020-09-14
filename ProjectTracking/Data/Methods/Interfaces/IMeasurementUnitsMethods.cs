using ProjectTracking.DataContract;
using System.Collections.Generic;

namespace ProjectTracking.Data.Methods.Interfaces
{
  
    public interface IMeasurementUnitsMethods : IGenericRepository<DataSets.MeasurementUnit>
    {
        List<int> GetActiveIds();
    }
}