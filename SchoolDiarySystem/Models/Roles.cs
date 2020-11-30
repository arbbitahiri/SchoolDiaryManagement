using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Roles : BaseAuditObject
    {
        public int RoleID { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}