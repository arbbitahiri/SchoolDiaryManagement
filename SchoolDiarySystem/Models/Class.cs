using SchoolDiarySystem.Models.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Class : BaseAuditObject
    {
        public int ClassID { get; set; }

        [Display(Name = "Teacher")]
        [Required(ErrorMessage = "Please select a teacher!")]
        public int TeacherID { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Please write a class number, between 1 and 9")]
        [ValidateClassNo]
        public int ClassNo { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Please select a room!")]
        public int RoomID { get; set; }

        public virtual Teachers Teacher { get; set; }
        public virtual Rooms Room { get; set; }
    }
}