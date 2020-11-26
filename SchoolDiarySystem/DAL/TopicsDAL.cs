using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class TopicsDAL : IBaseCRUD<Topics>, IBaseConvert<Topics>
    {
        public bool Create(Topics model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Topic_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "date", model.TopicDate);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "content", model.Content);
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

        public bool Update(Topics model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Topic_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "topicID", model.TopicID);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "date", model.TopicDate);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "content", model.Content);
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
                    string sqlproc = "dbo.usp_Topic_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "topicID", id);

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

        public Topics Get(int id)
        {
            try
            {
                Topics topic = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Topic_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "topicID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            topic = new Topics();
                            while (reader.Read())
                            {
                                topic = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    topic.Class = new Class { ClassNo = (int)reader["Class_No"] };
                                    topic.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                }
                            }
                        }
                    }
                }
                return topic;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Topics> GetAll()
        {
            try
            {
                List<Topics> MyTopics = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Topics_ViewAll";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyTopics = new List<Topics>();
                            while (reader.Read())
                            {
                                var topic = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    topic.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    topic.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                }
                                MyTopics.Add(topic);
                            }
                        }
                    }
                }
                return MyTopics;
            }
            catch (Exception)
            {

                throw;

            }
        }

        public Topics ToObject(SqlDataReader dataReader)
        {
            try
            {
                var topic = new Topics();

                if (dataReader["TopicID"] != DBNull.Value)
                    topic.TopicID = int.Parse(dataReader["TopicID"].ToString());

                if (dataReader["ClassID"] != DBNull.Value)
                    topic.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["SubjectID"] != DBNull.Value)
                    topic.SubjectID = int.Parse(dataReader["SubjectID"].ToString());

                if (dataReader["Date"] != DBNull.Value)
                    topic.TopicDate = DateTime.Parse(dataReader["Date"].ToString());

                if (dataReader["Time"] != DBNull.Value)
                    topic.Time = int.Parse(dataReader["Time"].ToString());

                if (dataReader["Content"] != DBNull.Value)
                    topic.Content = dataReader["Content"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    topic.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    topic.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    topic.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    topic.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    topic.LUN = int.Parse(dataReader["LUN"].ToString());

                return topic;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
