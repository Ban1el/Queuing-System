using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;

namespace QueueAPI.Helpers
{
    public class DBConnection
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["QueueDB"].ConnectionString;
        public void SetConnectionString(DBConnectionName connection)
        {
            switch (connection)
            {
                case DBConnectionName.QueueDB:
                    ConnectionString = ConfigurationManager.ConnectionStrings["QueueDB"].ConnectionString;
                    break;

                default:
                    ConnectionString = ConfigurationManager.ConnectionStrings["QueueDB"].ConnectionString;
                    break;
            }
        }

        public DataTable Read(string sp_name, List<SqlParameter> parameters = null, DBConnectionName cs = DBConnectionName.QueueDB)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sp_name, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            dt.Load(sdr); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            return dt;
        }

        public int Execute(string sp_name, List<SqlParameter> parameters = null, DBConnectionName cs = DBConnectionName.QueueDB)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sp_name, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();
                        affectedRows = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            return affectedRows;
        }
    }
}