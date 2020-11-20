using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class UsersDAL : IBaseCRUD<Users>, IBaseConvert<Users>
    {
        public Users Login(string username, string password)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Authenticate";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "username", username);
                        DataConnection.AddParameter(command, "password", password);

                        using (var reader = command.ExecuteReader())
                        {
                            Users result = null;
                            while (reader.Read())
                            {
                                result = ToObject(reader);
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Create(Users model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Users model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Users model)
        {
            throw new NotImplementedException();
        }

        public Users Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Users ToObject(SqlDataReader dataReader)
        {
            try
            {
                var user = new Users();

                if (dataReader["UserID"] != DBNull.Value)
                    user.UserID = int.Parse(dataReader["UserID"].ToString());

                if (dataReader["UserName"] != DBNull.Value)
                    user.Username = dataReader["UserName"].ToString();

                if (dataReader["UserPass"] != DBNull.Value)
                    user.Password = dataReader["UserPass"].ToString();

                if (dataReader["ExpiresDate"] != DBNull.Value)
                    user.ExpiresDate = DateTime.Parse(dataReader["ExpiresDate"].ToString());

                if (dataReader["RoleID"] != DBNull.Value)
                    user.RoleID = int.Parse(dataReader["RoleID"].ToString());

                if (dataReader["InsertBy"] != DBNull.Value)
                    user.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    user.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    user.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    user.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    user.LUN = int.Parse(dataReader["LUN"].ToString());

                if (dataReader["TeacherID"] != DBNull.Value)
                    user.TeacherID = int.Parse(dataReader["TeacherID"].ToString());

                if (dataReader["First_Name"] != DBNull.Value)
                    user.FirstName = dataReader["First_Name"].ToString();

                if (dataReader["Last_Name"] != DBNull.Value)
                    user.LastName = dataReader["Last_Name"].ToString();

                if (dataReader["LastLoginDate"] != DBNull.Value)
                    user.LastLoginDate = DateTime.Parse(dataReader["LastLoginDate"].ToString());

                if (dataReader["LastPasswordChangeDate"] != DBNull.Value)
                    user.LastPasswordChangeDate = DateTime.Parse(dataReader["LastPasswordChangeDate"].ToString());

                if (dataReader["IsPasswordChanged"] != DBNull.Value)
                    user.IsPasswordChanged = bool.Parse(dataReader["IsPasswordChanged"].ToString());

                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
