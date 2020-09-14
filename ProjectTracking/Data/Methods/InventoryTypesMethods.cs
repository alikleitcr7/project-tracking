
using ProjectTracking.Data.Methods.Interfaces;


namespace ProjectTracking.Data.Methods
{
    public class InventoryTypesMethods : GenericRepository<DataSets.InventoryType>, IInventoryTypesMethods
    {
        public InventoryTypesMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }

}
