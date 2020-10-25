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
            return date.ToString("d MMM yyyy");
        }

        public static string ToDisplayDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDisplayDate() : "-";
        }

        public static string ToDisplayDateTime(this DateTime date, bool includeSeconds = false)
        {
            return date.ToString($"d MMM yyyy h:m{(includeSeconds? ":s" :"")} tt");
        }
        public static string ToDisplayDateTime(this DateTime? date, bool includeSeconds = false)
        {
            return date.HasValue ? date.Value.ToDisplayDateTime(includeSeconds) : "-";
        }
    }
}
