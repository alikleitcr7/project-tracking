using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Models.Statistics;
using ProjectTracking.Data.DataAccess;
using ProjectTracking.Data.Methods.Interfaces.Statistics;
using ProjectTracking.DataContract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracking.Data.Methods.Statistics
{
    public class InsightsMethods : IInsightsMethods
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IDataAccess dataAccess;

        public InsightsMethods(ApplicationDbContext dbContext, IMapper mapper, IDataAccess dataAccess)
        {
            _db = dbContext;
            _mapper = mapper;
            this.dataAccess = dataAccess;
        }

        public List<UserMonthlyActivities> GetUserMonthlyActivities(string userId, bool includeMonth = true, bool includeDay = false)
        {

            List<KeyValuePair<string, string>> projectionColumns = new List<KeyValuePair<string, string>>();

            projectionColumns.Add(new KeyValuePair<string, string>("YEAR(a.FromDate)", "Year"));

            if (includeMonth)
            {
                projectionColumns.Add(new KeyValuePair<string, string>("DATEPART(MONTH,a.FromDate)", "Month"));
            }

            if (includeDay)
            {
                projectionColumns.Add(new KeyValuePair<string, string>("DATEPART(DAY,a.FromDate)", "Day"));
            }

            string selectColumns = string.Join(",", projectionColumns.Select(k => $"{k.Key} '{k.Value}'").ToArray());
            string groupByColumns = string.Join(",", projectionColumns.Select(k => k.Key).ToArray());
            string orderByColumns = string.Join(",", projectionColumns.Select(k => $"{k.Key} asc").ToArray());

            StringBuilder query = new StringBuilder();

            query.AppendLine($"select {selectColumns},  sum(DATEDIFF(HOUR,a.FromDate,a.ToDate)) as 'TotalHours'");
            query.AppendLine($"from TimeSheetActivities a");
            query.AppendLine($"inner join TimeSheetProjects tsp on tsp.ID = a.TimeSheetProjectId");
            query.AppendLine($"inner join TimeSheets t on t.ID = tsp.TimeSheetId");
            query.AppendLine($"where t.UserId = @userId");
            query.AppendLine($"group by {groupByColumns}");
            query.AppendLine($"order by {orderByColumns}");

            SqlCommand cmd = new SqlCommand(query.ToString());

            cmd.Parameters.AddWithValue("@userId", userId);

            return dataAccess.ToObjectList<UserMonthlyActivities>(cmd);
        }


        public List<UserLog> GetOnlineUsers()
        {
            return _db.UserLogging.Include(k => k.User)
                .Where(k => !k.ToDate.HasValue)
                .ToList()
                .Select(_mapper.Map<UserLog>)
                .ToList();
        }


        public List<TimeSheetActivity> GetOnGoingActivities()
        {
            List<TimeSheetActivity> records = _db.TimeSheetActivities
                      .Include(k => k.TimeSheetTask)
                      .Where(k => !k.ToDate.HasValue)
                      .ToList()
                      .Select(_mapper.Map<TimeSheetActivity>)
                      .ToList();

            //TimeSheetActivitiesMethods.PopulateIpAddresses(records, _db.IpAddresses.ToList());

            return records;
        }
    }
}
