using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class StudentsDAL : IBaseCRUD<Students>, IBaseConvert<Students>
    {
        public bool Create(Students model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Student_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "firstName", model.FirstName);
                        DataConnection.AddParameter(command, "lastName", model.LastName);
                        DataConnection.AddParameter(command, "gender", model.Gender);
                        DataConnection.AddParameter(command, "dob", model.DayofBirth);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "parentID", model.ParentID);
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

        public bool Update(Students model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Student_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "studentID", model.StudentID);
                        DataConnection.AddParameter(command, "firstname", model.FirstName);
                        DataConnection.AddParameter(command, "lastname", model.LastName);
                        DataConnection.AddParameter(command, "gender", model.Gender);
                        DataConnection.AddParameter(command, "dob", model.DayofBirth);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "parentID", model.ParentID);
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
                    string sqlproc = "dbo.usp_Student_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "studentID", id);

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

        public Students Get(int id)
        {
            try
            {
                Students student = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Student_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "studentID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            student = new Students();
                            while (reader.Read())
                            {
                                student = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["First_Name_P"] != DBNull.Value && reader["Last_Name_P"] != DBNull.Value)
                                {
                                    student.Class = new Class { ClassNo = (int)reader["Class_No"] };
                                    student.Parent = new Parents { FirstName = reader["First_Name_P"].ToString(), LastName = reader["Last_Name_P"].ToString() };
                                }
                            }
                        }
                    }
                }
                return student;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Students> GetMyStudents(int id)
        {
            try
            {
                List<Students> MyStudents = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Get_MyStudents";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyStudents = new List<Students>();
                            while (reader.Read())
                            {
                                var student = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["TeacherID"] != DBNull.Value)
                                {
                                    student.Class = new Class { ClassNo = (int)reader["Class_No"], TeacherID = (int)reader["TeacherID"] };
                                    student.Parent = new Parents { FirstName = reader["First_Name_P"].ToString(), LastName = reader["Last_Name_P"].ToString() };
                                }
                                MyStudents.Add(student);
                            }
                        }
                    }
                }
                return MyStudents;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<Students> GetAll()
        {
            try
            {
                List<Students> MyStudents = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Student_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyStudents = new List<Students>();
                            while (reader.Read())
                            {
                                var student = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["First_Name_P"] != DBNull.Value && reader["Last_Name_P"] != DBNull.Value)
                                {
                                    student.Class = new Class { ClassNo = (int)reader["Class_No"] };
                                    student.Parent = new Parents { FirstName = reader["First_Name_P"].ToString(), LastName = reader["Last_Name_P"].ToString() };
                                }
                                MyStudents.Add(student);
                            }
                        }
                    }
                }
                return MyStudents;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Count()
        {
            try
            {
                int result = 0;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Count_Students";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        result = (int)command.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Students ToObject(SqlDataReader dataReader)
        {
            try
            {
                var student = new Students();

                if (dataReader["StudentID"] != DBNull.Value)
                    student.StudentID = int.Parse(dataReader["StudentID"].ToString());

                if (dataReader["First_Name"] != DBNull.Value)
                    student.FirstName = dataReader["First_Name"].ToString();

                if (dataReader["Last_Name"] != DBNull.Value)
                    student.LastName = dataReader["Last_Name"].ToString();

                if (dataReader["Gender"] != DBNull.Value)
                    student.Gender = dataReader["Gender"].ToString();

                if (dataReader["Day_of_Birth"] != DBNull.Value)
                    student.DayofBirth = DateTime.Parse(dataReader["Day_of_Birth"].ToString());

                if (dataReader["ClassID"] != DBNull.Value)
                    student.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["ParentID"] != DBNull.Value)
                    student.ParentID = int.Parse(dataReader["ParentID"].ToString());

                if (dataReader["InsertBy"] != DBNull.Value)
                    student.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    student.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    student.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    student.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    student.LUN = int.Parse(dataReader["LUN"].ToString());

                return student;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
