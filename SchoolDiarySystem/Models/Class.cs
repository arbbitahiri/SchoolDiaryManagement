using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Class : BaseAuditObject
    {
        public int ClassID { get; set; }

        [Display(Name = "Teacher")]
        public int TeacherID { get; set; }

        [Display(Name = "Class")]
        [Range(1, 9, ErrorMessage = "Please write a class number, between 1 and 9")]
        public int ClassNo { get; set; }

        [Display(Name = "Room")]
        public int RoomID { get; set; }

        public virtual Teachers Teacher { get; set; }
        public virtual Rooms Room { get; set; }
        public virtual Students Student { get; set; }
    }
}