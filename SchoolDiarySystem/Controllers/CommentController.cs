using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();

        // GET: Comment
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var comments = commentsDAL.GetAll();
                return View(comments);
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
                GetSubjectAndClass();
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

                var comments = commentsDAL.Get((int)id);
                if (comments == null)
                {
                    return RedirectToAction("Index");
                }
                GetSubjectAndClass(comments);
                return View(comments);
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

                var comments = commentsDAL.Get((int)id);
                if (comments == null)
                {
                    return RedirectToAction("Index");
                }
                return View(comments);
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

                var comments = commentsDAL.Get((int)id);
                if (comments == null)
                {
                    return RedirectToAction("Index");
                }
                return View(comments);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetSubjectAndClass()
        {
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Time = new SelectList(times, "Time");
            ViewBag.SubjectID = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle");
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo");
        }

        private void GetSubjectAndClass(Comments comments)
        {
            ViewBag.SubjectID = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle", comments.SubjectID);
            ViewBag.ClassID = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo", comments.ClassID);
        }
    }
}