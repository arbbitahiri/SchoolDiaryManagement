using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class SubjectsDAL : IBaseCRUD<Subjects>, IBaseConvert<Subjects>
    {
        public bool Create(Subjects model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Subjects model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Subjects model)
        {
            throw new NotImplementedException();
        }

        public Subjects Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Subjects> GetAll()
        {
            throw new NotImplementedException();
        }

        public Subjects ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
