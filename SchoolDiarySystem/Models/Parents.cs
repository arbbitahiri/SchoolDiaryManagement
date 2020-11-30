using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Parents : BaseAuditObject
    {
        public int ParentID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please write parent's first name!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please write parent's last name!")]
        public string LastName { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please write city!")]
        public string City { get; set; }


        [Display(Name = "Parent")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}