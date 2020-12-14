using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class AbsencesDAL : IBaseCRUD<Absences>, IBaseConvert<Absences>
    {
        public bool Create(Absences model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Absence_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "date", model.AbsenceDate);
                        DataConnection.AddParameter(command, "absencereasoning", model.AbsenceReasoning);
                        DataConnection.AddParameter(command, "studentID", model.StudentID);
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

        public bool Update(Absences model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Absence_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "absenceID", model.AbsenceID);
                        DataConnection.AddParameter(command, "classID", model.ClassID);
                        DataConnection.AddParameter(command, "subjectID", model.SubjectID);
                        DataConnection.AddParameter(command, "date", model.AbsenceDate);
                        DataConnection.AddParameter(command, "absencereasoning", model.AbsenceReasoning);
                        DataConnection.AddParameter(command, "studentID", model.StudentID);
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
                    string sqlproc = "dbo.usp_Absence_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "absenceID", id);

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

        public Absences Get(int id)
        {
            try
            {
                Absences absence = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Absence_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            absence = new Absences();
                            while (reader.Read())
                            {
                                absence = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value
                                    && reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    absence.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    absence.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                    absence.Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                            }
                        }
                    }
                }
                return absence;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Absences> GetAll()
        {
            try
            {
                List<Absences> MyAbsences = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Absence_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyAbsences = new List<Absences>();
                            while (reader.Read())
                            {
                                var absence = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value
                                    && reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    absence.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    absence.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                    absence.Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                                MyAbsences.Add(absence);
                            }
                        }
                    }
                }
                return MyAbsences;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Absences> GetAllForTeacher(int teacherID)
        {
            try
            {
                List<Absences> MyAbsences = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Absence_GetList_ForTeacher";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "teacherID", teacherID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyAbsences = new List<Absences>();
                            while (reader.Read())
                            {
                                var absence = ToObject(reader);
                                if (reader["Class_No"] != DBNull.Value && reader["Subject_Title"] != DBNull.Value
                                    && reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    absence.Class = new Class { ClassNo = int.Parse(reader["Class_No"].ToString()) };
                                    absence.Subject = new Subjects { SubjectTitle = reader["Subject_Title"].ToString() };
                                    absence.Student = new Students
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString()
                                    };
                                }
                                MyAbsences.Add(absence);
                            }
                        }
                    }
                }
                return MyAbsences;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Absences ToObject(SqlDataReader dataReader)
        {
            try
            {
                var absence = new Absences();

                if (dataReader["AbsenceID"] != DBNull.Value)
                    absence.AbsenceID = int.Parse(dataReader["AbsenceID"].ToString());

                if (dataReader["ClassID"] != DBNull.Value)
                    absence.ClassID = int.Parse(dataReader["ClassID"].ToString());

                if (dataReader["SubjectID"] != DBNull.Value)
                    absence.SubjectID = int.Parse(dataReader["SubjectID"].ToString());

                if (dataReader["StudentID"] != DBNull.Value)
                    absence.StudentID = int.Parse(dataReader["StudentID"].ToString());

                if (dataReader["Date"] != DBNull.Value)
                    absence.AbsenceDate = DateTime.Parse(dataReader["Date"].ToString());

                if (dataReader["AbsenceReasoning"] != DBNull.Value)
                    absence.AbsenceReasoning = dataReader["AbsenceReasoning"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    absence.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    absence.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    absence.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    absence.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    absence.LUN = int.Parse(dataReader["LUN"].ToString());

                return absence;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
