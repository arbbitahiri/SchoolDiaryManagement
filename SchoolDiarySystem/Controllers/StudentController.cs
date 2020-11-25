using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();

        // GET: Student
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var students = studentsDAL.GetAll();
                return View(students);
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
                GetParentsClassesGenders();
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

                var students = studentsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                GetParentsClassesGenders();
                GetParentsAndClass(students);
                return View(students);
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

                var students = studentsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                return View(students);
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

                var students = studentsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetParentsClassesGenders()
        {
            List<string> genders = new List<string>() { "Male", "Female" };

            ViewBag.ParentID = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName");
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo");
            ViewBag.GenderID = new SelectList(genders, "GenderID");
        }

        private void GetParentsAndClass(Students students)
        {
            ViewBag.ParentID = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName", students.ParentID);
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo", students.ClassID);
        }
    }
}