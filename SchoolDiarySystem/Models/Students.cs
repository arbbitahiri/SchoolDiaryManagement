using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolDiarySystem.Models
{
    public class Students : BaseAuditObject
    {
        public int StudentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please write student's first name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please write student's last name!")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        //[Required(ErrorMessage = "Please select a gender!")]
        public string Gender { get; set; }

        [Display(Name = "Day of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
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

        public IEnumerable<SelectListItem> ParentsList { get; set; }
        public IEnumerable<SelectListItem> ClassesList { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
    }
}