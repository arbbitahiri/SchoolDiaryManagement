using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SchoolDiarySystem.Models
{
    public class Students : BaseAuditObject
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime DayofBirth { get; set; }
        public int ClassID { get; set; }
        public int ParentID { get; set; }

        public virtual Class Class { get; set; }
        public virtual Parents Parent { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public SelectList ParentsList { get; set; }
        public SelectList ClassesList { get; set; }
    }
}