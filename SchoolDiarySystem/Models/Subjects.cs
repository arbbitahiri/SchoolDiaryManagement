namespace SchoolDiarySystem.Models
{
    public class Subjects : BaseAuditObject
    {
        public int SubjectID { get; set; }
        public string SubjectTitle { get; set; }
        public string Book { get; set; }
        public string BookAuthor { get; set; }
        public int TeacherID { get; set; }

        public virtual Teachers Teacher { get; set; }
    }
}