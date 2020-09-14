using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{
    public class SubProjectsMethods : GenericRepository<DataSets.InventorySubProject>, ISubProjectsMethods
    {
        public SubProjectsMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}