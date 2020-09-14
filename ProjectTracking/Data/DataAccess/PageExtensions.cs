using System;

namespace ProjectTracking.Data.DataAccess
{
    public static class PageExtensions
    {
      
        public static int? ToInt(this object obj)
        {
            if (obj != DBNull.Value && obj != null && int.TryParse(obj.ToString(), out int number))
            {
                return number;
            }

            return null;
        }

        public static short? ToShort(this object obj)
        {
            if (obj != DBNull.Value && obj != null && short.TryParse(obj.ToString(), out short number))
            {
                return number;
            }

            return null;
        }

        public static DateTime? ToDateTime(this object obj)
        {
            if (obj != DBNull.Value && obj != null && DateTime.TryParse(obj.ToString(), out DateTime date))
            {
                return date;
            }

            return null;
        }

        public static string ToQueryCondition(this bool condition)
        {
            return "'" + (condition ? "1" : "0") + "'";
        }
      
       

        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }


    }
}
