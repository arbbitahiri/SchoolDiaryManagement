using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

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

        public static List<Subjects> GetSubjectForTeacher(int teacherID, List<Subjects> subjects)
        {
            List<Subjects> teacherSubjects = new List<Subjects>();
            foreach (var teacher in subjects)
            {
                if (teacherID == teacher.TeacherID)
                {
                    teacherSubjects.Add(teacher);
                }
            }
            return teacherSubjects;
        }
    }
}