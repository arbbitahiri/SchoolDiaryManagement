﻿using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchoolDiarySystem.DAL
{
    public class AbsencesDAL : IBaseCRUD<Absences>, IBaseConvert<Absences>
    {
        public bool Create(Absences model)
        {
            throw new NotImplementedException();
        }

        public bool Update(Absences model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Absences model)
        {
            throw new NotImplementedException();
        }

        public Absences Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Absences> GetAll()
        {
            throw new NotImplementedException();
        }

        public Absences ToObject(SqlDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}
