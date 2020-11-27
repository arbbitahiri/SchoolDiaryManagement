using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Comments : BaseAuditObject
    {
        public int CommentID { get; set; }
        public string Comment { get; set; }
        public int StudentID { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int Time { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime CommentDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
        public virtual Students Student { get; set; }
        public virtual Reviews Review { get; set; }
    }
}