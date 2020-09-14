using System;
using System.Data;
using System.Reflection;

namespace ProjectTracking.Data.DataAccess
{
    public static class DataRowExtensions
    {
        //public static T FieldOrDefault<T>(this DataRow row, string columnName)
        //{
        //    return row.IsNull(columnName) ? default(T) : row.Field<T>(columnName);
        //}
        public static T ToObject<T>(this DataRow row, string prefix = "")
          where T : new()
        {
            T item = new T();

            foreach (DataColumn column in row.Table.Columns)
            {

                string columnName = column.ColumnName;

                if (!string.IsNullOrEmpty(prefix))
                {
                    int idx = columnName.IndexOf("_");

                    if (idx != -1 && columnName.Split('_')[0].ToLower().Equals(prefix.ToLower()))
                    {
                        columnName = columnName.Substring(idx + 1, columnName.Length - idx - 1);
                    }
                    else
                    {
                        continue;

                    }
                }

                PropertyInfo property = item.GetType().GetProperty(columnName);

                if (property != null && row[column] != DBNull.Value)
                {
                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;


                    if (t.IsEnum)
                    {
                        object safeValue = (row[column] == null) ? null : Enum.ToObject(t, row[column]);

                        property.SetValue(item, safeValue, null);
                    }
                    else
                    {
                        object safeValue = (row[column] == null) ? null : Convert.ChangeType(row[column], t);

                        //object safeValue = (row[column] == null) ? null : Convert.ChangeType(row[column], t);
                        property.SetValue(item, safeValue, null);

                        //object result = Convert.ChangeType(row[column], property.PropertyType);
                        //property.SetValue(item, result, null);
                    }
                }
            }

            return item;
        }
    }
}
