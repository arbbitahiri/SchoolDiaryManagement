using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class SubjectsDAL : IBaseCRUD<Subjects>, IBaseConvert<Subjects>
    {
        public bool Create(Subjects model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Subject_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "subjecttitle", model.SubjectTitle);
                        DataConnection.AddParameter(command, "book", model.Book);
                        DataConnection.AddParameter(command, "bookauthor", model.BookAuthor);
                        DataConnection.AddParameter(command, "insertby", model.InsertBy);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);


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

        public bool Update(Subjects model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Subject_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "book", model.Book);
                        DataConnection.AddParameter(command, "bookauthor", model.BookAuthor);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "teacherID", model.TeacherID);


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
                    string sqlproc = "dbo.usp_Subject_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "subjectID", id);

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

        public Subjects Get(int id)
        {
            try
            {
                Subjects subject = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Subject_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "@subjectID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            subject = new Subjects();
                            while (reader.Read())
                            {
                                subject = ToObject(reader);
                                if (reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    subject.Teacher = new Teachers
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                return subject;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Subjects> GetAll()
        {
            try
            {
                List<Subjects> MySubjects = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Subject_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MySubjects = new List<Subjects>();
                            while (reader.Read())
                            {
                                var subject = ToObject(reader);
                                if (reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    subject.Teacher = new Teachers
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                                MySubjects.Add(subject);
                            }
                        }
                    }
                }
                return MySubjects;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Subjects ToObject(SqlDataReader dataReader)
        {
            try
            {
                var subject = new Subjects();

                if (dataReader["SubjectID"] != DBNull.Value)
                    subject.SubjectID = int.Parse(dataReader["SubjectID"].ToString());

                if (dataReader["Subject_Title"] != DBNull.Value)
                    subject.SubjectTitle = dataReader["Subject_Title"].ToString();

                if (dataReader["Book"] != DBNull.Value)
                    subject.Book = dataReader["Book"].ToString();

                if (dataReader["Book_Author"] != DBNull.Value)
                    subject.BookAuthor = dataReader["Book_Author"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    subject.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    subject.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    subject.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    subject.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    subject.LUN = int.Parse(dataReader["LUN"].ToString());

                if (dataReader["TeacherID"] != DBNull.Value)
                    subject.TeacherID = int.Parse(dataReader["TeacherID"].ToString());

                return subject;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
