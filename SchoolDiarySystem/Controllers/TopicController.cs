using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class TopicController : Controller
    {
        private readonly TopicsDAL topicsDAL = new TopicsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();

        // GET: Topic
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var topics = topicsDAL.GetAll();
                return View(topics);
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

                var topics = topicsDAL.Get((int)id);
                if (topics == null)
                {
                    return RedirectToAction("Index");
                }
                GetParentsClassesGenders(topics);
                return View(topics);
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

                var topics = topicsDAL.Get((int)id);
                if (topics == null)
                {
                    return RedirectToAction("Index");
                }
                return View(topics);
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

                var topics = topicsDAL.Get((int)id);
                if (topics == null)
                {
                    return RedirectToAction("Index");
                }
                return View(topics);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetSubjectAndClass()
        {
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            ViewBag.Time = new SelectList(times, "Time");
            ViewBag.SubjectID = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle");
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo");
        }

        private void GetSubjectAndClass(Topics topic)
        {
            ViewBag.SubjectID = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle", topic.SubjectID);
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo", topic.ClassID);
        }
    }
}