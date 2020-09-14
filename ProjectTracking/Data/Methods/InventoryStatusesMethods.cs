using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{

    public class InventoryStatusesMethods : GenericRepository<DataSets.InventoryStatus>, IInventoryStatusesMethods
    {
        public InventoryStatusesMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}