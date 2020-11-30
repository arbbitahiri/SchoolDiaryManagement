using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Reviews : BaseAuditObject
    {
        public int ReviewID { get; set; }

        [Display(Name = "Comment")]
        public int CommentID { get; set; }

        [Display(Name = "Review")]
        [Required(ErrorMessage = "Please write the review!")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Review is to short!")]
        public string Review { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        public DateTime ReviewDate { get; set; }

        public virtual Comments Comment { get; set; }
    }
}