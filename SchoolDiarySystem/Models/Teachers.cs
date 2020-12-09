using SchoolDiarySystem.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolDiarySystem.Models
{
    public class Teachers : BaseAuditObject
    {
        public int TeacherID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please write teacher's first name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please write teacher's last name!")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        //[Required(ErrorMessage = "Please select a gender!")]
        public string Gender { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please write teacher's city!")]
        public string City { get; set; }

        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Please write teacher's qualification!")]
        public string Qualification { get; set; }

        [Display(Name = "Day of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        [TeacherBirthDate]
        public DateTime DayofBirth { get; set; }

        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "E-mail is not valid!")]
        [Required(ErrorMessage = "Please write an e-mail!")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please write a phone number!")]
        [CheckPhoneNo]
        public string PhoneNo { get; set; }


        [Display(Name = "Teacher")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public IEnumerable<SelectListItem> GenderEnumeration { get; set; }
    }
}