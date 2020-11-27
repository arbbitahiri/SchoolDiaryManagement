using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class CommentsDAL : IBaseCRUD<Comments>, IBaseConvert<Comments>
    {
        public bool Create(Comments model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Comment_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "date", model.CommentDate);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "comment", model.Comment);
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

        public bool Update(Comments model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Comment_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "commentID", model.CommentID);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "date", model.CommentDate);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "comment", model.Comment);
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
            throw new NotImplementedException();
        }

        public Comments Get(int id)
        {
            try
            {
                Comments comment = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Comment_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "commentID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            comment = new Comments();
                            while (reader.Read())
                            {
                                comment = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    comment.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    comment.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                    comment.Review = new Reviews { Review = reader["Review"].ToString(), ReviewID = int.Parse(reader["ReviewID"].ToString()) };
                                    comment.Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                return comment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Comments> GetAll()
        {
            try
            {
                List<Comments> MyComments = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Comment_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyComments = new List<Comments>();
                            while (reader.Read())
                            {
                                var comment = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    comment.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    comment.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                    if (reader["Review"] != DBNull.Value && reader["ReviewID"] != DBNull.Value)
                                    {
                                        comment.Review = new Reviews
                                        {
                                            Review = reader["Review"].ToString(),
                                            ReviewID = int.Parse(reader["ReviewID"].ToString())
                                        };
                                    }
                                    else
                                    {
                                        comment.Review = new Reviews
                                        {
                                            Review = "Not reviewed yet!",
                                            ReviewID = 0
                                        };
                                    }
                                    comment.Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                                MyComments.Add(comment);
                            }
                        }
                    }
                }
                return MyComments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Comments ToObject(SqlDataReader dataReader)
        {
            try
            {
                var comment = new Comments();

                if (dataReader["CommentID"] != DBNull.Value)
                    comment.CommentID = int.Parse(dataReader["CommentID"].ToString());

                if (dataReader["ClassID"] != DBNull.Value)
                    comment.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["SubjectID"] != DBNull.Value)
                    comment.SubjectID = int.Parse(dataReader["SubjectID"].ToString());

                if (dataReader["Date"] != DBNull.Value)
                    comment.CommentDate = DateTime.Parse(dataReader["Date"].ToString());

                if (dataReader["Time"] != DBNull.Value)
                    comment.Time = int.Parse(dataReader["Time"].ToString());

                if (dataReader["Comment"] != DBNull.Value)
                    comment.Comment = dataReader["Comment"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    comment.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    comment.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    comment.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    comment.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    comment.LUN = int.Parse(dataReader["LUN"].ToString());

                return comment;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
