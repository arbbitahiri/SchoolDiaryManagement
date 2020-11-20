using System;

namespace SchoolDiarySystem.Models
{
    public class Users : BaseAuditObject
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ExpiresDate { get; set; }
        public DateTime AbsenceDate { get; set; }
        public string AbsenceReasoning { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public bool IsPasswordChanged { get; set; }
        public int TeacherID { get; set; }
        public int ParentID { get; set; }
    }
}