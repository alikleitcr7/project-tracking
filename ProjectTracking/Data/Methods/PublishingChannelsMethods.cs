using ProjectTracking.Data.Methods.Interfaces;

namespace ProjectTracking.Data.Methods
{
    public class PublishingChannelsMethods : GenericRepository<DataSets.PublishingChannel>, IPublishingChannelsMethods
    {
        public PublishingChannelsMethods(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}