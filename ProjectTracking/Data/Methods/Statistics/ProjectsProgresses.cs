using Microsoft.EntityFrameworkCore;
using ProjectTracking.Data.DataAccess;
using ProjectTracking.Data.Methods.Interfaces.Statistics;
using ProjectTracking.Models.Statistics;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProjectTracking.Data.Methods.Statistics
{
    public class ProjectsProgresses : IProjectsStatistics
    {
        private ApplicationDbContext db;
        private readonly IDataAccess dataAccess;

        public ProjectsProgresses(ApplicationDbContext context, IDataAccess dataAccess)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Setting.ConnectionString);
            //db = new ApplicationDbContext(optionsBuilder.Options);
            db = context;
            this.dataAccess = dataAccess;
        }
        public List<ProjectsProgress> GetProjectsProgress(bool byYear, bool byYearAndMonth, string userId = null)
        {

            StringBuilder sqlSelect = new StringBuilder();
            StringBuilder sqlParentProjectJoin = new StringBuilder();
            StringBuilder sqlParentProjectGroupBy = new StringBuilder();


            StringBuilder sqlGroupBy = new StringBuilder();
            StringBuilder orderBy = new StringBuilder();
            StringBuilder sqlGetProjectsProgress = new StringBuilder();
            sqlParentProjectJoin.Append("select  pr.title as ProjectTitle,sum(Hours) 'Hours',sum(number) 'Number',MeasurementUnit ");
            //initial select
            sqlSelect.Append(
             @"
					 from (select p.ID ,p.ParentId , title as ProjectTitle,case
                    			when sum(number) is null then 0 
                    			else sum(number)
                    		    end 'Number',sum(DATEDIFF(HOUR,tsa.FromDate,tsa.ToDate))'Hours',
                    		case when	name is null then 'none' else name end 'MeasurementUnit' ");
            //initial group by
            sqlGroupBy.Append(" group by title,name");
            sqlParentProjectGroupBy.Append("   group by title, MeasurementUnit");
            //adding year
            if (byYear)
            {
                sqlParentProjectJoin.Append(",a.Year ");

                sqlSelect.Append(",Year(tsa.FromDate)'Year'");
                sqlGroupBy.Append(",Year(tsa.FromDate)");
                orderBy.Append(" order by Year desc ,number desc");
                sqlParentProjectGroupBy.Append(",Year");

            }
            //adding month
            if (byYearAndMonth)
            {
                sqlParentProjectJoin.Append(",a.Month");

                sqlSelect.Append(",DATEPART(MONTH,tsa.fromdate) 'Month'");
                sqlGroupBy.Append(",DATEPART(MONTH,tsa.fromdate)");
                orderBy.Clear();
                orderBy.Append(" order by Year desc , month desc,number desc");
                sqlParentProjectGroupBy.Append(",Month ");

            }
            sqlGroupBy.Append(",p.ID ,p.ParentId ) a ");
            sqlSelect = sqlParentProjectJoin.Append(sqlSelect);
            //appending joins
            sqlSelect.Append(
                 @" from projects p 
                        inner join TimeSheetProjects tsp on p.ID = tsp.ProjectId
                        inner join TimeSheetActivities tsa on tsa.TimeSheetProjectId=tsp.ID
                        left join MeasurementUnit mu on tsa.MeasurementUnitId = mu.ID "
                        );
            //appending user relationship

            if (!string.IsNullOrEmpty(userId))
                sqlSelect.Append(@" inner join TimeSheets ts on ts.ID = tsp.TimeSheetId inner join AspNetUsers anu on anu.Id= ts.UserId 
            where anu.Id=@userId and tsa.ToDate is not null ");
            else
                sqlSelect.Append(@" where tsa.ToDate is not null ");

            sqlGetProjectsProgress.Append(sqlSelect);
            sqlGetProjectsProgress.Append(sqlGroupBy);
            sqlGetProjectsProgress.Append(@"inner join Projects pr on pr.ID=a.ParentId");
            sqlGetProjectsProgress.Append(sqlParentProjectGroupBy);
            sqlGetProjectsProgress.Append(orderBy);
            //inner join parent project table

            SqlCommand cmd = new SqlCommand(sqlGetProjectsProgress.ToString());
            if (!string.IsNullOrEmpty(userId))
                cmd.Parameters.AddWithValue("@userId", userId);

            return dataAccess.ToObjectList<ProjectsProgress>(cmd);
        }

        public List<MeasurementUnitsProgressTotal> GetMeasurementUnitsTotalProgress(string userId, string year, string month, string day)
        {
            StringBuilder dateFilter = new StringBuilder();
            if (!string.IsNullOrEmpty(year))
                dateFilter.Append($"{year}-");
            if (!string.IsNullOrEmpty(month))
                dateFilter.Append($"{int.Parse(month).ToString("D2")}-");
            if (!string.IsNullOrEmpty(day))
                dateFilter.Append($"{int.Parse(day).ToString("D2")}");

            dateFilter.Append("%");
            StringBuilder sqlGetProjectsProgress = new StringBuilder();
            sqlGetProjectsProgress.Append(@"select  sum(DATEDIFF(HOUR,tsa.FromDate,tsa.ToDate))'Hours',
            case
			when sum(number) is null then 0 
			else sum(number)
		    end 'Number',
	case when	name is null then 'Unspecified Units' else name end as 'MeasurementUnit'
                from projects p 
               	left join TimeSheetProjects tsp on p.ID = tsp.ProjectId
	left join TimeSheetActivities tsa on tsa.TimeSheetProjectId=tsp.ID
	left join MeasurementUnit mu on tsa.MeasurementUnitId = mu.ID");
            //adding relation with user 
            if (!string.IsNullOrEmpty(userId))
                sqlGetProjectsProgress.Append(@" 	inner join TimeSheets ts on ts.ID = tsp.TimeSheetId
	left join AspNetUsers anu on anu.Id= ts.UserId where anu.Id=@userId and tsa.FromDate like @filterDate and tsa.ToDate is not null");
            else
                sqlGetProjectsProgress.Append(@" where tsa.FromDate like @filterDate  and  tsa.ToDate is not null");
            sqlGetProjectsProgress.Append(" group by name");

            SqlCommand cmd = new SqlCommand(sqlGetProjectsProgress.ToString());
            if (!string.IsNullOrEmpty(userId))
                cmd.Parameters.AddWithValue("@userId", userId);

            cmd.Parameters.AddWithValue("@filterDate", dateFilter.ToString());

            return dataAccess.ToObjectList<MeasurementUnitsProgressTotal>(cmd);
        }
     
        #region progressYearsMonthsDays
        public List<int> GetProjectProgressYears()
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => new { tsRelation, timesheet })
                                                        
                                                 .Select(c => c.tsRelation.tsa.FromDate.Year).Distinct().ToList();


            return dbProjects;
        }
        public List<int> GetProjectProgressYearsByUser(string userId)
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => new { tsRelation, timesheet })
                                                 .Join(db.Users,
                                                        userRelation => userRelation.timesheet.UserId,
                                                        user => user.Id,
                                                        (userRelation, user) => new { userRelation, user })
                                                 .Where(c => c.user.Id == userId )
                                                 .Select(c => c.userRelation.tsRelation.tsa.FromDate.Year).Distinct().ToList();


            return dbProjects;
        }
        public List<int> GetProjectProgressMonthsByYear(int year)
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => new { tsRelation, timesheet })
                                                        .Where(c => c.tsRelation.tsa.FromDate.Year == year )
                                                 .Select(c => c.tsRelation.tsa.FromDate.Month).Distinct().ToList();


            return dbProjects;
        }
        public List<int> GetProjectProgressMonthsByYearAndUser(string userId, int year)
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => new { tsRelation, timesheet })
                                                 .Join(db.Users,
                                                        userRelation => userRelation.timesheet.UserId,
                                                        user => user.Id,
                                                        (userRelation, user) => new { userRelation, user })
                                                 .Where(c => c.user.Id == userId && c.userRelation.tsRelation.tsa.FromDate.Year == year )
                                                 .Select(c => c.userRelation.tsRelation.tsa.FromDate.Month).Distinct().ToList();


            return dbProjects;
        }
        public List<int> GetProjectProgressDaysByMonthAndYear(int year, int month)
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => new { tsRelation, timesheet })
                                                        .Where(c => c.tsRelation.tsa.FromDate.Year == year && c.tsRelation.tsa.FromDate.Month == month)
                                                 .Select(c => c.tsRelation.tsa.FromDate.Day).Distinct().ToList();


            return dbProjects;
        }
        public List<int> GetProjectProgressDaysByMonthAndYearAndUser(string userId, int year, int month)
        {
            var dbProjects = db.TimeSheetTasks.Join(db.TimeSheetActivities,
                                                       tsp => tsp.ID, tsa => tsa.TimeSheetTask.ID,
                                                       (tsp, tsa) => new { tsp, tsa })
                                                 .Join(db.TimeSheets,
                                                        tsRelation => tsRelation.tsp.TimeSheetId,
                                                        timesheet => timesheet.ID, (tsRelation, timesheet) => 
                                                                    new { tsRelation, timesheet })
                                                 .Join(db.Users,
                                                        userRelation => userRelation.timesheet.UserId,
                                                        user => user.Id,
                                                        (userRelation, user) => new { userRelation, user })
                                                  .Where(c => c.user.Id == userId && 
                                                         c.userRelation.tsRelation.tsa.FromDate.Year == year && 
                                                         c.userRelation.tsRelation.tsa.FromDate.Month == month )
                                                 .Select(c => c.userRelation.tsRelation.tsa.FromDate.Day)
                                                 .Distinct().ToList();
            return dbProjects;
        }
        #endregion
        
    }
}
