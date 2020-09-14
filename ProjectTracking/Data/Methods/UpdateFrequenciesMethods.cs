using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{


    public class UpdateFrequenciesMethods : GenericRepository<DataSets.UpdateFrequency>, IUpdateFrequenciesMethods
    {
        public UpdateFrequenciesMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}