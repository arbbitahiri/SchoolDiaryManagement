using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class TopicsDAL : IBaseCRUD<Topics>, IBaseConvert<Topics>
    {
        public bool Create(Topics model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Topics model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Topics model)
        {
            throw new NotImplementedException();
        }

        public Topics Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Topics> GetAll()
        {
            throw new NotImplementedException();
        }

        public Topics ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
