using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class StudentsDAL : IBaseCRUD<Students>, IBaseConvert<Students>
    {
        public bool Create(Students model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Students model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Students Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Students> GetAll()
        {
            throw new NotImplementedException();
        }

        public Students ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
