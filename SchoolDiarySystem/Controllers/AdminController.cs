using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using SchoolDiarySystem.Models.Dashboard;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();

        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    NumbersCount();
                    return View();
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
                NoTeachers = teachersDAL.Count(),
                NoParents = parentsDAL.Count(),
                NoStudents = studentsDAL.Count()
            };

            ViewBag.Teachers = statistics.NoTeachers;
            ViewBag.Parents = statistics.NoParents;
            ViewBag.Students = statistics.NoStudents;
        }
    }
}