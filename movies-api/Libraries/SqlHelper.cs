using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace movies_api.Libraries
{
    public class SqlHelper
    {
        public static string ConnString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";
        public string _connectString = "";
        public string _activityString = "";

        public string ConnectString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }
        public string ActivityConnectString
        {
            get
            {
                return _activityString;
            }
        }

        private static SqlHelper _instance;
        private static object _lock = new object();
        public static SqlHelper GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlHelper();
                    }
                }
            }
            return _instance;
        }

        private static SqlCommand CreateCMD(string cmdText, SqlParameter[] prms, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            if (prms != null)
                cmd.Parameters.AddRange(prms);
            if (conn != null)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            return cmd;
        }

        public static object ExecuteScalar(SqlConnection conn, string safeSql, params SqlParameter[] prms)
        {
            SqlCommand cmd = new SqlCommand(safeSql.Trim(), conn);
            if (prms != null && prms.Length > 0)
            {
                cmd.Parameters.AddRange(prms);
            }

            cmd.CommandTimeout = 180;
            cmd.Connection = conn;
            return cmd.ExecuteScalar();
        }

        public static object ExecuteScalar(SqlConnection conn, SqlCommand cmd, string cmdText, params SqlParameter[] prms)
        {
            if (conn == null)
                return 0;

            if (prms != null)
                cmd.Parameters.AddRange(prms);
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 180;
            object obj = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return obj;
        }

        public static SqlDataReader ExecuteReader(SqlConnection conn, string safeSql, params SqlParameter[] prms)
        {
            SqlCommand cmd = new SqlCommand(safeSql.Trim(), conn);
            if (prms != null && prms.Length > 0)
            {
                cmd.Parameters.AddRange(prms);
            }

            cmd.CommandTimeout = 180;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }

        public static SqlDataReader ExecuteProcedureReader(SqlConnection conn, string procedureName, params SqlParameter[] prms)
        {
            SqlCommand cmd = new SqlCommand(procedureName.Trim(), conn);
            cmd.CommandType = CommandType.StoredProcedure;
            if (prms != null && prms.Length > 0)
            {
                cmd.Parameters.AddRange(prms);
            }

            cmd.CommandTimeout = 180;
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return reader;
        }

        public static int Execute(SqlConnection conn, SqlCommand cmd, string cmdText, params SqlParameter[] prms)
        {
            if (conn == null)
                return 0;

            if (prms != null)
                cmd.Parameters.AddRange(prms);
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 180;
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
        }

        public static int Execute(SqlConnection conn, string cmdText, params SqlParameter[] prms)
        {
            SqlCommand cmd = CreateCMD(cmdText, prms, conn);

            cmd.CommandTimeout = 180;
            int num = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return num;
        }
    }
}