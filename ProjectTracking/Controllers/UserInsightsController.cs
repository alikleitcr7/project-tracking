using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Models.Statistics;
using ProjectTracking.Data.Methods.Interfaces;
using ProjectTracking.Data.Methods.Interfaces.Statistics;

namespace ProjectTracking.Controllers
{
    public class UserInsightsController : Controller
    {
        private readonly IInsightsMethods _insights;
        public UserInsightsController(IInsightsMethods insights)
        {
            _insights = insights;
        }

        public IActionResult Index()
        {
            return View();
        }

        public object UserMonthlyActivities(string userId, bool monthly, bool daily, int? year, int? month)
        {
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }

            List<UserMonthlyActivities> insights = _insights.GetUserMonthlyActivities(userId, monthly, daily);

            if (monthly && !daily && year.HasValue)
            {
                return new
                {
                    insights = insights.Where(k => k.Year == year.Value),
                    years = insights.Select(k => k.Year).Distinct().ToList(),
                    months = insights.Where(k => k.Year == year.Value).Select(k => k.Month).Distinct().ToList()
                };
            }

            if (daily && month.HasValue && year.HasValue)
            {
                return new
                {
                    insights = insights.Where(k => k.Month == month && k.Year == year.Value),
                    years = insights.Select(k => k.Year).Distinct().ToList(),
                    months = insights.Where(k => k.Year == year.Value).Select(k => k.Month).Distinct().ToList()
                };
            }

            return new
            {
                insights,
                years = insights.Select(k => k.Year).Distinct().ToList()
            };
        }

    }
}