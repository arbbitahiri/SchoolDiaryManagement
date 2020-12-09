using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class StudentsBirthDate : ValidationAttribute
    {
        public StudentsBirthDate()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime birthdate = Convert.ToDateTime(value);
                var age = GetAge(birthdate);
                if (age >= 5 && age <= 15)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("Student must be between 5 to 15 years old!");
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