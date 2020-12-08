using SchoolDiarySystem.Models;
using SchoolDiarySystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SchoolDiarySystem.Models.Dashboard;

namespace SchoolDiarySystem.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly int teacher = !string.IsNullOrEmpty(UserSession.GetUsers.TeacherID.ToString()) ? UserSession.GetUsers.TeacherID : 0;

        // GET: Professor
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var students = await Task.Run(() => studentsDAL.GetMyStudents(teacher));

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
                NoStudents = studentsDAL.Count()
            };
            ViewBag.Students = statistics.NoStudents;
        }
    }
}