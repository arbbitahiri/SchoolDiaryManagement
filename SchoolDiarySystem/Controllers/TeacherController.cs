using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Teacher
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var teachers = teachersDAL.GetAll();
                return View(teachers);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Create()
        {
            if (UserSession.GetUsers != null)
            {
                GettingGenders();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var teachers = teachersDAL.Get((int)id);
                if (teachers == null)
                {
                    return RedirectToAction("Index");
                }
                GettingGenders();
                return View(teachers);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var teachers = teachersDAL.Get((int)id);
                if (teachers == null)
                {
                    return RedirectToAction("Index");
                }
                return View(teachers);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var teachers = teachersDAL.Get((int)id);
                if (teachers == null)
                {
                    return RedirectToAction("Index");
                }
                return View(teachers);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GettingGenders()
        {
            List<string> genders = new List<string>() { "Male", "Female" };
            ViewBag.GenderID = new SelectList(genders, "GenderID");
        }
    }
}