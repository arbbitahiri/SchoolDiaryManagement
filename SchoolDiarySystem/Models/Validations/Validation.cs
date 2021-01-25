using System;
using System.Security.Cryptography;
using System.Text;

namespace SchoolDiarySystem.Models.DataAnnotations
{
    public class Validation
    {
        public static string CalculateHASH(string password)
        {
            var inputBuffer = Encoding.Unicode.GetBytes(password);

            byte[] hashBytes;
            using (var sha = new SHA256Managed())
            {
                hashBytes = sha.ComputeHash(inputBuffer);
            }

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}