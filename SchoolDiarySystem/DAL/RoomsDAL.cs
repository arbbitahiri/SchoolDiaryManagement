using SchoolDiarySystem.DAL.DataBase;
using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class RoomsDAL : IBaseCRUD<Rooms>, IBaseConvert<Rooms>
    {
        public bool Create(Rooms model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Room_Create";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "roomNo", model.RoomNo);
                        DataConnection.AddParameter(command, "roomType", model.RoomType);
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

        public bool Update(Rooms model)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Room_Update";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "roomID", model.RoomID);
                        DataConnection.AddParameter(command, "roomno", model.RoomNo);
                        DataConnection.AddParameter(command, "roomtype", model.RoomType);
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

        public bool Delete(int id)
        {
            try
            {
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Room_Delete";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "roomID", id);

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

        public Rooms Get(int id)
        {
            try
            {
                Rooms room = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Room_Get";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        DataConnection.AddParameter(command, "roomID", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            room = new Rooms();
                            while (reader.Read())
                            {
                                room = ToObject(reader);
                            }
                        }
                    }
                }
                return room;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Rooms> GetAll()
        {
            try
            {
                List<Rooms> MyRooms = null;
                using (var connection = DataConnection.GetConnection())
                {
                    string sqlproc = "dbo.usp_Room_GetList";
                    using (var command = DataConnection.GetCommand(connection, sqlproc, CommandType.StoredProcedure))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            MyRooms = new List<Rooms>();
                            while (reader.Read())
                                MyRooms.Add(ToObject(reader));
                        }
                    }
                }
                return MyRooms;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Rooms ToObject(SqlDataReader dataReader)
        {
            try
            {
                var room = new Rooms();

                if (dataReader["RoomID"] != DBNull.Value)
                    room.RoomID = int.Parse(dataReader["RoomID"].ToString());

                if (dataReader["Room_No"] != DBNull.Value)
                    room.RoomNo = int.Parse(dataReader["Room_No"].ToString());

                if (dataReader["Room_Type"] != DBNull.Value)
                    room.RoomType = dataReader["Room_Type"].ToString();

                if (dataReader["InsertBy"] != DBNull.Value)
                    room.InsertBy = dataReader["InsertBy"].ToString();

                if (dataReader["InsertDate"] != DBNull.Value)
                    room.InsertDate = DateTime.Parse(dataReader["InsertDate"].ToString());

                if (dataReader["LUB"] != DBNull.Value)
                    room.LUB = dataReader["LUB"].ToString();

                if (dataReader["LUD"] != DBNull.Value)
                    room.LUD = DateTime.Parse(dataReader["LUD"].ToString());

                if (dataReader["LUN"] != DBNull.Value)
                    room.LUN = int.Parse(dataReader["LUN"].ToString());

                return room;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
