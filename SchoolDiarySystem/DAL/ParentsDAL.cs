using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class ParentsDAL : IBaseCRUD<Parents>, IBaseConvert<Parents>
    {
        public bool Create(Parents model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Parent_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "firstName", model.FirstName);
                        DataConnection.AddParameter(command, "lastName", model.LastName);
                        DataConnection.AddParameter(command, "city", model.City);
                        DataConnection.AddParameter(command, "insertby", model.InsertBy);
                        DataConnection.AddParameter(command, "LUB", model.LUB);
                        DataConnection.AddParameter(command, "LUN", model.LUN);

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

        public bool Update(Parents model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Parent_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "parentID", model.ParentID);
                        DataConnection.AddParameter(command, "firstName", model.FirstName);
                        DataConnection.AddParameter(command, "lastName", model.LastName);
                        DataConnection.AddParameter(command, "city", model.City);
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
                    string sqlproc = "dbo.usp_Parent_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "parentID", id);

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

        public Parents Get(int id)
        {
            try
            {
                Parents parent = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Parent_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "parentID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            parent = new Parents();
                            while (reader.Read())
                            {
                                parent = ToObject(reader);
                            }
                        }
                    }
                }
                return parent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Parents> GetAll()
        {
            try
            {
                List<Parents> MyParents = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Parent_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyParents = new List<Parents>();
                            while (reader.Read())
                            {
                                MyParents.Add(ToObject(reader));
                            }
                        }
                    }
                }
                return MyParents;
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
                    string sqlproc = "dbo.usp_Count_Parents";
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

        public Parents ToObject(SqlDataReader dataReader)
        {
            try
            {
                var parent = new Parents();

                if (dataReader["ParentID"] != DBNull.Value)
                    parent.ParentID = int.Parse(dataReader["ParentID"].ToString());

                if (dataReader["First_Name_P"] != DBNull.Value)
                    parent.FirstName = dataReader["First_Name_P"].ToString();

                if (dataReader["Last_Name_P"] != DBNull.Value)
                    parent.LastName = dataReader["Last_Name_P"].ToString();

                if (dataReader["City"] != DBNull.Value)
                    parent.City = dataReader["City"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    parent.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    parent.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    parent.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    parent.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    parent.LUN = int.Parse(dataReader["LUN"].ToString());

                return parent;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
