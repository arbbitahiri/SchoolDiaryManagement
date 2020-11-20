namespace SchoolDiarySystem.Models
{
    public class Parents : BaseAuditObject
    {
        public int ParentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
    }
}