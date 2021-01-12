using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class StaffAbsenceDAL : IBaseConvert<StaffAbsence>, IBaseCRUD<StaffAbsence>
    {
        public bool Create(StaffAbsence model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_StaffAbsence_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "userID", model.UserID);
                        DataConnection.AddParameter(command, "absencedate", model.AbsenceDate);
                        DataConnection.AddParameter(command, "abreasoning", model.AbsenceReasoning);
                        DataConnection.AddParameter(command, "LUN", model.LUN);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "insertby", model.InsertBy);

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

        public bool Update(StaffAbsence model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_StaffAbsence_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "staffabsenceID", model.StaffAbsenceID);
                        DataConnection.AddParameter(command, "userID", model.UserID);
                        DataConnection.AddParameter(command, "absencedate", model.AbsenceDate);
                        DataConnection.AddParameter(command, "abreasoning", model.AbsenceReasoning);
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
                    string sqlproc = "dbo.usp_StaffAbsence_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "staffID", id);

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

        public StaffAbsence Get(int id)
        {
            try
            {
                StaffAbsence staffAbsence = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_StaffAbsence_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "staffID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            staffAbsence = new StaffAbsence();
                            while (reader.Read())
                            {
                                staffAbsence = ToObject(reader);
                                if (reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    staffAbsence.User = new Users
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString(),
                                        Role = new Roles
                                        {
                                            RoleName = reader["RoleName"].ToString()
                                        }
                                    };
                                }
                                else if (reader["First_Name_T"] != DBNull.Value && reader["Last_Name_T"] != DBNull.Value)
                                {
                                    staffAbsence.User = new Users
                                    {
                                        Teacher = new Teachers
                                        {
                                            FirstName = reader["First_Name_T"].ToString(),
                                            LastName = reader["Last_Name_T"].ToString()
                                        }
                                    };
                                }
                            }
                        }
                    }
                }
                return staffAbsence;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StaffAbsence> GetAll()
        {
            try
            {
                List<StaffAbsence> MyStaffAbsences = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_StaffAbsence_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyStaffAbsences = new List<StaffAbsence>();
                            while (reader.Read())
                            {
                                var staffAbsence = ToObject(reader);
                                if (reader["First_Name"] != DBNull.Value && reader["Last_Name"] != DBNull.Value)
                                {
                                    staffAbsence.User = new Users
                                    {
                                        FirstName = reader["First_Name"].ToString(),
                                        LastName = reader["Last_Name"].ToString(),
                                        Role = new Roles
                                        {
                                            RoleName = reader["RoleName"].ToString()
                                        }
                                    };
                                }
                                else if (reader["First_Name_T"] != DBNull.Value && reader["Last_Name_T"] != DBNull.Value)
                                {
                                    staffAbsence.User = new Users
                                    {
                                        Teacher = new Teachers
                                        {
                                            FirstName = reader["First_Name_T"].ToString(),
                                            LastName = reader["Last_Name_T"].ToString()
                                        }
                                    };
                                }
                                MyStaffAbsences.Add(staffAbsence);
                            }
                        }
                    }
                }
                return MyStaffAbsences;
            }
            catch (Exception e)
            {
                string s = e.Message;
                throw;
            }
        }

        public StaffAbsence ToObject(SqlDataReader dataReader)
        {
            try
            {
                var staffAbsence = new StaffAbsence();

                if (dataReader["StaffAbsenceID"] != DBNull.Value)
                    staffAbsence.StaffAbsenceID = int.Parse(dataReader["StaffAbsenceID"].ToString());

                if (dataReader["UserID"] != DBNull.Value)
                    staffAbsence.UserID = int.Parse(dataReader["UserID"].ToString());

                if (dataReader["AbsenceDate"] != DBNull.Value)
                    staffAbsence.AbsenceDate = DateTime.Parse(dataReader["AbsenceDate"].ToString());

                if (dataReader["AbsenceReasoning"] != DBNull.Value)
                    staffAbsence.AbsenceReasoning = dataReader["AbsenceReasoning"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    staffAbsence.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    staffAbsence.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    staffAbsence.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    staffAbsence.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    staffAbsence.LUN = int.Parse(dataReader["LUN"].ToString());

                return staffAbsence;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}