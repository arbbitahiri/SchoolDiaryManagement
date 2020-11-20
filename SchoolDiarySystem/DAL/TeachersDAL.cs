using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class TeachersDAL : IBaseCRUD<Teachers>, IBaseConvert<Teachers>
    {
        public bool Create(Teachers model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Teachers model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Teachers model)
        {
            throw new NotImplementedException();
        }

        public Teachers Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Teachers> GetAll()
        {
            throw new NotImplementedException();
        }

        public Teachers ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
