using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTracking.DataContract
{
    public static class DateTimeExtensions
    {
        public static string ToDisplayDate(this DateTime date)
        {
            return date.ToString("dd-MM-yyyy");
        }

        public static string ToDisplayDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDisplayDate() : "-";
        }
    }
}
