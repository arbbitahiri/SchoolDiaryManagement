namespace SchoolDiarySystem.Models
{
    public class Class : BaseAuditObject
    {
        public int ClassID { get; set; }
        public int TeacherID { get; set; }
        public int ClassNo { get; set; }
        public int RoomID { get; set; }

        public virtual Teachers Teacher { get; set; }
        public virtual Rooms Room { get; set; }
    }
}