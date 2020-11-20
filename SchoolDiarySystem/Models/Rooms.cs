namespace SchoolDiarySystem.Models
{
    public class Rooms : BaseAuditObject
    {
        public int RoomID { get; set; }
        public int RoomNo { get; set; }
        public string RoomType { get; set; }
    }
}