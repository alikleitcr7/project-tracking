using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ProjectTracking.Data.DataAccess
{
    public interface IDataAccess
    {
        bool ExecuteNonQuery(SqlCommand cmd);
        object ExecuteScalare(SqlCommand cmd);
        DataTable GetDataTable(SqlCommand cmd);
        T ToObject<T>(SqlCommand cmd) where T : new();
        List<T> ToObjectList<T>(DataTable dt) where T : new();
        List<T> ToObjectList<T>(SqlCommand cmd) where T : new();
    }
}