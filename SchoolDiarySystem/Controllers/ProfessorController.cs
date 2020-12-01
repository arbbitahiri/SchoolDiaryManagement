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

        // GET: Professor
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var kids = await Task.Run(() => studentsDAL.GetMyStudents(UserSession.GetUsers.TeacherID));

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.FirstName == searchString || f.LastName == searchString || f.FullName == searchString).ToList();
                    }

                    NumbersCount();
                    return View(kids);
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