using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class ClassDAL : IBaseCRUD<Class>, IBaseConvert<Class>
    {
        public bool Create(Class model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Class_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);
                        DataConnection.AddParameter(command, "classNo", model.ClassNo);
                        DataConnection.AddParameter(command, "roomID", model.RoomID);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "insertby", model.InsertBy);

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

        public bool Update(Class model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Class_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);
                        DataConnection.AddParameter(command, "roomID", model.RoomID);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "LUB", model.LUB);

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

        public bool Delete(int id)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Class_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", id);

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

        public Class Get(int id)
        {
            try
            {
                Class classes = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Class_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            classes = new Class();
                            while (reader.Read())
                            {
                                classes = ToObject(reader);
                                if (reader["Room_Type"] != DBNull.Value && reader["First_Name_T"] != DBNull.Value && reader["Last_Name_T"] != DBNull.Value)
                                {
                                    classes.Room = new Rooms { RoomType = reader["Room_Type"].ToString() };
                                    classes.Teacher = new Teachers { FirstName = reader["First_Name_T"].ToString(), LastName = reader["Last_Name_T"].ToString() };
                                }
                            }
                        }
                    }
                }
                return classes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Class> GetAll()
        {
            try
            {
                List<Class> MyClasses = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Class_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyClasses = new List<Class>();
                            while (reader.Read())
                            {
                                var classes = ToObject(reader);
                                if (reader["Room_Type"] != DBNull.Value && reader["First_Name_T"] != DBNull.Value && reader["Last_Name_T"] != DBNull.Value)
                                {
                                    classes.Room = new Rooms { RoomType = reader["Room_Type"].ToString() };
                                    classes.Teacher = new Teachers { FirstName = reader["First_Name_T"].ToString(), LastName = reader["Last_Name_T"].ToString() };
                                }
                                MyClasses.Add(classes);
                            }
                        }
                    }
                }
                return MyClasses;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Class ToObject(SqlDataReader dataReader)
        {
            try
            {
                var classes = new Class();

                if (dataReader["ClassID"] != DBNull.Value)
                    classes.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["TeacherID"] != DBNull.Value)
                    classes.TeacherID = int.Parse(dataReader["TeacherID"].ToString());

                if (dataReader["Class_No"] != DBNull.Value)
                    classes.ClassNo = int.Parse(dataReader["Class_No"].ToString());

                if (dataReader["RoomID"] != DBNull.Value)
                    classes.RoomID = int.Parse(dataReader["RoomID"].ToString());

                if (dataReader["InsertBy"] != DBNull.Value)
                    classes.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    classes.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    classes.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    classes.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    classes.LUN = int.Parse(dataReader["LUN"].ToString());

                return classes;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
