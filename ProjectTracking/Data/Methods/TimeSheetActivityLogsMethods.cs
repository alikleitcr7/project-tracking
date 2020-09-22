
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectTracking.Data.Methods
{
    public class TimeSheetActivityLogsMethods : ITimeSheetActivityLogsMethods
    {
        private ApplicationDbContext db;
        private readonly IMapper _mapper;

        public TimeSheetActivityLogsMethods(IMapper mapper, ApplicationDbContext context)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
        }

        public TimeSheetActivityLog Add(TimeSheetActivityLog activity)
        {
            try
            {
                var activeActivity = db.TimeSheetActivities.FirstOrDefault(k => k.TimeSheetProjectTaskId == activity.TimeSheetTaskId && !k.ToDate.HasValue);

                if (activeActivity != null)
                {
                    return null;
                }

                var dbActivity = _mapper.Map<DataSets.TimeSheetActivityLog>(activity);

                var record_pending = db.TimeSheetActivityLogs.Add(dbActivity);

                if (db.SaveChanges() > 0)
                {
                    return Get(record_pending.Entity.ID);
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public TimeSheetActivityLog Get(int id)
        {
            DataSets.TimeSheetActivityLog dbActivity = db.TimeSheetActivityLogs
                                                      .FirstOrDefault(k => k.ID == id);

            return dbActivity == null ? null : _mapper.Map<TimeSheetActivityLog>(dbActivity);
        }

        public List<TimeSheetActivityLog> GetByActivity(int activityId)
        {
            List<DataSets.TimeSheetActivityLog> dbActivities = db.TimeSheetActivityLogs
                                                              .Where(k => k.TimeSheetActivityId == activityId)
                                                              .OrderByDescending(k => k.DateAdded)
                                                              .ToList();

            if (dbActivities == null)
            {
                return new List<TimeSheetActivityLog>();
            }

            List<TimeSheetActivityLog> logs = dbActivities.Select(_mapper.Map<TimeSheetActivityLog>).ToList();

            //db.IpAddresses.ToList()

            TimeSheetActivitiesMethods.PopulateIpAddresses(logs, db.IpAddresses.ToList());

            return logs.ToList();
        }

        public bool Delete(int id)
        {
            DataSets.TimeSheetActivityLog dbActivity = db.TimeSheetActivityLogs.FirstOrDefault(k => k.ID == id);

            if (dbActivity != null)
            {
                db.TimeSheetActivityLogs.Remove(dbActivity);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public bool Clear(int activityId)
        {
            var dbActivities = db.TimeSheetActivityLogs.Where(k => k.TimeSheetActivityId == activityId);

            if (dbActivities != null)
            {
                db.TimeSheetActivityLogs.RemoveRange(dbActivities);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public TimeSheetActivity GetActivity(int id)
        {
            TimeSheetActivityLog log = Get(id);

            if (log == null)
            {
                return null;
            }

            var dbActivity = db.TimeSheetActivities.FirstOrDefault(k => k.ID == log.TimeSheetActivityId);

            if (dbActivity == null)
            {
                return null;
            }

            return _mapper.Map<TimeSheetActivity>(dbActivity);
        }


    }
}
