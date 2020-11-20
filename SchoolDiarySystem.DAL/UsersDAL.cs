using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class UsersDAL : IBaseCRUD<Users>, IBaseConvert<Users>
    {
        public static Users Login(string username, string password)
        {
            return new Users();
        }

        public bool Create(Users model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Users model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Users model)
        {
            throw new NotImplementedException();
        }

        public Users Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Users ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
