using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{

    public class CountriesMethods : GenericRepository<DataSets.Country>, ICountriesMethods
    {
        public CountriesMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}