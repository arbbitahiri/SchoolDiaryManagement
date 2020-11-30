using System.ComponentModel.DataAnnotations;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class ValidateClassNo : ValidationAttribute
    {
        public int MaxLength { get; set; }

        public ValidateClassNo() : base ("Write a number between 1 and 9 only!")
        {
            MaxLength = 1;
        }

        public override bool IsValid(object value)
        {
            string strValue = value as string;
            int x = int.Parse(value.ToString());

            if (!string.IsNullOrEmpty(strValue))
            {
                if (strValue.Length == MaxLength)
                {
                    if (x < 10 && x > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}