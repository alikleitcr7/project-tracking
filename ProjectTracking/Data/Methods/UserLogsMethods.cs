using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private string GetUserName(string userId)
        {
            return db.Users.Select(k => new { k.Id, FullName = k.FirstName + " " + k.LastName }).First(k => k.Id == userId).FullName;
        }

        public UserLog AddStartLog(string userId, string ipAddress, UserLogStatus status)
        {
            try
            {
                var dbActiveLog = db.UserLogging.FirstOrDefault(k => k.UserId == userId && !k.ToDate.HasValue);

                if (dbActiveLog != null)
                {
                    UserLog activeLog = _mapper.Map<UserLog>(dbActiveLog);

                    activeLog.UserName = GetUserName(userId);

                    return activeLog;
                }

                if (ipAddress == "::1")
                {
                    ipAddress = "127.0.0.1";
                }

                bool ipAdded = _ipAddressesMethods.AddIfNotExist(ipAddress);

                DataSets.UserLog dbLog = new DataSets.UserLog()
                {
                    LogStatusCode = (short)status,
                    Address = ipAdded ? ipAddress : null,
                    FromDate = DateTime.Now,
                    UserId = userId
                };

                db.UserLogging.Add(dbLog);

                db.SaveChanges();

                UserLog log = _mapper.Map<UserLog>(dbLog);

                log.UserName = GetUserName(userId);

                return log;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserLog EndActiveLog(string userId, UserLogStatus status)
        {
            try
            {
                var log = db.UserLogging.FirstOrDefault(k => k.UserId == userId && !k.ToDate.HasValue);

                if (log != null)
                {
                    log.ToDate = DateTime.Now;
                    log.LogStatusCode = (short)status;
                    //db.UserLogging.Update(log);
                    db.SaveChanges();
                }

                return log != null ? _mapper.Map<UserLog>(log) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UserLog GetActiveUserLog(string userId)
        {
            var dbLog = db.UserLogging
                .FirstOrDefault(k => k.UserId == userId && !k.ToDate.HasValue);

            return dbLog == null ? null : _mapper.Map<UserLog>(dbLog);
        }


        public static Expression<Func<DataSets.UserLog, UserLog>> MapGetUserLog =>
      k => new UserLog()
      {
          ID = k.ID,
          FromDate = k.FromDate,
          ToDate = k.ToDate,
          LogStatusCode = k.LogStatusCode,
          UserId = k.UserId,
          UserName = k.User.FirstName + " " + k.User.LastName,
      };

    }
}
