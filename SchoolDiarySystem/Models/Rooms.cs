using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Rooms : BaseAuditObject
    {
        public int RoomID { get; set; }

        [Display(Name = "Room Number")]
        [Required(ErrorMessage = "Please write a room number!")]
        public int RoomNo { get; set; }

        [Display(Name = "Room Type")]
        [Required(ErrorMessage = "Please write the type of room!")]
        public string RoomType { get; set; }
    }
}