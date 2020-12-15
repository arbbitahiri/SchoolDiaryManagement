using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class StaffAbsence : BaseAuditObject
    {
        public int StaffAbsenceID { get; set; }

        [Display(Name = "Staff")]
        public int UserID { get; set; }

        [Display(Name = "Absence Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime AbsenceDate { get; set; }

        [Display(Name = "Absence Reason")]
        public string AbsenceReasoning { get; set; }

        public virtual Users User { get; set; }
    }
}