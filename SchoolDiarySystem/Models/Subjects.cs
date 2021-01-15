using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolDiarySystem.Models
{
    public class Subjects : BaseAuditObject
    {
        public int SubjectID { get; set; }

        [Display(Name = "Subject Title")]
        [Required(ErrorMessage = "Please write a subject!")]
        public string SubjectTitle { get; set; }

        [Display(Name = "Book")]
        [Required(ErrorMessage = "Please write book's name, for this subject!")]
        public string Book { get; set; }

        [Display(Name = "Book Author")]
        [Required(ErrorMessage = "Please write author of the book, for this subject!")]
        public string BookAuthor { get; set; }

        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "Please select a teacher!")]
        public int TeacherID { get; set; }

        public virtual Teachers Teacher { get; set; }
    }
}