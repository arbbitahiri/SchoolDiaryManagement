using SchoolDiarySystem.Interface;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;

namespace SchoolDiarySystem.BLL
{
    public class AdministrationBLL
    {
        public static Users Login(string username, string password)
        {
            return new Users();
        }
    }
}
