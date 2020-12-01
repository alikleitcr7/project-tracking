using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.DataContract;
using ProjectTracking.Data.Methods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using ProjectTracking.Data.DataAccess;
using Microsoft.Extensions.Configuration;
using ProjectTracking.AppStart;
using ProjectTracking.Models.Users;
using ProjectTracking.Exceptions;
using System.ComponentModel.DataAnnotations;
using ProjectTracking.Utils;
using System.Threading.Tasks;
using ProjectTracking.Models.Tasks;
using ProjectTracking.Models.Dashboard;
using ProjectTracking.Models.Teams;
using System.Linq.Expressions;

namespace ProjectTracking.Data.Methods
{
    public class UsersMethods : IUserMethods
    {
        private ApplicationDbContext db;
        private readonly IMapper _mapper;
        private readonly IDataAccess dataAccess;

        private readonly IValidationExtensions _validation;
        private readonly IIpAddressMethods _ipAddressesMethods;

        public UsersMethods(IMapper mapper, IConfiguration config, IDataAccess dataAccess, IValidationExtensions validation)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Setting.ConnectionString);
            db = new ApplicationDbContext(optionsBuilder.Options, config);
            //db = dbContext;
            _mapper = mapper;
            this.dataAccess = dataAccess;
            _validation = validation;
            //this._ipAddressesMethods = ipAddressesMethods;
        }

        public User Save(UserSaveModel model)
        {
            ICollection<ValidationResult> results;

            if (!_validation.ValidateAnnotations(model, out results))
            {
                throw new ClientException(string.Join(", ", results.Select(k => k.ErrorMessage)));
            }

            // check if title already exist under the selected category
            bool emailExist = db.Users.Any(k => k.Email == model.email && k.Id != model.id);

            if (emailExist)
            {
                throw new ClientException($"email already exist!");
            }

            bool usernameExist = db.Users.Any(k => k.UserName == model.userName && k.Id != model.id);

            if (usernameExist)
            {
                throw new ClientException($"username already exist!");
            }

            if (model.id != null)
            {
                // save user

                // get user

                var dbUser = db.Users.FirstOrDefault(k => k.Id == model.id);

                if (dbUser == null)
                {
                    throw new ClientException("record not found");
                }

                dbUser.Email = model.email;
                dbUser.NormalizedEmail = model.email.ToUpper();
                dbUser.UserName = model.userName;
                dbUser.NormalizedUserName = model.userName.ToUpper();
                dbUser.FirstName = model.firstName;
                dbUser.LastName = model.lastName;
                //dbUser.MiddleName = model.middleName;
                dbUser.Title = model.title;
                dbUser.EmploymentTypeCode = model.employmentTypeCode;
                //dbUser.MonthlySalary = model.monthlySalary;
                //dbUser.HourlyRate = model.hourlyRate;

                db.SaveChanges();

                return GetById(dbUser.Id);
            }
            else
            {
                throw new Exception("id not provided");
            }
        }

        public List<User> Search(string keyword, int page, int countPerPage, string byUserId, out int totalCount)
        {
            var dbUser = db.Users.FirstOrDefault(k => k.Id == byUserId);

            if (dbUser == null)
            {
                throw new KeyNotFoundException("user not found");
            }

            ApplicationUserRole role = (ApplicationUserRole)dbUser.RoleCode;

            if (role == ApplicationUserRole.TeamMember)
            {
                throw new UnauthorizedAccessException();
            }


            IQueryable<User> query = db.Users.Select(k => new User()
            {
                Id = k.Id,
                FirstName = k.FirstName,
                ////MiddleName = k.MiddleName,
                LastName = k.LastName,
                Title = k.Title,
                Email = k.Email,
                //Role = k.Roles.FirstOrDefault().RoleId,
                DateOfBirth = k.DateOfBirth,
                UserName = k.UserName,
                //SupervisingCount = k.Supervising.Count(),
                EmploymentTypeCode = k.EmploymentTypeCode,
                //MonthlySalary = k.MonthlySalary,
                //HourlyRate = k.HourlyRate,
                RoleAssignedDate = k.RoleAssignedDate,
                RoleAssignedByUserId = k.RoleAssignedByUserId,
                //RoleAssignedByUserName = k.RoleAssignedByUser.FirstName + " " + k.RoleAssignedByUser.LastName,
                RoleCode = k.RoleCode
            });


            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(k => k.FirstName.Contains(keyword) || k.LastName.Contains(keyword) || k.Email.Contains(keyword));
            }

            if (role == ApplicationUserRole.Supervisor)
            {
                List<string> supervisingIds = SupervisingUsers(byUserId);

                query = query.Where(k => supervisingIds.Contains(k.Id));
            }

            totalCount = query.Count();

            return query.Skip(page * countPerPage)
                .Take(countPerPage)
                .ToList();
        }


        public List<KeyValuePair<string, int>> GetTotalCountByRoles()
        {
            return db.Users
                    .GroupBy(k => k.RoleCode)
                    .AsEnumerable()
                    .Select(k => new KeyValuePair<string, int>(((ApplicationUserRole)k.Key).ToString(), k.Count()))
                    .ToList();
        }

        public void Delete(string id)
        {
            //DataSets.User dbUser = new DataSets.User() { ID = id };

            var dbUser = db.Users.FirstOrDefault(k => k.Id == id);

            if (dbUser == null)
            {
                throw new ClientException("record not found or was already deleted");
            }

            // clear logs
            db.UserLogging.RemoveRange(db.UserLogging.Where(k => k.UserId == id));
            // clear supervisor
            db.SupervisorLogs.RemoveRange(db.SupervisorLogs.Where(k => k.UserId == id));

            // clear timesheets
            db.TimeSheets.RemoveRange(db.TimeSheets.Where(k => k.UserId == id));
            //db.TimeSheets.RemoveRange(db.TimeSheets.Where(k => k.AddedByUserId == id));

            // clear notifications (from or to)
            db.UserNotifications.RemoveRange(db.UserNotifications.Where(k => k.FromUserId == id || k.ToUserId == id));

            db.Users.Remove(dbUser);

            db.SaveChanges();
        }

        public User GetById(string id)
        {
            var record = db.Users
                .Include(k => k.Team)
                .Include(k => k.Supervising).FirstOrDefault(k => k.Id == id);

            if (record == null)
            {
                return null;
            }

            User parsedRecord = _mapper.Map<User>(record);
            //parsedRecord.SupervisingCount = record.Supervising.Count;

            return parsedRecord;
        }

        public List<User> GetUsersByRole(int roleCode)
        {
            return db.Users.Where(k => k.RoleCode == roleCode)
                .ToList()
                .Select(_mapper.Map<User>)
                .ToList();
        }
        public List<KeyValuePair<string, string>> GetUsersByRoleKeyValue(int roleCode, string byUserId)
        {
            var dbUser = db.Users.FirstOrDefault(k => k.Id == byUserId);

            if (dbUser == null || dbUser.RoleCode == (short)ApplicationUserRole.TeamMember)
            {
                throw new Exception("user dont exist");
            }

            var query = db.Users.Where(k => k.RoleCode == roleCode && k.Id != byUserId);

            if (dbUser.RoleCode == (short)ApplicationUserRole.Supervisor)
            {
                List<string> supervisingIds = SupervisingUsers(byUserId);

                return query.Where(k => supervisingIds.Contains(k.Id))
                               .Select(k => new KeyValuePair<string, string>(k.Id, k.FirstName + " " + k.LastName))
                               .ToList();
            }

            return query
                .Select(k => new KeyValuePair<string, string>(k.Id, k.FirstName + " " + k.LastName))
                .ToList();
        }

        public UserKeyValue GetUserKeyValue(string userId)
        {
            return db.Users
                .Select(k => new UserKeyValue(k.Id, k.FirstName + " " + k.LastName)
                {
                    TeamId = k.TeamId,
                    RoleCode = k.RoleCode
                })
                .FirstOrDefault(k => k.Id == userId);
        }

        public List<string> SupervisingUsers(string supervisorId)
        {
            //List<int> supervisingTeams = db.Supervisers.Where(k => k.UserId == supervisorId)
            //    .Select(k => k.TeamId).ToList();

            return db.Users.Where(k => db.Teams.Any(t => t.SupervisorId == supervisorId && t.ID == k.TeamId.Value))
                           .Select(k => k.Id)
                           .ToList();
        }

        public bool IsSupervisorOf(string supervisorId, string userId)
        {
            // get user's team
            int? teamId = db.Users.FirstOrDefault(k => k.Id == userId)?.TeamId;

            // user is not part of a team so he is not currently supervised
            if (!teamId.HasValue)
            {
                return false;
            }

            // check if the supervisor is supervising that team
            return db.Teams.Any(k => k.ID == teamId.Value && k.SupervisorId == supervisorId);
        }

        public bool IsSupervisor(string userId)
        {
            return db.Teams.Any(k => k.SupervisorId == userId);
        }

        public void AssignTeamSupervisor(string assignedById, string userId, int teamId)
        {
            var dbUser = db.Users.Include(k => k.Supervising).FirstOrDefault(k => k.Id == userId);

            if (dbUser == null)
            {
                throw new ClientException("user dont exist");
            }

            // get current team supervisor
            var currentSupervisor = db.SupervisorLogs.OrderByDescending(k => k.DateAssigned).FirstOrDefault(k => k.TeamId == teamId);

            // check if exist ? then check if that user is the current supervisor ? return 
            if (currentSupervisor != null && currentSupervisor.UserId == userId)
            {
                return;
            }

            db.SupervisorLogs.Add(new DataSets.SupervisorLog()
            {
                AssignedByUserId = assignedById,
                DateAssigned = DateTime.Now,
                UserId = userId,
                TeamId = teamId
            });

            db.SaveChanges();
        }

        //public void AddRemoveTeamsFromSupervisor(string userId, List<int> teamIds)
        //{
        //    var dbUser = db.Users.Include(k => k.Supervising).FirstOrDefault(k => k.Id == userId);

        //    if (dbUser == null)
        //    {
        //        throw new ClientException("user dont exist");
        //    }

        //    // remove all teams supervising from db that are not in the model
        //    dbUser.Supervising.RemoveAll(k => !teamIds.Contains(k.TeamId));

        //    // remove all items in the model that are already in db
        //    teamIds.RemoveAll(k => dbUser.Supervising.Any(u => u.TeamId == k));

        //    // add the rest
        //    if (teamIds.Count > 0)
        //    {
        //        dbUser.Supervising.AddRange(teamIds.Select(k => new DataSets.SupervisorLog()
        //        {
        //            UserId = userId,
        //            TeamId = k
        //        }));
        //    }

        //    db.SaveChanges();
        //}

        //public List<ApplicationIdentityRole> GetAllRoles()
        //{
        //    throw new NotImplementedException();
        //    //return db.Roles.ToList();
        //}

        public List<KeyValuePair<string, string>> GetAllUsersKeyValues()
        {
            return db.Users.ToList().Select(k => new KeyValuePair<string, string>(k.Id, k.FirstName + " " + k.LastName + $" ({k.UserName})")).ToList();
        }

        public List<KeyValuePair<string, string>> GetAllUsersExecludeTeamSupervisors(int teamId)
        {
            // users that ARE NOT SUPERVISING the TEAM
            return db.Users.Where(k => k.UserName != "admin" && !k.Supervising.Any(s => s.TeamId == teamId)).ToList().Select(k => new KeyValuePair<string, string>(k.Id, k.FirstName + " " + k.LastName + $" ({k.UserName})")).ToList();
        }

        //public UserLog AddStartLog(string userId, string ipAddress, string comments = null)
        //{
        //    var isActive = db.UserLogging.Any(k => k.UserId == userId && !k.ToDate.HasValue);

        //    if (isActive)
        //    {
        //        return null;
        //    }

        //    bool ipAdded = _ipAddressesMethods.AddIfNotExist(ipAddress);

        //    DataSets.UserLog dbLog = new DataSets.UserLog()
        //    {
        //        Comments = comments,
        //        Address = ipAdded ? ipAddress : null,
        //        FromDate = DateTime.Now,
        //        UserId = userId
        //    };

        //    db.UserLogging.Add(dbLog);

        //    db.SaveChanges();

        //    return _mapper.Map<UserLog>(dbLog);
        //}

        public List<UserLog> GetActiveLogs()
        {
            var dbLogs = db.UserLogging.Where(k => k.ToDate == null).ToList();

            return dbLogs.Select(k => _mapper.Map<UserLog>(k)).ToList();
        }

        //public List<string> GetSupervisorsIds(string forUserId)
        //{
        //    throw new NotImplementedException();

        //    //var dbUser = db.Users.FirstOrDefault(k => k.Id == forUserId);

        //    //if (dbUser == null)
        //    //{
        //    //    throw new KeyNotFoundException("user dont exist");
        //    //}

        //    //if (!dbUser.TeamId.HasValue)
        //    //{
        //    //    // user is not a part of a team
        //    //    return new List<string>();
        //    //}

        //    //return db.Supervisers.Where(k => k.TeamId == dbUser.TeamId).Select(k => k.SupervisorId).ToList();
        //}

        ////public List<string> GetSupervisorsIdsIncludingParents(string forUserId, int levels)
        ////{
        ////    var dbUser = db.Users.FirstOrDefault(k => k.Id == forUserId);

        ////    List<string> ids = db.Supervisers.Where(k => k.TeamId == forUserId).Select(k => k.SupervisorId).ToList();

        ////    List<string> upperIds = new List<string>();


        ////    if (levels > 0)
        ////    {
        ////        foreach (var upperId in ids)
        ////        {
        ////            upperIds.AddRange(GetSupervisorsIds(upperId));
        ////        }

        ////        ids.AddRange(upperIds);
        ////        upperIds.Clear();

        ////        if (levels > 1)
        ////        {
        ////            foreach (var upperId in ids)
        ////            {
        ////                upperIds.AddRange(GetSupervisorsIds(upperId));
        ////            }

        ////            ids.AddRange(upperIds);
        ////            upperIds.Clear();

        ////            if (levels > 2)
        ////            {
        ////                foreach (var upperId in ids)
        ////                {
        ////                    upperIds.AddRange(GetSupervisorsIds(upperId));
        ////                }

        ////                ids.AddRange(upperIds);
        ////                upperIds.Clear();
        ////            }
        ////        }
        ////    }

        ////    return ids.Distinct().ToList();

        ////    //List<string> ids = new List<string>(initIds);

        ////    //if (ids.Count == 0)
        ////    //{
        ////    //    levels = 0;
        ////    //}

        ////    //while (levels != 0)
        ////    //{
        ////    //    int nextLevel = --levels;

        ////    //    foreach (var upperLevelId in initIds)
        ////    //    {
        ////    //        List<string> upperLevelIds = GetSupervisorsIds(upperLevelId, nextLevel);

        ////    //        ids.AddRange(upperLevelIds);
        ////    //    }
        ////    //}

        ////    //return ids.Distinct().ToList();
        ////}

        //public List<int> GetSupervisingIds(string forUserId)
        //{
        //    throw new NotImplementedException();

        //    //var dbUser = db.Users.FirstOrDefault(k => k.Id == forUserId);

        //    //if (dbUser == null)
        //    //{
        //    //    throw new KeyNotFoundException("user dont exist");
        //    //}

        //    //if (!dbUser.TeamId.HasValue)
        //    //{
        //    //    // user is not a part of a team
        //    //    return new List<int>();
        //    //}

        //    //return db.Supervisers.Where(k => k.SupervisorId == dbUser.Id).Select(k => k.TeamId).ToList();
        //}

        //public List<string> GetUsersInRole(string role)
        //{

        //    throw new NotImplementedException();

        //    //var dbRole = db.Roles.FirstOrDefault(k => k.Name.Equals(role, StringComparison.OrdinalIgnoreCase));

        //    //List<string> users = new List<string>();

        //    //if (dbRole == null)
        //    //{
        //    //    return users;
        //    //}

        //    //return db.UserRoles.Where(k => k.RoleId == dbRole.Id).Select(k => k.UserId).ToList();
        //    //List<string> userIds = db.UserRoles.Where(k => k.RoleId == dbRole.Id).Select(k => k.UserId).ToList();

        //    //if (userIds.Count == 0)
        //    //{
        //    //    return users;
        //    //}

        //    //var dbUsers = db.Users.Where(u => userIds.Any(k => k == u.Id)).ToList();

        //    //if (dbUsers.Count == 0)
        //    //{
        //    //    return users;
        //    //}

        //    //return dbUsers.Select(_mapper.Map<User>).ToList();
        //}

        public User GetEmployee(string id)
        {
            var dbUser = db.Users.Include(k => k.Team)
                                 //.Include(k => k.Department)
                                 .FirstOrDefault(k => k.Id == id);

            return dbUser == null ? null : _mapper.Map<User>(dbUser);
        }

        //public List<TimeSheet> GetTimeSheets(string userId)
        //{
        //    var dbTimeSheets = db.TimeSheets
        //                         .Include(k => k.TimeSheetTasks)
        //                         .Where(k => k.UserId == userId);


        //    var parsedTimeSheets = dbTimeSheets.Select(_mapper.Map<TimeSheet>).ToList();

        //    return parsedTimeSheets;
        //}

        //public User GetProfile(string id)
        //{
        //    var dbUser = db.Users.Include(k => k.Team)
        //                         //.Include(k => k.RequestedPermissions)
        //                         .Include(k => k.Supervising)
        //                         //.Include(k => k.Supervisors)
        //                         .Include(k => k.Team)
        //                         .Where(k => k.Id == id)
        //                         .FirstOrDefault();

        //    return dbUser == null ? null : _mapper.Map<User>(dbUser);
        //}

        //public List<User> GetTeamMembers(int departmentId)
        //{
        //    var dbUsers = db.Users.Include(k => k.Team)
        //                          //.Include(k => k.RequestedPermissions)
        //                          //.Include(k => k.Company)
        //                          .Where(k => departmentId == 0 ? !k.TeamId.HasValue : k.TeamId.Value == departmentId)
        //                          .ToList();

        //    if (dbUsers.Count == 0)
        //    {
        //        return new List<User>();
        //    }

        //    return dbUsers.Select(_mapper.Map<User>).ToList();

        ////}
        //public List<User> GetEmployees()
        //{
        //    var dbUsers = db.Users.Include("Department")
        //                          //.Include(k => k.RequestedPermissions)
        //                          .Include(k => k.Team)
        //                          //.Include(k => k.Supervisors)
        //                          .Include(k => k.Supervising)
        //                          .ToList();
        //    List<User> mappedUser = dbUsers.Select(_mapper.Map<User>).ToList();

        //    return mappedUser;
        //}

        //public object GetSupervising(string Id)
        //{
        //    List<User> AllUsers = GetAllUsers().Where(c => c.Id.ToString() != Id).ToList();

        //    string[] SuperVising = db.Supervisers.Where(c => c.SupervisorId.Contains(Id))
        //                                         .Select(c => c.TeamId).ToArray();

        //    return new { All = AllUsers, SuperVise = SuperVising };
        //}

        //public List<User> GetSubordinatesWithTimeSheets(string superVisorId)
        //{
        //    List<ApplicationUser> AllUsers = db.Users.Where(c => c.Id.ToString() != superVisorId)
        //                                             .Include(c => c.TimeSheets)
        //                                             .Include(c => c.Team)
        //                                             //.Include(c => c.cate)
        //                                             .ToList();

        //    string[] SuperVising = db.Supervisers.Where(c => c.SupervisorId.Contains(superVisorId))
        //                                         .Select(c => c.TeamId).ToArray();

        //    var result = AllUsers.Where(c => SuperVising.Contains(c.Id)).Select(c => _mapper.Map<ApplicationUser, User>(c)).ToList();
        //    return result;
        //}

        public List<DataContract.UserLog> GetUsersLogs(List<string> userIds, int page, int countPerPage, DateTime? fromDate, DateTime? toDate, out int totalCount)
        {
            IQueryable<DataSets.UserLog> baseQuery = db.UserLogging
                .Include(k => k.IpAddress)
                .Include(k => k.User);

            if (userIds != null)
            {
                baseQuery = baseQuery.Where(k => userIds.Contains(k.UserId));
            }

            //if (forSupervisorId != null)
            //{
            //    string[] userIds = 
            //    baseQuery = baseQuery.Where(k=> k. )
            //}

            var query = baseQuery.Select(k => new
            {
                k.ID,
                k.FromDate,
                k.ToDate,
                k.LogStatusCode,
                k.Address,
                k.UserId,
                k.User,
                k.IpAddress,
            });

            //if (!(string.IsNullOrEmpty(fromDate) || fromDate == "null"))
            //{
            //    query = query.Where(k => k.CategoryId.HasValue && k.CategoryId.Value == categoryId.Value);
            //}

            //if (!string.IsNullOrEmpty(keyword))
            //{
            //    query = query.Where(k => k.Title.Contains(keyword) || k.Description.Contains(keyword));
            //}

            if (fromDate.HasValue && toDate.HasValue)
            {
                query = query.Where(k => k.FromDate >= fromDate.Value && k.FromDate <= toDate.Value);
            }

            else if (fromDate.HasValue)
            {
                query = query.Where(k => k.FromDate >= fromDate.Value);
            }
            else if (toDate.HasValue)
            {
                query = query.Where(k => k.FromDate <= toDate.Value);
            }

            totalCount = query.Count();

            return query.OrderByDescending(k => k.FromDate)
                .Skip(page * countPerPage)
                .Take(countPerPage)
                .ToList()
                .Select(k => new UserLog()
                {
                    ID = k.ID,
                    LogStatusCode = k.LogStatusCode,
                    FromDate = k.FromDate,
                    ToDate = k.ToDate,
                    UserId = k.UserId,
                    IPAddress = _mapper.Map<IpAddress>(k.IpAddress),
                    FullName = k.User.FirstName + " " + k.User.LastName,
                    UserName = k.User.UserName
                })
                .ToList();

            //totalPages = 0;
            //StringBuilder sqlGetUsersLogs = new StringBuilder();
            //sqlGetUsersLogs.Append(@"select CONCAT(FirstName,' ',LastName) as FullName, UserName ,FromDate ,ToDate ,Comments , UserLogging.IPAddress as IPAddress, ia.Title as IPAddressTitle
            //                            from UserLogging inner join AspNetUsers anu on UserId=anu.Id
            //                            left join IpAddresses ia on ia.Address = IPAddress
            //                                where fromdate between @fromDate and @toDate
            //                            order by fromdate desc");
            ////adding relation with user 

            //SqlCommand cmd = new SqlCommand(sqlGetUsersLogs.ToString());

            //cmd.Parameters.AddWithValue("@fromDate", string.IsNullOrEmpty(fromDate) || fromDate == "null" ? "0001-01-01" : fromDate);
            //cmd.Parameters.AddWithValue("@toDate", string.IsNullOrEmpty(toDate) || toDate == "null" ? "3001-01-01" : toDate);
            //var result = dataAccess.ToObjectList<DataContract.UserLog>(cmd);
            //totalPages = result.Count();
            //var records = result.Skip(page * countPerPage)
            //                     .Take(countPerPage)
            //                     .ToList();

            //return records;

        }

        public void EndActiveLog(string userId, UserLogStatus status)
        {
            var log = db.UserLogging.FirstOrDefault(k => k.UserId == userId && !k.ToDate.HasValue);

            if (log != null)
            {
                log.ToDate = DateTime.Now;
                log.LogStatusCode = (short)status;
                //db.UserLogging.Update(log);
                db.SaveChanges();
            }

        }

        //public List<User> UsersNotRegisteredTimeSheetActivityToday()
        //{
        //    string sql = @" select distinct anu.Id, FirstName, LastName 
        //                    from AspNetUsers anu
        //                    left join TimeSheets ts on UserId = anu.Id
        //                    left join TimeSheetProjects tsp on tsp.TimeSheetId=ts.ID
        //                    left join TimeSheetActivities tsa on tsa.TimeSheetProjectId=tsp.ID 
        //                    where GETDATE()  between ts.fromdate and ts.todate and tsa.ID  is null and istracked =1 
        //                    and anu.id not in (select distinct users.Id 
        //                                       from RequestedPermissions rp 
        //                                       inner join RequestedPermissionsStatuses rps on RequestedPermissionId = rp.ID
        //                                       inner join AspNetUsers users on users.Id = rp.ApplicationUserId
        //                                       where rp.id not in (select distinct RequestedPermissionId 
        //                                                           from RequestedPermissionsStatuses
        //                                                           where IsApproved <> 1)
        //                   and GETDATE() between rp.FromDate and rp.ToDate)";
        //    SqlCommand cmd = new SqlCommand(sql);

        //    return dataAccess.ToObjectList<User>(cmd);
        //}
        //public List<User> UsersNotHavingTimeSheetThisMonthYet()
        //{
        //    string sql = @"select anu.Id, FirstName, LastName 
        //                    from AspNetUsers anu
        //                    where anu.Id not in (select userid 
        //                                         from TimeSheets ts 
        //                                         where GETDATE()  between ts.fromdate and ts.todate )";

        //    SqlCommand cmd = new SqlCommand(sql);

        //    return dataAccess.ToObjectList<User>(cmd);
        //}
        //public List<User> GetAllUsers()
        //{
        //    return db.Users.ToList().Select(_mapper.Map<User>).ToList();
        //}

        #region supervising
        //public bool AddSupervising(string userId, string superVisedId)
        //{
        //    return AddSupervising(userId, new List<string>() { superVisedId });
        //}
        //public bool AddSupervising(string userId, List<string> superVisedIds)
        //{
        //    ApplicationUser SuperVisor = db.Users.FirstOrDefault(k => k.Id == userId);

        //    if (SuperVisor == null)
        //        return false;

        //    List<DataSets.Superviser> existingSuperVised = db.Supervisers.Where(k => k.SupervisorId == userId).ToList();

        //    superVisedIds = superVisedIds.Where(p => !existingSuperVised.Any(k => k.TeamId == p)).ToList();

        //    foreach (string id in superVisedIds)
        //    {
        //        db.Supervisers.Add(new DataSets.Superviser()
        //        {
        //            TeamId = id,
        //            SupervisorId = userId
        //        });
        //    }

        //    return db.SaveChanges() > 0;
        //}

        //public bool RemoveSuperVised(string userId, string superVisedId)
        //{
        //    return RemoveSuperVised(userId, new List<string>() { superVisedId });
        //}

        //public bool RemoveSuperVised(string userId, List<string> superVisedIds)
        //{
        //    ApplicationUser supervisor = db.Users.FirstOrDefault(k => k.Id == userId);
        //    if (supervisor == null)
        //        return false;
        //    List<DataSets.Superviser> supervising = db.Supervisers
        //        .Where(k => superVisedIds.Contains(k.TeamId) && k.SupervisorId == userId)
        //        .ToList();
        //    db.Supervisers.RemoveRange(supervising);
        //    return db.SaveChanges() > 0;
        //}

        #endregion

        #region Supervisors
        //public bool AddSupervisors(string userId, List<string> superVisorsIds)
        //{
        //    ApplicationUser employee = db.Users.FirstOrDefault(c => c.Id == userId);
        //    if (employee == null)
        //        return false;
        //    List<DataSets.Superviser> existingSupervisors = db.Supervisers.Where(k => k.TeamId == userId)
        //                                                                  .ToList();
        //    superVisorsIds = superVisorsIds.Where(p => !existingSupervisors.Any(c => c.SupervisorId == p))
        //                                   .ToList();
        //    foreach (string id in superVisorsIds)
        //    {
        //        db.Supervisers.Add(new DataSets.Superviser()
        //        {
        //            TeamId = userId,
        //            SupervisorId = id
        //        });
        //    }
        //    return db.SaveChanges() > 0;
        //}
        //public bool RemoveSuperVisors(string userId, List<string> superVisorsIds)
        //{
        //    ApplicationUser employee = db.Users.FirstOrDefault(c => c.Id == userId);
        //    if (employee == null)
        //        return false;
        //    List<DataSets.Superviser> existingSupervisors = db.Supervisers.Where(k => k.TeamId == userId)
        //                                                                  .ToList();
        //    List<DataSets.Superviser> supervisers = db.Supervisers.Where(k => k.TeamId == userId && superVisorsIds.Contains(k.SupervisorId)).ToList();
        //    db.Supervisers.RemoveRange(supervisers);

        //    return db.SaveChanges() > 0;

        //}
        //public object GetSupervisors(string Id)
        //{
        //    if (string.IsNullOrEmpty(Id))
        //        throw new ArgumentNullException();
        //    ApplicationUser employee = db.Users.FirstOrDefault(c => c.Id == Id);
        //    if (employee == null)
        //        throw new NullReferenceException();

        //    List<User> allUsers = GetAllUsers().Where(c => c.Id.ToString() != Id).ToList();
        //    //List <appli>
        //    string[] supervisorIds = db.Supervisers.Where(c => c.TeamId == Id)
        //                                                .Select(c => c.SupervisorId).ToArray();

        //    return new { All = allUsers, SuperVise = supervisorIds };
        //}




        #endregion

        //public object GetRoles(string Id)
        //{
        //    return null;
        //    //if (string.IsNullOrEmpty(Id))
        //    //    throw new ArgumentNullException();
        //    //ApplicationUser employee = db.Users.FirstOrDefault(c => c.Id == Id);
        //    //if (employee == null)
        //    //    throw new NullReferenceException();
        //    //List<IdentityRole> roles = _roleManager.Roles.ToList();
        //    //string[] rolesTakenIds = db.UserRoles.Where(c => c.UserId == Id).Select(c => c.RoleId).ToArray();
        //    //return new { All = roles, rolesTakes = rolesTakenIds };

        //}

        //public bool CheckIfEmployeeHasSubordinates(string userId)
        //{
        //    throw new NotImplementedException();

        //    //var subs = db.Supervisers.Where(c => c.SupervisorId == userId).ToList();
        //    //return subs.Count > 0;
        //}

        //public List<User> GetSubordinatesWithTimeSheets(string superVisorId)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<string> GetSupervisorsIdsIncludingParents(string forUserId, int levels)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool AddSupervising(string userId, string superVisedId)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool AddSupervising(string userId, List<string> superVisedIds)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RemoveSuperVised(string userId, string superVisedId)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RemoveSuperVised(string userId, List<string> superVisedIds)
        //{
        //    throw new NotImplementedException();
        //}

        //public object GetSupervising(string Id)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool AddSupervisors(string userId, List<string> superVisedIds)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool RemoveSuperVisors(string userId, List<string> superVisedIds)
        //{
        //    throw new NotImplementedException();
        //}

        //public object GetSupervisors(string Id)
        //{
        //    throw new NotImplementedException();
        //}

        public DateTime SetRole(string byUserId, string userId, short roleCode)
        {
            var dbUser = db.Users.FirstOrDefault(k => k.Id == userId);

            if (dbUser == null)
            {
                throw new ClientException("user dont exist");
            }

            // same role
            if (dbUser.RoleCode == roleCode)
            {
                return dbUser.RoleAssignedDate;
            }

            // user is in a team
            if (dbUser.TeamId.HasValue)
            {
                throw new ClientException("unable to change role, since user is a part of a team");
            }

            // check if supervisor
            if (db.Teams.Any(k => k.SupervisorId == userId))
            {
                throw new ClientException("unable to change role, since user is supervising a team");
            }

            // log
            db.UserRoleLogs.Add(new DataSets.UserRoleLog()
            {
                AssignedByUserId = dbUser.RoleAssignedByUserId,
                DateAssigned = dbUser.RoleAssignedDate,
                UserId = dbUser.Id,
                RoleCode = dbUser.RoleCode
            });


            dbUser.RoleAssignedDate = DateTime.Now;
            dbUser.RoleAssignedByUserId = byUserId;
            dbUser.RoleCode = roleCode;

            db.SaveChanges();

            return dbUser.RoleAssignedDate;
        }

        public bool HasSupervisorLog(string userId)
        {
            return db.SupervisorLogs.Any(k => k.UserId == userId);
        }

        public bool HasTimeSheets(string userId)
        {
            return db.TimeSheets.Any(k => k.UserId == userId);
        }

        public List<UserRoleLog> GetUserRoleLogs(string userId)
        {
            var logs = db.UserRoleLogs
              .AsNoTracking()
              .Include(k => k.User)
              .Include(k => k.AssignedByUser)
              .Where(k => k.UserId == userId)
              .Select(k => new UserRoleLog()
              {
                  RoleCode = k.RoleCode,
                  UserId = k.UserId,
                  AssignedByUserId = k.AssignedByUserId,
                  DateAssigned = k.DateAssigned,
                  User = new User()
                  {
                      Id = k.User.Id,
                      FirstName = k.User.FirstName,
                      LastName = k.User.LastName,
                  },
                  AssignedByUser = new User()
                  {
                      Id = k.AssignedByUser.Id,
                      FirstName = k.AssignedByUser.FirstName,
                      LastName = k.AssignedByUser.LastName,
                  }
              });

            return logs.ToList();
        }

        public UserRoleLog GetUserRole(string userId)
        {
            return db.Users
              //.AsNoTracking()
              //.Include(k => k.RoleAssignedByUser)
              .Where(k => k.Id == userId)
              .Select(k => new UserRoleLog()
              {
                  RoleCode = k.RoleCode,
                  UserId = k.Id,
                  AssignedByUserId = k.RoleAssignedByUserId,
                  DateAssigned = k.RoleAssignedDate,
                  AssignedByUser = k.RoleAssignedByUser == null ? null : new User()
                  {
                      Id = k.RoleAssignedByUser.Id,
                      FirstName = k.RoleAssignedByUser.FirstName,
                      LastName = k.RoleAssignedByUser.LastName,
                  }
              }).FirstOrDefault();
        }

        public TeamMemberOverview GetTeamMemberOverview(string userId)
        {
            if (!db.Users.Any(k => k.Id == userId && k.RoleCode == (short)ApplicationUserRole.TeamMember))
            {
                throw new ClientException("DONT_EXIST_OR_NOT_CLAIMED_ROLE");
            }

            var q_tsActivities = GetUserActivitiesQuery(userId);

            TeamMemberOverview overview = new TeamMemberOverview();

            overview.ActivitiesFrequency = GetUserActivitiesFrequency(q_tsActivities);
            overview.ActivitiesMinuts = GetUserActivitiesMinutes(q_tsActivities);
            overview.LatestActivities = GetUserLatestActivities(q_tsActivities);

            List<short> assignedTasksViewStatuses = new List<short>()
            {
                (short)ProjectTaskStatus.Pending,
                (short)ProjectTaskStatus.InProgress,
            };

            overview.AssignedTasks = db.ProjectTasks.Where(k =>
                        k.TimeSheetTasks.Any(t => t.TimeSheet.UserId == userId) &&
                        assignedTasksViewStatuses.Contains(k.StatusCode))
                .Select(TasksMethods.MapProjectTaskBasics)
                .ToList();

            return overview;
        }

        public SupervisorOverview GetSupervisorOverview(string userId, bool logsOnly)
        {
            if (!db.Users.Any(k => k.Id == userId && k.RoleCode == (short)ApplicationUserRole.Supervisor))
            {
                throw new ClientException("DONT_EXIST_OR_NOT_CLAIMED_ROLE");
            }

            SupervisorOverview overview = new SupervisorOverview();

            List<int> supervisingTeamsIds = db.Teams.Where(k => k.SupervisorId == userId)
                .Select(k => k.ID)
                .ToList();

            if (supervisingTeamsIds.Count == 0)
            {
                throw new ClientException("NO_SUPERVISING_TEAMS");
            }

            DateTime today = DateTime.Now.Date;


            // supervising users
            List<UserKeyValue> supervisingUsers = db.Users.Where(k => supervisingTeamsIds.Contains(k.TeamId.Value))
                    .Select(k => new UserKeyValue(k.Id, k.FirstName + " " + k.LastName))
                    .ToList();

            // s.users ids
            List<string> supervisingUsersIds = supervisingUsers.Select(k => k.Id).ToList();


            // today's logs
            overview.UserLogsToday = GetUsersLogsByDate(supervisingUsersIds, today);

            if (logsOnly)
            {
                return overview;
            }

            // s.users activities query
            var q_tsActivities = GetUserActivitiesQuery(supervisingUsersIds);

            // latest activities
            overview.LatestActivities = GetUserLatestActivities(q_tsActivities);



            // target from/to days
            int fromDateTargetDays = 30;
            DateTime fromDateTarget = today.AddDays(-fromDateTargetDays);

            // fill dates
            List<DateTime> dates = Enumerable.Range(1, fromDateTargetDays)
                      .Select(x => fromDateTarget.AddDays(x))
                      .ToList();

            // group activities under supervising users (last n days)

            // members activities
            var enum_membersActivities = q_tsActivities
                .Select(k => new { k.FromDate, k.ToDate, k.TimeSheetTask.TimeSheet.UserId, UserName = k.TimeSheetTask.TimeSheet.User.FirstName + " " + k.TimeSheetTask.TimeSheet.User.LastName })
                .Where(k => k.FromDate.Date >= fromDateTarget.Date)
                .GroupBy(k => k.UserId)
                .AsEnumerable();

            // frequencies foreach date in dates
            overview.MembersActivitiesFrequency = enum_membersActivities
                .Select(key => new MemberActivitiesFrequency()
                {
                    User = new UserKeyValue(key.Key, key.First().UserName),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date).Count()))
                        .ToList()
                })
                .ToList();

            // minutes foreach date in dates
            overview.MembersActivitiesMinutes = enum_membersActivities
                .Select(key => new MemberActivitiesFrequency()
                {
                    User = new UserKeyValue(key.Key, key.First().UserName),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date)
                               .Sum(x => (int)Math.Floor(((x.ToDate ?? DateTime.Now) - x.FromDate).TotalMinutes))))
                               .ToList()
                })
                .ToList();


            // group activities under supervising teams
            var enum_teamsActivities = q_tsActivities
                .Select(k => new { k.FromDate, k.ToDate, k.TimeSheetTask.TimeSheet.User.Team.Name, TeamId = k.TimeSheetTask.TimeSheet.User.TeamId.Value })
                .Where(k => k.FromDate.Date >= fromDateTarget.Date)
                .GroupBy(k => k.TeamId)
                .AsEnumerable();

            // frequencies foreach date in dates
            overview.TeamsActivitiesFrequency = enum_teamsActivities
                .Select(key => new TeamActivitiesFrequency()
                {
                    Team = new TeamKeyValue(key.Key, key.First().Name),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date).Count()))
                        .ToList()
                })
                .ToList();

            // minutes foreach date in dates
            overview.TeamsActivitiesMinutes = enum_teamsActivities
                .Select(key => new TeamActivitiesFrequency()
                {
                    Team = new TeamKeyValue(key.Key, key.First().Name),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date)
                               .Sum(x => (int)Math.Floor(((x.ToDate ?? DateTime.Now) - x.FromDate).TotalMinutes))))
                               .ToList()
                })
                .ToList();


            return overview;
        }

        public AdminOverview GetAdminOverview(string userId, bool logsAndCountersOnly)
        {
            if (!db.Users.Any(k => k.Id == userId && k.RoleCode == (short)ApplicationUserRole.Admin))
            {
                throw new ClientException("DONT_EXIST_OR_NOT_CLAIMED_ROLE");
            }

            AdminOverview overview = new AdminOverview();

            // logged in user
            overview.LoggedInUsers = db.UserLogging.Where(k =>
                    k.LogStatusCode == (short)UserLogStatus.Login && !k.ToDate.HasValue)
                    .GroupBy(k => k.User.RoleCode)
                    .AsEnumerable()
                    .Select(k => new KeyValuePair<string, int>(((ApplicationUserRole)k.Key).ToString(), k.Count()))
                    .ToList();

            var appRoles = Enum.GetValues(typeof(ApplicationUserRole)).Cast<ApplicationUserRole>();

            foreach (ApplicationUserRole role in appRoles)
            {
                string roleKey = role.ToString();

                if (!overview.LoggedInUsers.Any(k => k.Key == roleKey))
                {
                    overview.LoggedInUsers.Add(new KeyValuePair<string, int>(roleKey, 0));
                }
            }

            // projects performance


            DateTime today = DateTime.Now.Date;

            overview.UserLogsToday = GetUsersLogsByDate(today);


            if (logsAndCountersOnly)
            {
                return overview;
            }

            // s.users activities query
            var q_tsActivities = GetUserActivitiesQuery();


            // target from/to days
            int fromDateTargetDays = 30;
            DateTime fromDateTarget = today.AddDays(-fromDateTargetDays);

            // fill dates
            List<DateTime> dates = Enumerable.Range(1, fromDateTargetDays)
                      .Select(x => fromDateTarget.AddDays(x))
                      .ToList();


            // group activities under supervising teams
            var enum_teamsActivities = q_tsActivities
                .Select(k => new { k.FromDate, k.ToDate, k.TimeSheetTask.TimeSheet.User.Team.Name, TeamId = k.TimeSheetTask.TimeSheet.User.TeamId.Value })
                .Where(k => k.FromDate.Date >= fromDateTarget.Date)
                .GroupBy(k => k.TeamId)
                .AsEnumerable();

            // frequencies foreach date in dates
            overview.TeamsActivitiesFrequency = enum_teamsActivities
                .Select(key => new TeamActivitiesFrequency()
                {
                    Team = new TeamKeyValue(key.Key, key.First().Name),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date).Count()))
                        .ToList()
                })
                .ToList();

            // minutes foreach date in dates
            overview.TeamsActivitiesMinutes = enum_teamsActivities
                .Select(key => new TeamActivitiesFrequency()
                {
                    Team = new TeamKeyValue(key.Key, key.First().Name),
                    Activities = dates.Select(date => new KeyValuePair<DateTime, int>(date,
                            key.Where(a => a.FromDate.Date == date)
                               .Sum(x => (int)Math.Floor(((x.ToDate ?? DateTime.Now) - x.FromDate).TotalMinutes))))
                               .ToList()
                })
                .ToList();

            //List<short> projectsTargetStatuses = new List<short>()
            //{
            //    (short)ProjectStatus.Done,
            //}

            // status in ()
            overview.Projects = db.Projects.OrderByDescending(k => k.ID)
                                           .Take(10)
                                           .Select(k => new ProjectDashboardView()
                                           {
                                               ID = k.ID,
                                               StatusCode = k.StatusCode,
                                               Title = k.Title,
                                               TasksPerformance = new TasksPerformance()
                                               {
                                                   TotalCount = k.Tasks.Count(),
                                                   DoneCount = k.Tasks.Count(t => t.StatusCode == (short)ProjectTaskStatus.Done),
                                                   ProgressCount = k.Tasks.Count(t => t.StatusCode == (short)ProjectTaskStatus.InProgress),
                                                   PendingCount = k.Tasks.Count(t => t.StatusCode == (short)ProjectTaskStatus.Pending),
                                                   FailedOrTerminatedCount = k.Tasks.Count(t => t.StatusCode == (short)ProjectTaskStatus.Failed || t.StatusCode == (short)ProjectTaskStatus.Terminated),
                                               }
                                           })
                                           .ToList();


            overview.ProjectsPerformance = new Models.Projects.ProjectsPerformance()
            {
                TotalCount = db.Projects.Count(),
                DoneCount = db.Projects.Count(t => t.StatusCode == (short)ProjectTaskStatus.Done),
                ProgressCount = db.Projects.Count(t => t.StatusCode == (short)ProjectTaskStatus.InProgress),
                ProposedCount = db.Projects.Count(t => t.StatusCode == (short)ProjectTaskStatus.Pending),
                FailedOrTerminatedCount = db.Projects.Count(t => t.StatusCode == (short)ProjectTaskStatus.Failed || t.StatusCode == (short)ProjectTaskStatus.Terminated),
            };

            return overview;
        }

        private List<UserLog> GetUsersLogsByDate(List<string> supervisingUsersIds, DateTime date)
        {
            return db.UserLogging
                            .Where(k => supervisingUsersIds.Contains(k.UserId) && k.FromDate.Date == date)
                            .OrderByDescending(k => k.ID)
                            .Select(MapUserLogForOverview)
                            .ToList();
        }

        private List<UserLog> GetUsersLogsByDate(DateTime date)
        {
            return db.UserLogging
                            .Where(k => k.FromDate.Date == date)
                            .OrderByDescending(k => k.ID)
                            .Select(MapUserLogForOverview)
                            .ToList();
        }

        public static Expression<Func<DataSets.UserLog, UserLog>> MapUserLogForOverview =>
      k => new UserLog()
      {
          ID = k.ID,
          FromDate = k.FromDate,
          ToDate = k.ToDate,
          LogStatusCode = k.LogStatusCode,
          UserId = k.UserId,
          UserName = k.User.FirstName + " " + k.User.LastName,
      };

        public UserInsights GetUserInsights(string userId)
        {
            if (!db.Users.Any(k => k.Id == userId))
            {
                throw new ClientException("user dont exist");
            }

            var q_tsActivities = GetUserActivitiesQuery(userId);

            UserInsights overview = new UserInsights();

            overview.ActivitiesFrequency = GetUserActivitiesFrequency(q_tsActivities);

            overview.ActivitiesMinuts = GetUserActivitiesMinutes(q_tsActivities);
            overview.LatestActivities = GetUserLatestActivities(q_tsActivities);

            IQueryable<DataSets.ProjectTask> q_tasks = GetUserTasksQuery(userId);
            overview.TasksPerformance = GetUserTasksPerformance(q_tasks);

            overview.LatestLogs = GetLatestUserLogs(userId, 10);

            overview.ActiveMinuts = db.UserLogging
                .Where(k => k.UserId == userId)
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.FromDate.Date)
                .Take(30)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<DateTime, int>(key.Key, (int)Math.Floor(key.Sum(a => ((a.ToDate ?? DateTime.Now) - a.FromDate).TotalMinutes))))
                .ToList();

            //overview.UserActionLogs = new List<UserActionLog>();
            // projects added
            // timesheets added
            // teams added


            return overview;
        }

        private TasksPerformance GetUserTasksPerformance(IQueryable<DataSets.ProjectTask> q_tasks)
        {
            return new TasksPerformance()
            {
                TotalCount = q_tasks.Count(),
                DoneCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Done),
                ProgressCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.InProgress),
                PendingCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Pending),
                FailedOrTerminatedCount = q_tasks.Count(k => k.StatusCode == (short)ProjectTaskStatus.Failed || k.StatusCode == (short)ProjectTaskStatus.Terminated),
            };
        }

        private IQueryable<DataSets.ProjectTask> GetUserTasksQuery(string userId)
        {
            return db.ProjectTasks
                            .Where(k => k.TimeSheetTasks.Any(t => t.TimeSheet.UserId == userId));
        }

        private IQueryable<DataSets.TimeSheetActivity> GetUserActivitiesQuery(string userId)
        {
            return db.TimeSheetActivities.Where(k => !k.DeletedAt.HasValue && k.TimeSheetTask.TimeSheet.UserId == userId);
        }

        private IQueryable<DataSets.TimeSheetActivity> GetUserActivitiesQuery()
        {
            return db.TimeSheetActivities.Where(k => !k.DeletedAt.HasValue);
        }

        private IQueryable<DataSets.TimeSheetActivity> GetUserActivitiesQuery(List<string> userIds)
        {
            return db.TimeSheetActivities.Where(k => !k.DeletedAt.HasValue && userIds.Contains(k.TimeSheetTask.TimeSheet.UserId));
        }

        private List<KeyValuePair<DateTime, int>> GetUserActivitiesFrequency(IQueryable<DataSets.TimeSheetActivity> q_tsActivities)
        {
            return q_tsActivities
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.FromDate.Date)
                .Take(30)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<DateTime, int>(key.Key, key.Count()))
                .ToList();
        }

        private List<KeyValuePair<DateTime, int>> GetUserActivitiesMinutes(IQueryable<DataSets.TimeSheetActivity> q_tsActivities)
        {
            return q_tsActivities
                .OrderByDescending(k => k.FromDate)
                .GroupBy(k => k.FromDate.Date)
                .Take(30)
                .AsEnumerable()
                .Select((key) => new KeyValuePair<DateTime, int>(key.Key, (int)Math.Floor(key.Sum(a => ((a.ToDate ?? DateTime.Now) - a.FromDate).TotalMinutes))))
                .ToList();
        }

        private List<TimeSheetActivity> GetUserLatestActivities(IQueryable<DataSets.TimeSheetActivity> q_tsActivities, int take = 10)
        {
            return q_tsActivities
                .OrderByDescending(k => k.ID)
                .Take(take)
                .Select(k => new TimeSheetActivity()
                {
                    ID = k.ID,
                    FromDate = k.FromDate,
                    ToDate = k.ToDate,
                    User = new User()
                    {
                        Id = k.TimeSheetTask.TimeSheet.User.Id,
                        FirstName = k.TimeSheetTask.TimeSheet.User.FirstName,
                        LastName = k.TimeSheetTask.TimeSheet.User.LastName,
                    },
                    ProjectTask = new ProjectTask()
                    {
                        ID = k.TimeSheetTask.ProjectTask.ID,
                        Title = k.TimeSheetTask.ProjectTask.Title,
                        StatusCode = k.TimeSheetTask.ProjectTask.StatusCode,
                        TimeSheetId = k.TimeSheetTask.TimeSheetId,
                    }
                })
                .ToList();
        }

        public UserLog GetLatestUserLog(string userId)
        {
            return db.UserLogging
                .Where(k => k.UserId == userId)
                .OrderByDescending(k => k.ID)
                .Select(_mapper.Map<UserLog>)
                .FirstOrDefault();
        }

        public List<UserLog> GetLatestUserLogs(string userId, int take)
        {
            return db.UserLogging
                .Where(k => k.UserId == userId)
                .OrderByDescending(k => k.ID)
                .Take(take)
                .Select(_mapper.Map<UserLog>)
                .ToList();
        }

    }
}
