using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class RolesDAL : IBaseCRUD<Roles>, IBaseConvert<Roles>
    {
        public bool Create(Roles model)
        {
            throw new NotImplementedException();
        }

        public Roles ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Roles model)
        {
            throw new NotImplementedException();
        }

        public Roles Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Roles> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Roles model)
        {
            throw new NotImplementedException();
        }
    }
}
