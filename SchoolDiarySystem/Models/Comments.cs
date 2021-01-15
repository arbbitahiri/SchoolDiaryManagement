using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Comments : BaseAuditObject
    {
        public int CommentID { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please write a comment!")]
        public string Content { get; set; }

        [Display(Name = "Student")]
        public int StudentID { get; set; }

        [Display(Name = "Subject")]
        public int SubjectID { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please select a time!")]
        public int Time { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        public DateTime CommentDate { get; set; }

        public virtual Subjects Subject { get; set; }
        public virtual Students Student { get; set; }
        public virtual Reviews Review { get; set; }
    }
}