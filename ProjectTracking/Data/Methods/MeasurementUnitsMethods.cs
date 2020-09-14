using ProjectTracking.Data.Methods.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTracking.Data.Methods
{


    public class MeasurementUnitsMethods : GenericRepository<DataSets.MeasurementUnit>, IMeasurementUnitsMethods
    {
        public MeasurementUnitsMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }

        public List<int> GetActiveIds()
        {
            return _context.TimeSheetActivities.Where(k => k.MeasurementUnitId.HasValue)
                .Select(k => k.MeasurementUnitId.Value).Distinct().ToList();
        }
    }
}