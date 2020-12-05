using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class CheckPhoneNo : ValidationAttribute
    {
        public int MaxLength { get; set; }

        public CheckPhoneNo() : base("{0} is not a valid number. Example: +38349700700")
        {
            MaxLength = 12;
        }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                return false;
            }
            else
            {
                string strValue = value.ToString();
                if (!string.IsNullOrEmpty(strValue))
                {
                    int strLength = strValue.Length;
                    if (strLength == MaxLength)
                    {
                        if (strValue[0] != '+')
                        {
                            return false;
                        }
                        else
                        {
                            string newStr = strValue.Remove(0, 1);
                            bool IsANumber = newStr.All(char.IsDigit);
                            if (!IsANumber)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
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
}