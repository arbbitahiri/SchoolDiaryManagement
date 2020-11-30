using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace SchoolDiarySystem.DAL
{
    public class ReviewsDAL : IBaseConvert<Reviews>, IBaseCRUD<Reviews>
    {
        public bool Create(Reviews model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Review_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "commentID", model.CommentID);
                        DataConnection.AddParameter(command, "review", model.Review);
                        DataConnection.AddParameter(command, "reviewdate", model.ReviewDate);
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

        public bool Update(Reviews model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Review_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "reviewID", model.CommentID);
                        DataConnection.AddParameter(command, "review", model.Review);
                        DataConnection.AddParameter(command, "reviewdate", model.ReviewDate);
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

        public Reviews Get(int id)
        {
            try
            {
                Reviews review = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Review_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "reviewID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            review = new Reviews();
                            while (reader.Read())
                            {
                                review = ToObject(reader);
                                review.Comment = new Comments
                                {
                                    Comment = reader["Comment"].ToString(),
                                    CommentDate = DateTime.Parse(reader["Date"].ToString()),
                                    Time = int.Parse(reader["Time"].ToString()),
                                    ClassID = int.Parse(reader["ClassID"].ToString()),
                                    SubjectID = int.Parse(reader["SubjectID"].ToString()),
                                    Class = new Class
                                    {
                                        ClassNo = int.Parse(reader["Class_No"].ToString())
                                    },
                                    Subject = new Subjects
                                    {
                                        SubjectTitle = reader["Subject_Title"].ToString()
                                    },
                                    Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    }
                                };
                            }
                        }
                    }
                }
                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Reviews> GetAll()
        {
            try
            {
                List<Reviews> MyReviews = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Review_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyReviews = new List<Reviews>();
                            while (reader.Read())
                            {
                                var review = ToObject(reader);
                                review.Comment = new Comments
                                {
                                    Comment = reader["Comment"].ToString(),
                                    CommentDate = DateTime.Parse(reader["Date"].ToString()),
                                    Time = int.Parse(reader["Time"].ToString()),
                                    ClassID = int.Parse(reader["ClassID"].ToString()),
                                    SubjectID = int.Parse(reader["SubjectID"].ToString()),
                                    Class = new Class
                                    {
                                        ClassNo = int.Parse(reader["Class_No"].ToString())
                                    },
                                    Subject = new Subjects
                                    {
                                        SubjectTitle = reader["Subject_Title"].ToString()
                                    },
                                    Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    }
                                };
                                MyReviews.Add(review);
                            }
                        }
                    }
                }
                return MyReviews;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Reviews ToObject(SqlDataReader dataReader)
        {
            try
            {
                var reviews = new Reviews();

                if (dataReader["ReviewID"] != DBNull.Value)
                    reviews.ReviewID = int.Parse(dataReader["ReviewID"].ToString());

                if (dataReader["CommentID"] != DBNull.Value)
                    reviews.CommentID = int.Parse(dataReader["CommentID"].ToString());

                if (dataReader["Review"] != DBNull.Value)
                    reviews.Review = dataReader["Review"].ToString();

                if (dataReader["ReviewDate"] != DBNull.Value)
                    reviews.ReviewDate = DateTime.Parse(dataReader["ReviewDate"].ToString());

                if (dataReader["InsertBy"] != DBNull.Value)
                    reviews.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    reviews.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    reviews.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    reviews.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    reviews.LUN = int.Parse(dataReader["LUN"].ToString());

                return reviews;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}