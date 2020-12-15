using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using SchoolDiarySystem.DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace SchoolDiarySystem.DAL
{
    public class ClassSchedulesDAL : IBaseCRUD<ClassSchedules>, IBaseConvert<ClassSchedules>
    {
        public bool Create(ClassSchedules model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_ClassSchedule_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "date", model.Day);
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

        public bool Update(ClassSchedules model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_ClassSchedule_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "scheduleID", model.ScheduleID);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "time", model.Time);
                        DataConnection.AddParameter(command, "date", model.Day);
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
                    string sqlproc = "dbo.usp_ClassSchedule_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "scheduleID", id);

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

        public ClassSchedules Get(int id)
        {
            try
            {
                ClassSchedules schedule = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_ClassSchedule_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "scheduleID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            schedule = new ClassSchedules();
                            while (reader.Read())
                            {
                                schedule = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    schedule.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    schedule.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                }
                            }
                        }
                    }
                }
                return schedule;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClassSchedules> GetAll()
        {
            try
            {
                List<ClassSchedules> MySchedules = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_ClassSchedule_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MySchedules = new List<ClassSchedules>();
                            while (reader.Read())
                            {
                                var schedule = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    schedule.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    schedule.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                }
                                MySchedules.Add(schedule);
                            }
                        }
                    }
                }
                return MySchedules;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ClassSchedules> GetAllForParent(int parentID)
        {
            try
            {
                List<ClassSchedules> MySchedules = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_ClassSchedule_GetList_ForParent";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "parentID", parentID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MySchedules = new List<ClassSchedules>();
                            while (reader.Read())
                            {
                                var schedule = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value)
                                {
                                    schedule.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    schedule.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                }
                                MySchedules.Add(schedule);
                            }
                        }
                    }
                }
                return MySchedules;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ClassSchedules ToObject(SqlDataReader dataReader)
        {
            try
            {
                var schedule = new ClassSchedules();

                if (dataReader["ScheduleID"] != DBNull.Value)
                    schedule.ScheduleID = int.Parse(dataReader["ScheduleID"].ToString());

                if (dataReader["ClassID"] != DBNull.Value)
                    schedule.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["SubjectID"] != DBNull.Value)
                    schedule.SubjectID = int.Parse(dataReader["SubjectID"].ToString());

                if (dataReader["Time"] != DBNull.Value)
                    schedule.Time = int.Parse(dataReader["Time"].ToString());

                if (dataReader["Day"] != DBNull.Value)
                    schedule.Day = dataReader["Day"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    schedule.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    schedule.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    schedule.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    schedule.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    schedule.LUN = int.Parse(dataReader["LUN"].ToString());

                return schedule;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
