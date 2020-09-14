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
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            _mapper = mapper;
            _activityLogs = activityLogs;
        }

        public TimeSheetActivity Add(TimeSheetActivity activity)
        {
            try
            {
                if (activity.TimeSheetProjectId == 0)
                {
                    return null;
                }

                var dbActivity = _mapper.Map<DataSets.TimeSheetActivity>(activity);

                if (dbActivity.ProjectFile != null && dbActivity.ProjectFile.ID == 0)
                {
                    var dbTimeSheetProject = db.TimeSheetProjects.FirstOrDefault(k => k.ID == activity.TimeSheetProjectId);

                    if (dbTimeSheetProject == null)
                    {
                        return null;
                    }

                    var dbProjectFile = new DataSets.ProjectReference()
                    {
                        Name = dbActivity.ProjectFile.Name,
                        ProjectId = dbTimeSheetProject.ProjectId
                    };

                    db.ProjectReference.Add(dbProjectFile);

                    if (db.SaveChanges() > 0)
                    {
                        dbActivity.ProjectFileId = dbProjectFile.ID;
                    }
                }
                else
                {
                    dbActivity.ProjectFile = null;
                }

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
                                                      .Include(k => k.ProjectFile)
                                                      .Include(k => k.MeasurementUnit)
                                                      .Include(k => k.TypeOfWork)
                                                      .Include(k => k.TimeSheetProject)
                                                      .FirstOrDefault(k => k.ID == id);

            if (dbActivity != null)
            {
                TimeSheetActivity activity = _mapper.Map<TimeSheetActivity>(dbActivity);

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

            return null;
        }

        public List<TimeSheetActivity> GetByTimeSheet(int timesheetId)
        {
            List<DataSets.TimeSheetActivity> dbActivities = db.TimeSheetActivities
                                                              .Include(k => k.MeasurementUnit)
                                                              .Include(k => k.ProjectFile)
                                                              .Include(k => k.TypeOfWork)
                                                              .Include(k => k.TimeSheetProject)
                                                              .Where(k => k.TimeSheetProject != null && k.TimeSheetProject.TimeSheetId == timesheetId)
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


                #region PROJECT FILE 

                if (activity.ProjectFile != null && activity.ProjectFile.ID == 0)
                {
                    var dbTimeSheetProject = db.TimeSheetProjects.FirstOrDefault(k => k.ID == activity.TimeSheetProjectId);

                    if (dbTimeSheetProject == null)
                    {
                        return null;
                    }

                    var dbProjectFile = new DataSets.ProjectReference()
                    {
                        Name = activity.ProjectFile.Name,
                        ProjectId = dbTimeSheetProject.ProjectId
                    };

                    db.ProjectReference.Add(dbProjectFile);

                    if (db.SaveChanges() > 0)
                    {
                        activity.ProjectFileId = dbProjectFile.ID;
                    }
                }
                else
                {
                    activity.ProjectFile = null;
                }

                #endregion

                #region ACTIVITY LOG


                TimeSheetActivityLog log = _mapper.Map<TimeSheetActivityLog>(dbActivity);
                log.DateAdded = DateTime.Now;
                log.TimeSheetActivityId = dbActivity.ID;
                log.ID = 0;
                _activityLogs.Add(log);

                #endregion

                dbActivity.Number = activity.Number;
                dbActivity.IpAddress = activity.IpAddress;
                dbActivity.FromDate = activity.FromDate;
                dbActivity.ToDate = activity.ToDate;
                dbActivity.Comment = activity.Comment;

                dbActivity.MeasurementUnitId = activity.MeasurementUnitId;
                dbActivity.TypeOfWorkId = activity.TypeOfWorkId;
                dbActivity.ProjectFileId = activity.ProjectFileId;

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

            var dbTimeSheetProject = db.TimeSheetProjects.First(k => k.ID == activity.TimeSheetProjectId);

            string userId = db.TimeSheets.First(k => k.ID == dbTimeSheetProject.TimeSheetId).UserId;

            var dbUser = db.Users.Include(k => k.Team).First(k => k.Id == userId);

            return _mapper.Map<DataContract.User>(dbUser);

        }

        public Project GetActivityProject(int id)
        {
            //db.SaveChanges();

            TimeSheetActivity activity = Get(id);

            if (activity == null)
            {
                return null;
            }

            var dbTimeSheetProject = db.TimeSheetProjects.First(k => k.ID == activity.TimeSheetProjectId);

            var dbProject = db.Projects.AsNoTracking().Include(k => k.Parent).First(k => k.ID == dbTimeSheetProject.ProjectId);


            dbProject.Team = null;
            dbProject.Category = null;
            dbProject.Activities = null;


            if (dbProject.Parent != null)
            {
                dbProject.Parent.Team = null;
                dbProject.Parent.Category = null;
                dbProject.Parent.Activities = null;
            }

            return _mapper.Map<Project>(dbProject);
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

        #endregion
    }
}
