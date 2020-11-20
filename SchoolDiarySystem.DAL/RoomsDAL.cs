using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class RoomsDAL : IBaseCRUD<Rooms>, IBaseConvert<Rooms>
    {
        public bool Create(Rooms model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rooms model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Rooms model)
        {
            throw new NotImplementedException();
        }

        public Rooms Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Rooms> GetAll()
        {
            throw new NotImplementedException();
        }

        public Rooms ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
