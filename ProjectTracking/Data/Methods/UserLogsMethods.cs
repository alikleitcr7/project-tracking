using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods
{
    public class UserLogsMethods : IUserLogsMethods
    {
        private readonly IIpAddressMethods _ipAddressesMethods;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext db;

        public UserLogsMethods(IIpAddressMethods ipAddressesMethods, IConfiguration config, IMapper mapper)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Setting.ConnectionString);
            db = new ApplicationDbContext(optionsBuilder.Options, config);

            this._ipAddressesMethods = ipAddressesMethods;
            this._mapper = mapper;
        }

        public UserLog AddStartLog(string userId, string ipAddress, string comments = null)
        {
            var isActive = db.UserLogging.Any(k => k.UserId == userId && !k.ToDate.HasValue);

            if (isActive)
            {
                return null;
            }

            bool ipAdded = _ipAddressesMethods.AddIfNotExist(ipAddress);

            DataSets.UserLog dbLog = new DataSets.UserLog()
            {
                Comments = comments,
                Address = ipAdded ? ipAddress : null,
                FromDate = DateTime.Now,
                UserId = userId
            };

            db.UserLogging.Add(dbLog);

            db.SaveChanges();

            return _mapper.Map<UserLog>(dbLog);
        }
    }
}
