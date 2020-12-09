using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class TeacherBirthDate : ValidationAttribute
    {
        public TeacherBirthDate()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime birthdate = Convert.ToDateTime(value);
                var age = GetAge(birthdate);
                if (age >= 18 && age <= 64)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Teacher must be between 18 to 64 years old!");
                }
            }
            return new ValidationResult("Please select your birthday!");
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}