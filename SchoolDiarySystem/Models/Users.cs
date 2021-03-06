﻿using SchoolDiarySystem.Models.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models
{
    public class Users : BaseAuditObject
    {
        public int UserID { get; set; }

        [Display(Name = "Username")]
        //[Required(ErrorMessage = "Please write user's username!")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Please write user's password!")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password doesn't not match!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        public int RoleID { get; set; }

        [Display(Name = "First Name")]
        [CheckIsLetter]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [CheckIsLetter]
        public string LastName { get; set; }

        [Display(Name = "Expire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime ExpiresDate { get; set; }

        [Display(Name = "Last Login Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LastLoginDate { get; set; }

        [Display(Name = "Last Login Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime LastLoginTime { get; set; }

        [Display(Name = "Last Date Password Has Changed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime LastPasswordChangeDate { get; set; }

        public bool IsPasswordChanged { get; set; }

        [Display(Name = "Teacher")]
        public int TeacherID { get; set; }

        [Display(Name = "Parent")]
        public int ParentID { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public virtual Roles Role { get; set; }
        public virtual Parents Parent { get; set; }
        public virtual Teachers Teacher { get; set; }
    }
}