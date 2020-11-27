using System;

namespace SchoolDiarySystem.Models
{
    public class Comments : BaseAuditObject
    {
        public int CommentID { get; set; }
        public string Comment { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int Time { get; set; }
        public DateTime CommentDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}