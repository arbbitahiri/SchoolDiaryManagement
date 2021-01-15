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
                                if ((reader["First_Name_T"] != DBNull.Value && reader["Last_Name_T"] != DBNull.Value)
                                    || (reader["First_Name_P"] != DBNull.Value && reader["Last_Name_P"] != DBNull.Value))
                                {
                                    result.Teacher = new Teachers { FirstName = reader["First_Name_T"].ToString(), LastName = reader["Last_Name_T"].ToString() };
                                    result.Parent = new Parents { FirstName = reader["First_Name_P"].ToString(), LastName = reader["Last_Name_P"].ToString() };
                                }

                                if (reader["RoleName"] != DBNull.Value)
                                {
                                    result.Role = new Roles { RoleName = reader["RoleName"].ToString() };
                                }
                            }

                            return result;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool Create(Users model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_User_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "username", model.Username);
                        DataConnection.AddParameter(command, "password", model.Password);
                        DataConnection.AddParameter(command, "firstname", model.FirstName);
                        DataConnection.AddParameter(command, "lastname", model.LastName);
                        DataConnection.AddParameter(command, "expiresdate", model.ExpiresDate);
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);
                        DataConnection.AddParameter(command, "parentID", model.ParentID);
                        DataConnection.AddParameter(command, "roleID", model.RoleID);
                        DataConnection.AddParameter(command, "insertby", model.InsertBy);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "LUN", model.LUN);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Users model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_User_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "userID", model.UserID);
                        DataConnection.AddParameter(command, "username", model.Username);
                        DataConnection.AddParameter(command, "firstname", model.FirstName);
                        DataConnection.AddParameter(command, "lastname", model.LastName);
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);
                        DataConnection.AddParameter(command, "parentID", model.ParentID);
                        DataConnection.AddParameter(command, "expiresdate", model.ExpiresDate);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "LUN", model.LUN);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangePassword(Users model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_User_ChangePassword";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "userID", model.UserID);
                        DataConnection.AddParameter(command, "userpass", model.Password);
                        DataConnection.AddParameter(command, "ispasswordchanged", model.IsPasswordChanged);
                        DataConnection.AddParameter(command, "lastpasswordchangedate", model.LastPasswordChangeDate);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "LUB", model.LUB);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_User_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "userID", id);

                        int result = command.ExecuteNonQuery();

                        return result > 0;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Users Get(int id)
        {
            try
            {
                Users user = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_User_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "userID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            user = new Users();
                            while (reader.Read())
                            {
                                user = ToObject(reader);
                                if (reader["RoleName"] != DBNull.Value && reader["RoleID"] != DBNull.Value)
                                {
                                    user.Role = new Roles
                                    {
                                        RoleName = reader["RoleName"].ToString(),
                                        RoleID = (int)reader["RoleID"]
                                    };
                                    user.Parent = new Parents
                                    {
                                        FirstName = reader["First_Name_P"].ToString(),
                                        LastName = reader["Last_Name_P"].ToString()
                                    };
                                    user.Teacher = new Teachers
                                    {
                                        FirstName = reader["First_Name_T"].ToString(),
                                        LastName = reader["Last_Name_T"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Users> GetAll()
        {
            try
            {
                List<Users> MyUsers = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Users_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyUsers = new List<Users>();
                            while (reader.Read())
                            {
                                var user = ToObject(reader);
                                if (reader["RoleName"] != DBNull.Value && reader["RoleID"] != DBNull.Value)
                                {
                                    user.Role = new Roles
                                    {
                                        RoleName = reader["RoleName"].ToString(),
                                        RoleID = (int)reader["RoleID"]
                                    };
                                    user.Parent = new Parents
                                    {
                                        FirstName = reader["First_Name_P"].ToString(),
                                        LastName = reader["Last_Name_P"].ToString()
                                    };
                                    user.Teacher = new Teachers
                                    {
                                        FirstName = reader["First_Name_T"].ToString(),
                                        LastName = reader["Last_Name_T"].ToString()
                                    };
                                }
                                MyUsers.Add(user);
                            }
                        }
                    }
                }
                return MyUsers;
            }
            catch (Exception)
            {

                throw;
            }
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

                if (dataReader["ParentID"] != DBNull.Value)
                    user.ParentID = int.Parse(dataReader["ParentID"].ToString());

                if (dataReader["First_Name"] != DBNull.Value)
                    user.FirstName = dataReader["First_Name"].ToString();

                if (dataReader["Last_Name"] != DBNull.Value)
                    user.LastName = dataReader["Last_Name"].ToString();

                if (dataReader["LastLoginDate"] != DBNull.Value)
                    user.LastLoginDate = DateTime.Parse(dataReader["LastLoginDate"].ToString());

                if (dataReader["LastLoginDate"] != DBNull.Value)
                    user.LastLoginTime = DateTime.Parse(dataReader["LastLoginDate"].ToString());

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
