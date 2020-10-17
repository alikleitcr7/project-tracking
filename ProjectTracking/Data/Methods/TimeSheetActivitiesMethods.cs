using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.DataContract;
using ProjectTracking.Exceptions;
using ProjectTracking.Models.TimeSheet;
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
        private readonly IIpAddressMethods _ipAddressMethods;

        public TimeSheetActivitiesMethods(IMapper mapper, ITimeSheetActivityLogsMethods activityLogs, IIpAddressMethods ipAddressMethods, ApplicationDbContext context)
        {
            db = context;
            _mapper = mapper;
            _activityLogs = activityLogs;
            this._ipAddressMethods = ipAddressMethods;
        }

        public TimeSheetActivity Start(int timeSheetTaskId, string ipAddress)
        {
            // get the record 

            var dbRecord = db.TimeSheetTasks
                .Include(k => k.TimeSheet)
                .FirstOrDefault(k => k.ID == timeSheetTaskId);

            if (dbRecord == null)
            {
                throw new Exception("record not found");
            }

            // check if there are any active activities on that timesheet
            //var hasActiveActivity = db.TimeSheetActivities.Any(k =>
            //db.TimeSheetTasks.Any(t => t.ID == k.TimeSheetTaskId && t.TimeSheetId == dbRecord.TimeSheetId && !k.ToDate.HasValue));

            bool hasActiveActivity = UserHasActiveActivity(dbRecord.TimeSheet.UserId);

            if (hasActiveActivity)
            {
                throw new Exception($"there is already an active activity");
            }

            DataSets.TimeSheetActivity activity = new DataSets.TimeSheetActivity()
            {
                Address = ipAddress,
                FromDate = DateTime.Now,
                TimeSheetTaskId = timeSheetTaskId,
                DateAdded = DateTime.Now
            };

            bool ipAdded = _ipAddressMethods.AddIfNotExist(ipAddress);

            if (ipAdded)
            {
                activity.Address = ipAddress;
            }

            // add the activity
            var record_pending = db.TimeSheetActivities.Add(activity);

            // save changes
            if (db.SaveChanges() > 0)
            {
                // return activity
                return Get(record_pending.Entity.ID);
            }

            return null;
        }

        public TimeSheetActivity Stop(TimeSheetActivityStopModel model)
        {
            var dbActivity = db.TimeSheetActivities
                .Include(k => k.IpAddress)
                .FirstOrDefault(k => k.ID == model.activityId);

            if (dbActivity == null)
            {
                throw new ClientException("activity not found");
            }

            if (string.IsNullOrEmpty(model.message))
            {
                throw new ClientException("message is required");
            }

            dbActivity.ToDate = DateTime.Now;
            dbActivity.Message = model.message;

            db.SaveChanges();

            return _mapper.Map<TimeSheetActivity>(dbActivity);
        }

        public TimeSheetActivity Update(TimeSheetActivityUpdateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("activity is null");
            }

            if (string.IsNullOrEmpty(model.message))
            {
                throw new ClientException("message is required");
            }

            // can be null (case VPN, etc..)
            string ipAddress = model.GetIpAddress();

            // clean
            model.message = model.message.Trim();

            // save the location if doesn't exist
            if (ipAddress != null)
            {
                _ipAddressMethods.AddIfNotExist(ipAddress);
            }

            var dbActivity = db.TimeSheetActivities
                .Include(k => k.IpAddress)
                .FirstOrDefault(k => k.ID == model.id);

            if (dbActivity == null)
            {
                throw new ClientException("activity not found");
            }

            // check for change
            bool fromDateChanged = dbActivity.FromDate != model.fromDate;
            bool toDateChanged = dbActivity.ToDate != model.toDate;
            bool messageChanged = dbActivity.Message != model.message;

            bool hasChange = fromDateChanged || toDateChanged || messageChanged;

            if (hasChange)
            {
                db.TimeSheetActivityLogs.Add(new DataSets.TimeSheetActivityLog()
                {
                    Address = dbActivity.Address,
                    FromDate = dbActivity.FromDate,
                    ToDate = dbActivity.ToDate,
                    Message = dbActivity.Message,
                    TimeSheetActivityId = dbActivity.ID,
                    DateAdded = DateTime.Now
                });

                // update the activity
                dbActivity.Address = ipAddress;
                dbActivity.Message = model.message;
                dbActivity.FromDate = model.fromDate;
                dbActivity.ToDate = model.toDate;

                db.SaveChanges();
            }

            return _mapper.Map<TimeSheetActivity>(dbActivity);
        }

        private bool UserHasActiveActivity(string userId)
        {
            List<int> timesheetIds = db.TimeSheets.Where(k => k.UserId == userId).Select(k => k.ID).ToList();

            return db.TimeSheetActivities.Any(k => db.TimeSheetTasks.Any(t => t.ID == k.TimeSheetTaskId) && !k.ToDate.HasValue && !k.DeletedAt.HasValue);
        }

        public TimeSheetActivity GetUserActiveActivity(string userId)
        {
            List<int> timesheetIds = db.TimeSheets.Where(k => k.UserId == userId).Select(k => k.ID).ToList();

            if (timesheetIds.Count == 0)
            {
                return null;
            }

            var dbActiveActivity = db.TimeSheetActivities.Include(k => k.IpAddress).
                FirstOrDefault(k => db.TimeSheetTasks.Any(t => t.ID == k.TimeSheetTaskId) && !k.ToDate.HasValue && !k.DeletedAt.HasValue);


            return dbActiveActivity != null ? _mapper.Map<TimeSheetActivity>(dbActiveActivity) : null;
        }

        //public TimeSheetActivity Add(TimeSheetActivity activity, string ipAddress)
        //{
        //    try
        //    {
        //        //TimeSheetTaskId,
        //        //ipAddress

        //        if (activity.TimeSheetTaskId == 0)
        //        {
        //            return null;
        //        }

        //        bool ipAdded = _ipAddressMethods.AddIfNotExist(ipAddress);

        //        if (ipAdded)
        //        {
        //            activity.Address = ipAddress;
        //        }

        //        var dbActivity = _mapper.Map<DataSets.TimeSheetActivity>(activity);

        //        var record_pending = db.TimeSheetActivities.Add(dbActivity);

        //        if (db.SaveChanges() > 0)
        //        {
        //            return Get(record_pending.Entity.ID);
        //        }

        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        //public TimeSheetActivity Update(TimeSheetActivity activity, string ipAddress)
        //{
        //    try
        //    {
        //        DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities.FirstOrDefault(k => k.ID == activity.ID);

        //        if (dbActivity == null)
        //        {
        //            return null;
        //        }

        //        #region ACTIVITY LOG


        //        TimeSheetActivityLog log = _mapper.Map<TimeSheetActivityLog>(dbActivity);
        //        log.DateAdded = DateTime.Now;
        //        log.TimeSheetActivityId = dbActivity.ID;
        //        log.ID = 0;
        //        _activityLogs.Add(log);

        //        #endregion

        //        bool ipAdded = _ipAddressMethods.AddIfNotExist(ipAddress);

        //        dbActivity.Address = ipAdded ? activity.Address : null;
        //        dbActivity.FromDate = activity.FromDate;
        //        dbActivity.ToDate = activity.ToDate;
        //        dbActivity.Message = activity.Message;

        //        db.SaveChanges();

        //        return Get(dbActivity.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public TimeSheetActivity Get(int id)
        {
            DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities
                                                      //.Include(k => k.TimeSheetTask)
                                                      .Include(k => k.IpAddress)
                                                      .FirstOrDefault(k => k.ID == id);

            if (dbActivity == null)
            {
                return null;
            }

            TimeSheetActivity activity = _mapper.Map<TimeSheetActivity>(dbActivity);

            // get ip title
            //if (dbActivity.IpAddress != null)
            //{
            //    DataSets.IpAddress address = db.IpAddresses.FirstOrDefault(k => k.Address == dbActivity.IpAddress);

            //    if (address != null)
            //    {
            //        activity.IpAddressDisplay = address.Title;
            //    }
            //}

            return activity;
        }

        public List<TimeSheetActivity> GetByTimeSheet(int timesheetId)
        {
            List<DataSets.TimeSheetActivity> dbActivities = db.TimeSheetActivities
                                                              .Include(k => k.TimeSheetTask)
                                                              .Where(k => k.TimeSheetTask.TimeSheetId == timesheetId && !k.DeletedAt.HasValue)
                                                              .ToList();

            if (dbActivities == null)
            {
                return new List<TimeSheetActivity>();
            }

            List<TimeSheetActivity> activities = dbActivities.Select(_mapper.Map<TimeSheetActivity>).ToList();

            //PopulateIpAddresses(activities, db.IpAddresses.ToList());

            return activities;
        }

        public void Delete(int id)
        {
            DataSets.TimeSheetActivity dbActivity = db.TimeSheetActivities.FirstOrDefault(k => k.ID == id);

            if (dbActivity == null)
            {
                throw new ClientException("dont exist");
            }

            dbActivity.DeletedAt = DateTime.Now;

            db.SaveChanges();
            //if (dbActivity != null)
            //{
            //    db.TimeSheetActivities.Remove(dbActivity);

            //    return db.SaveChanges() > 0;
            //}

            //return false;
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

        //public static void PopulateIpAddress(DataContract.TimeSheetActivity activity, List<DataSets.IpAddress> ips)
        //{
        //    //if (activity != null && activity.IpAddress != null)
        //    //{
        //    //    DataSets.IpAddress address = ips.FirstOrDefault(k => k.Address == activity.IpAddress);

        //    //    if (address != null)
        //    //    {
        //    //        activity.IpAddressDisplay = address.Title;
        //    //    }
        //    //}
        //}

        //    public static void PopulateIpAddress(DataContract.TimeSheetActivityLog activity, List<DataSets.IpAddress> ips)
        //    {
        //        //if (activity != null && activity.IpAddress != null)
        //        //{
        //        //    DataSets.IpAddress address = ips.FirstOrDefault(k => k.Address == activity.IpAddress);

        //        //    if (address != null)
        //        //    {
        //        //        activity.IpAddressDisplay = address.Title;
        //        //    }
        //        //}
        //    //}

        //    //public static void PopulateIpAddresses(List<DataContract.TimeSheetActivity> activities, List<DataSets.IpAddress> ips)
        //    //{
        //    //    //if (activities.Count > 0)
        //    //    //{
        //    //    //    //List<DataSets.IpAddress> ips = db.IpAddresses.ToList();

        //    //    //    foreach (DataContract.TimeSheetActivity activity in activities)
        //    //    //    {
        //    //    //        PopulateIpAddress(activity, ips);
        //    //    //    }
        //    //    //}
        //    //}
        //    //public static void PopulateIpAddresses(List<DataContract.TimeSheetActivityLog> activities, List<DataSets.IpAddress> ips)
        //    //{
        //    //    //if (activities.Count > 0)
        //    //    //{
        //    //    //    //List<DataSets.IpAddress> ips = db.IpAddresses.ToList();

        //    //    //    foreach (DataContract.TimeSheetActivityLog activity in activities)
        //    //    //    {
        //    //    //        PopulateIpAddress(activity, ips);
        //    //    //    }
        //    //    //}
        //    //}

        public ProjectTask GetActivityProjectTask(int id)
        {
            var activity = db.TimeSheetActivities.First(k => k.ID == id);

            return _mapper.Map<ProjectTask>(db.ProjectTasks.Include(k => k.Project).First(k => k.ID == activity.TimeSheetTaskId));
        }


        #endregion

    }
}
