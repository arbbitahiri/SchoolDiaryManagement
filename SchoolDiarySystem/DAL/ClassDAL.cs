using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class ClassDAL : IBaseCRUD<Class>, IBaseConvert<Class>
    {
        public bool Create(Class model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Class model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Class Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Class> GetAll()
        {
            throw new NotImplementedException();
        }

        public Class ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
