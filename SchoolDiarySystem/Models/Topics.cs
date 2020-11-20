using System;

namespace SchoolDiarySystem.Models
{
    public class Topics : BaseAuditObject
    {
        public int TopicID { get; set; }
        public string Content { get; set; }
        public int ClassID { get; set; }
        public int SubjectID { get; set; }
        public int Time { get; set; }
        public DateTime TopicDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}