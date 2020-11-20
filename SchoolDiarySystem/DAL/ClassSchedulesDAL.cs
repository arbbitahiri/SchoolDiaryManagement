using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class ClassSchedulesDAL : IBaseCRUD<ClassSchedules>, IBaseConvert<ClassSchedules>
    {
        public bool Create(ClassSchedules model)
        {
            throw new NotImplementedException();
        }

        public bool Update(ClassSchedules model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ClassSchedules model)
        {
            throw new NotImplementedException();
        }

        public ClassSchedules Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClassSchedules> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClassSchedules ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
