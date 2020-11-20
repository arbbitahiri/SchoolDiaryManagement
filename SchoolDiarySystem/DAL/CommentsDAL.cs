using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class CommentsDAL : IBaseCRUD<Comments>, IBaseConvert<Comments>
    {
        public bool Create(Comments model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Comments model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Comments model)
        {
            throw new NotImplementedException();
        }

        public Comments Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comments> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comments ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
