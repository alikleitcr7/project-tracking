using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectTracking.Data.Methods
{
    public class TimeSheetActivitiesMethods : ITimeSheetActivitiesMethods
    {
        private ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly ITimeSheetActivityLogsMethods _activityLogs;

        public TimeSheetActivitiesMethods(IMapper mapper, ITimeSheetActivityLogsMethods activityLogs, ApplicationDbContext context)
        {
            db = context;
            _mapper = mapper;
            _activityLogs = activityLogs;
        }

        public TimeSheetActivity Add(TimeSheetActivity activity)
        {
            try
            {
                if (activity.TimeSheetTaskId == 0)
                {
                    return null;
                }

                var dbActivity = _mapper.Map<DataSets.TimeSheetActivity>(activity);

                var record_pending = db.TimeSheetActivities.Add(dbActivity);

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

        public TimeSheetActivity Get(int id)
        {
            DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities
                                                      .Include(k => k.TimeSheetTask)
                                                      .FirstOrDefault(k => k.ID == id);

            if (dbActivity == null)
            {
                return null;
            }

            TimeSheetActivity activity = _mapper.Map<TimeSheetActivity>(dbActivity);

            // get ip title
            if (dbActivity.IpAddress != null)
            {
                DataSets.IpAddress address = db.IpAddresses.FirstOrDefault(k => k.Address == dbActivity.IpAddress);

                if (address != null)
                {
                    activity.IpAddressTitle = address.Title;
                }
            }

            return activity;
        }

        public List<TimeSheetActivity> GetByTimeSheet(int timesheetId)
        {
            List<DataSets.TimeSheetActivity> dbActivities = db.TimeSheetActivities
                                                              .Include(k => k.TimeSheetTask)
                                                              .Where(k => k.TimeSheetTask.TimeSheetId == timesheetId)
                                                              .ToList();

            if (dbActivities == null)
            {
                return new List<TimeSheetActivity>();
            }

            List<TimeSheetActivity> activities = dbActivities.Select(_mapper.Map<TimeSheetActivity>).ToList();

            PopulateIpAddresses(activities, db.IpAddresses.ToList());

            return activities;
        }

        public TimeSheetActivity Update(TimeSheetActivity activity)
        {
            try
            {
                DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities.FirstOrDefault(k => k.ID == activity.ID);

                if (dbActivity == null)
                {
                    return null;
                }


                #region ACTIVITY LOG


                TimeSheetActivityLog log = _mapper.Map<TimeSheetActivityLog>(dbActivity);
                log.DateAdded = DateTime.Now;
                log.TimeSheetActivityId = dbActivity.ID;
                log.ID = 0;
                _activityLogs.Add(log);

                #endregion

                dbActivity.IpAddress = activity.IpAddress;
                dbActivity.FromDate = activity.FromDate;
                dbActivity.ToDate = activity.ToDate;
                dbActivity.Comment = activity.Comment;

                db.SaveChanges();

                return Get(dbActivity.ID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Delete(int id)
        {
            DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities.FirstOrDefault(k => k.ID == id);

            if (dbActivity != null)
            {
                db.TimeSheetActivities.Remove(dbActivity);

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public User GetActivityUser(int id)
        {
            TimeSheetActivity activity = Get(id);

            if (activity == null)
            {
                return null;
            }

            var dbTimeSheetProject = db.TimeSheetTasks.First(k => k.ID == activity.TimeSheetTaskId);

            string userId = db.TimeSheets.First(k => k.ID == dbTimeSheetProject.TimeSheetId).UserId;

            var dbUser = db.Users.Include(k => k.Team).First(k => k.Id == userId);

            return _mapper.Map<DataContract.User>(dbUser);

        }

        public ProjectTask GetActivityTask(int id)
        {
            //db.SaveChanges();

            TimeSheetActivity activity = Get(id);

            if (activity == null)
            {
                return null;
            }

            var dbTimeSheetTask = db.TimeSheetTasks.First(k => k.ID == activity.TimeSheetTaskId);

            var dbProject = db.ProjectTasks.AsNoTracking()
                .First(k => k.ID == dbTimeSheetTask.ProjectTaskId);

            //dbProject.Team = null;
            //dbProject.Category = null;
            //dbProject.Tasks = null;

            //if (dbProject.Parent != null)
            //{
            //    dbProject.Parent.Team = null;
            //    dbProject.Parent.Category = null;
            //    dbProject.Parent.Tasks = null;
            //}

            return _mapper.Map<ProjectTask>(dbProject);
        }

        #region STATIC

        public static void PopulateIpAddress(DataContract.TimeSheetActivity activity, List<DataSets.IpAddress> ips)
        {
            if (activity != null && activity.IpAddress != null)
            {
                DataSets.IpAddress address = ips.FirstOrDefault(k => k.Address == activity.IpAddress);

                if (address != null)
                {
                    activity.IpAddressTitle = address.Title;
                }
            }
        }

        public static void PopulateIpAddress(DataContract.TimeSheetActivityLog activity, List<DataSets.IpAddress> ips)
        {
            if (activity != null && activity.IpAddress != null)
            {
                DataSets.IpAddress address = ips.FirstOrDefault(k => k.Address == activity.IpAddress);

                if (address != null)
                {
                    activity.IpAddressTitle = address.Title;
                }
            }
        }

        public static void PopulateIpAddresses(List<DataContract.TimeSheetActivity> activities, List<DataSets.IpAddress> ips)
        {
            if (activities.Count > 0)
            {
                //List<DataSets.IpAddress> ips = db.IpAddresses.ToList();

                foreach (DataContract.TimeSheetActivity activity in activities)
                {
                    PopulateIpAddress(activity, ips);
                }
            }
        }
        public static void PopulateIpAddresses(List<DataContract.TimeSheetActivityLog> activities, List<DataSets.IpAddress> ips)
        {
            if (activities.Count > 0)
            {
                //List<DataSets.IpAddress> ips = db.IpAddresses.ToList();

                foreach (DataContract.TimeSheetActivityLog activity in activities)
                {
                    PopulateIpAddress(activity, ips);
                }
            }
        }

        public ProjectTask GetActivityProjectTask(int id)
        {
            var activity = db.TimeSheetActivities.First(k => k.ID == id);

            return _mapper.Map<ProjectTask>(db.ProjectTasks.Include(k=>k.Project).First(k => k.ID == activity.TimeSheetProjectTaskId));
        }

        #endregion
    }
}
