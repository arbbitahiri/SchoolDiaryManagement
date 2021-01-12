using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class CheckIsLetter : ValidationAttribute
    {
        public CheckIsLetter() : base("Use only letters!")
        {

        }

        public override bool IsValid(object value)
        {
            string strValue = value as string;

            if (!string.IsNullOrEmpty(strValue))
            {
                bool isStr = strValue.All(char.IsLetter);
                if (isStr)
                {
                    return true;
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