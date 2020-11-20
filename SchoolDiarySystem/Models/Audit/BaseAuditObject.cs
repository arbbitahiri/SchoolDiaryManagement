using System;

namespace SchoolDiarySystem.Models
{
    public class BaseAuditObject
    {
        public int LUN { get; set; }
        public DateTime LUD { get; set; }
        public string LUB { get; set; }
        public DateTime InsertDate { get; set; }
        public string InsertBy { get; set; }
    }
}