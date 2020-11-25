using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class TeachersDAL : IBaseCRUD<Teachers>, IBaseConvert<Teachers>
    {
        public bool Create(Teachers model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Teacher_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "firstname", model.FirstName);
                        DataConnection.AddParameter(command, "lastname", model.LastName);
                        DataConnection.AddParameter(command, "gender", model.Gender);
                        DataConnection.AddParameter(command, "city", model.City);
                        DataConnection.AddParameter(command, "qualification", model.Qualification);
                        DataConnection.AddParameter(command, "dayofbirth", model.DayofBirth);
                        DataConnection.AddParameter(command, "email", model.Email);
                        DataConnection.AddParameter(command, "phoneno", model.PhoneNo);
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

        public bool Update(Teachers model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Teacher_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);
                        DataConnection.AddParameter(command, "firstname", model.FirstName);
                        DataConnection.AddParameter(command, "lastname", model.LastName);
                        DataConnection.AddParameter(command, "gender", model.Gender);
                        DataConnection.AddParameter(command, "city", model.City);
                        DataConnection.AddParameter(command, "qualification", model.Qualification);
                        DataConnection.AddParameter(command, "dayofbirth", model.DayofBirth);
                        DataConnection.AddParameter(command, "email", model.Email);
                        DataConnection.AddParameter(command, "phoneno", model.PhoneNo);
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

        public bool Delete(int id)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Teachers_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", id);

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

        public Teachers Get(int id)
        {
            try
            {
                Teachers teachers = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Teacher_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            teachers = new Teachers();
                            while (reader.Read())
                            {
                                teachers = ToObject(reader);
                            }
                        }
                    }
                }
                return teachers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Teachers> GetAll()
        {
            try
            {
                List<Teachers> MyTeachers = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Teachers_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyTeachers = new List<Teachers>();
                            while (reader.Read())
                            {
                                MyTeachers.Add(ToObject(reader));
                            }
                        }
                    }
                }
                return MyTeachers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Teachers ToObject(SqlDataReader dataReader)
        {
            try
            {
                var teacher = new Teachers();

                if (dataReader["TeacherID"] != DBNull.Value)
                    teacher.TeacherID = int.Parse(dataReader["TeacherID"].ToString());

                if (dataReader["First_Name"] != DBNull.Value)
                    teacher.FirstName = dataReader["First_Name"].ToString();

                if (dataReader["Last_Name"] != DBNull.Value)
                    teacher.LastName = dataReader["Last_Name"].ToString();

                if (dataReader["Gender"] != DBNull.Value)
                    teacher.Gender = dataReader["Gender"].ToString();

                if (dataReader["City"] != DBNull.Value)
                    teacher.City = dataReader["City"].ToString();

                if (dataReader["Qualification"] != DBNull.Value)
                    teacher.Qualification = dataReader["Qualification"].ToString();

                if (dataReader["Day_of_Birth"] != DBNull.Value)
                    teacher.DayofBirth = DateTime.Parse(dataReader["Day_of_Birth"].ToString());

                if (dataReader["Email"] != DBNull.Value)
                    teacher.Email = dataReader["Email"].ToString();

                if (dataReader["Phone_No"] != DBNull.Value)
                    teacher.PhoneNo = dataReader["Phone_No"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    teacher.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    teacher.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    teacher.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    teacher.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    teacher.LUN = int.Parse(dataReader["LUN"].ToString());

                return teacher;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
