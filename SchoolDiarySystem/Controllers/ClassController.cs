using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ClassController : Controller
    {
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly RoomsDAL roomsDAL = new RoomsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Class
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var classes = classDAL.GetAll();
                return View(classes);
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
                GetTeachersAndRoom();
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

                var students = classDAL.Get((int)id);
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

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var students = classDAL.Get((int)id);
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

                var students = classDAL.Get((int)id);
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

        private void GetTeachersAndRoom()
        {
            ViewBag.TeacherID = new SelectList(teachersDAL.GetAll(), "TeacherID", "FullName");
            ViewBag.RoomID = new SelectList(roomsDAL.GetAll(), "RoomID", "RoomType");
        }
    }
}