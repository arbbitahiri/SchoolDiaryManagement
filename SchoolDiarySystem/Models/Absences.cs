using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiarySystem.Models
{
    public class Absences : BaseAuditObject
    {
        public int AbsenceID { get; set; }
        public string AbsenceReasoning { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int Time { get; set; }
        public DateTime AbsenceDate { get; set; }

        public virtual Students Student { get; set; }
        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}