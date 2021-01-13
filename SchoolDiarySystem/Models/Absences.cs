using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Absences : BaseAuditObject
    {
        public int AbsenceID { get; set; }

        [Display(Name = "Absence Reasoning")]
        [Required(ErrorMessage = "Please select a reason!")]
        public string AbsenceReasoning { get; set; }

        [Display(Name = "Student")]
        [Required(ErrorMessage = "Please select a student!")]
        public int StudentID { get; set; }

        [Display(Name = "Class")]
        //[Required(ErrorMessage = "Please select a class!")]
        public int ClassID { get; set; }

        [Display(Name = "Subject")]
        public int SubjectID { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        public DateTime AbsenceDate { get; set; }

        public virtual Students Student { get; set; }
        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}