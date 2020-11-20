using System;

namespace SchoolDiarySystem.Models
{
    public class Students : BaseAuditObject
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DayofBirth { get; set; }
        public int ClassID { get; set; }
        public int ParentID { get; set; }

        public virtual Class Class { get; set; }
        public virtual Parents Parent { get; set; }
    }
}