using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SchoolDiarySystem.Models
{
    public class Comments : BaseAuditObject
    {
        public int CommentID { get; set; }

        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Please write a comment!")]
        //[StringLength(250, MinimumLength = 5, ErrorMessage = "Comment is to short!")]
        public string Content { get; set; }

        [Display(Name = "Student")]
        //[Required(ErrorMessage = "Please select a student!")]
        public int StudentID { get; set; }

        [Display(Name = "Subject")]
        //[Required(ErrorMessage = "Please select a subject!")]
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

        public IEnumerable<SelectListItem> SubjectsList { get; set; }
        public IEnumerable<SelectListItem> StudentsList { get; set; }
        public IEnumerable<SelectListItem> Times { get; set; }
    }
}