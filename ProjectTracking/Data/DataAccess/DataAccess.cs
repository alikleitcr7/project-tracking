using ProjectTracking.AppStart;
using ProjectTracking.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ProjectTracking.Data.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public T ToObject<T>(SqlCommand cmd) where T : new()

        {
            DataTable dt = GetDataTable(cmd);

            if (dt != null && dt.Rows.Count == 1)
            {
                using (dt)
                {
                    return dt.Rows[0].ToObject<T>();
                }
            }

            return default(T);
        }

        public List<T> ToObjectList<T>(DataTable dt) where T : new()
        {
            List<T> records = new List<T>();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    records.Add(row.ToObject<T>());
                }
            }

            return records;
        }

        public List<T> ToObjectList<T>(SqlCommand cmd) where T : new()

        {
            DataTable dt = GetDataTable(cmd);

            List<T> records = new List<T>();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    records.Add(row.ToObject<T>());
                }
            }

            return records;
        }

        public DataTable GetDataTable(SqlCommand cmd)
        {
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    cmd.Connection = con;

                    DataTable dt = new DataTable("Temp");

                    using (var adapt = new SqlDataAdapter(cmd))
                    {
                        adapt.Fill(dt);
                    }

                    return dt;

                }
                catch (Exception ex)
                {
                    StackTrace stackTrace = new StackTrace();
                    var frame = stackTrace.GetFrame(1);

                    throw new Exception(ex.Message + "\n\n StackTrace:\nMethod: " + frame.GetMethod().Name + "\nCMD\t " + cmd.CommandText);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public bool ExecuteNonQuery(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (cmd)
                {
                    conn.Open();
                    cmd.Connection = conn;

                    try
                    {
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        public object ExecuteScalare(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (cmd)
                {
                    conn.Open();
                    cmd.Connection = conn;

                    try
                    {
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }


        public static readonly string SqlDateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff";

        public static readonly string SqlDateFormat = "yyyy-MM-dd";
    }
}
