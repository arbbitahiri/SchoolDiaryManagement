using System;

namespace SchoolDiarySystem.Models
{
    public class Reviews : BaseAuditObject
    {
        public int ReviewID { get; set; }
        public int CommentID { get; set; }
        public string Review { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Comments Comment { get; set; }
    }
}