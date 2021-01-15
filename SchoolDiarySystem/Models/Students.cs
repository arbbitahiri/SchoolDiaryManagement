using SchoolDiarySystem.Models.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Students : BaseAuditObject
    {
        public int StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please write student's first name!")]
        [CheckIsLetter]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please write student's last name!")]
        [CheckIsLetter]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Day of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        [StudentsBirthDate]
        public DateTime DayofBirth { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Please select a class!")]
        public int ClassID { get; set; }

        [Display(Name = "Parent")]
        [Required(ErrorMessage = "Please select a parent!")]
        public int ParentID { get; set; }

        public virtual Class Class { get; set; }
        public virtual Parents Parent { get; set; }


        [Display(Name = "Student")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}