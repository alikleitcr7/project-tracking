using AutoMapper;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class HolidaysMethods : GenericRepository<DataSets.Holiday, DataContract.Holiday>, IHolidaysMethods
    {
        public HolidaysMethods(ApplicationDbContext dbContext, IMapper mapper)
          : base(dbContext, mapper)
        {
        }
    }
}
