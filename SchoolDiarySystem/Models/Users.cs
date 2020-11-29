using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Users : BaseAuditObject
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime ExpiresDate { get; set; }
        public DateTime AbsenceDate { get; set; }
        public string AbsenceReasoning { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LastLoginDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime LastLoginTime { get; set; }
        public DateTime LastPasswordChangeDate { get; set; }
        public bool IsPasswordChanged { get; set; }
        public int TeacherID { get; set; }
        public int ParentID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual Roles Role { get; set; }
        public virtual Parents Parent { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}