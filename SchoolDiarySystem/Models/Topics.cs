using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Topics : BaseAuditObject
    {
        public int TopicID { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "Please write topic's content!")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Content is to short!")]
        public string Content { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Please select a class!")]
        public int ClassID { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Please select a subject!")]
        public int SubjectID { get; set; }

        [Display(Name = "Time")]
        [Required(ErrorMessage = "Please select a time!")]
        public int Time { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        [Required(ErrorMessage = "Please select a date!")]
        public DateTime TopicDate { get; set; }

        public virtual Class Class { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}