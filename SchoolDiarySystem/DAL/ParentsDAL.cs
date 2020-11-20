using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class ParentsDAL : IBaseCRUD<Parents>, IBaseConvert<Parents>
    {
        public bool Create(Parents model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Parents model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Parents model)
        {
            throw new NotImplementedException();
        }

        public Parents Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Parents> GetAll()
        {
            throw new NotImplementedException();
        }

        public Parents ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
