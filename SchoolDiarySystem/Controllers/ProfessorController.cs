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
        public async Task<ActionResult> Index()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => studentsDAL.GetAll());
                    kids = kids.Where(k => k.ClassID == UserSession.GetUsers.).ToList();

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