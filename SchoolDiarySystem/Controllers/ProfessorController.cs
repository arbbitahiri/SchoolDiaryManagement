using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using SchoolDiarySystem.Models.Dashboard;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly int teacher = !string.IsNullOrEmpty(UserSession.GetUsers.TeacherID.ToString()) ? UserSession.GetUsers.TeacherID : 0;

        // GET: Professor
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    var students = studentsDAL.GetMyStudents(teacher);

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        students = students.Where(f => f.FirstName == searchString || f.LastName == searchString || f.FullName == searchString).ToList();
                    }

                    NumbersCount();
                    return View(students);
                }
                else
                {
                    return Content("You're not allowed to view this page!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void NumbersCount()
        {
            Statistics statistics = new Statistics
            {
                NoStudents = studentsDAL.Count(teacher)
            };
            ViewBag.Students = statistics.NoStudents;
        }
    }
}