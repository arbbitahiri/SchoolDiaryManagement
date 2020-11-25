using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ParentController : Controller
    {
        private readonly ParentsDAL parentsDAL = new ParentsDAL();

        // GET: Parent
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var students = parentsDAL.GetAll();
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

                var teachers = parentsDAL.Get((int)id);
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

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var teachers = parentsDAL.Get((int)id);
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

                var teachers = parentsDAL.Get((int)id);
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
    }
}