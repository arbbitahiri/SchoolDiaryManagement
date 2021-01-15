using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL.DataBase
{
    class DataConnection
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["diaryConnStr"].ConnectionString;

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static SqlCommand GetCommand(SqlConnection connection, string cmdText, CommandType commandType)
        {
            SqlCommand command = new SqlCommand(cmdText, connection)
            {
                CommandType = commandType
            };
            return command;
        }

        public static void AddParameter(SqlCommand command, string parameterName, object value)
        {
            SqlParameter parameter = command.CreateParameter();
            parameter.ParameterName = parameterName;

            if (value != null)
            {
                if (value is DateTime)
                {
                    if (DateTime.Parse(value.ToString()) <= DateTime.Parse("1/01/1950 12:00:00"))
                    {
                        value = null;
                    }
                }
            }

            parameter.Value = value ?? DBNull.Value;
            command.Parameters.Add(parameter);
        }
    }
}
