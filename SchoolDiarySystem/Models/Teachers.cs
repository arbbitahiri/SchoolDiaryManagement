using System;
using System.Collections;
using System.Collections.Generic;

namespace SchoolDiarySystem.Models
{
    public class Teachers : BaseAuditObject
    {
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Qualification { get; set; }
        public DateTime DayofBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}