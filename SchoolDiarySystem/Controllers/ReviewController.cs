using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewsDAL reviewsDAL = new ReviewsDAL();
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly StudentsDAL studentsDAL = new StudentsDAL();

        // GET: Review
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var reviews = reviewsDAL.GetAll();

                return View(reviews);
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

                var reviews = reviewsDAL.Get((int)id);
                if (reviews == null)
                {
                    return RedirectToAction("Index");
                }
                GetComment(reviews);
                return View(reviews);
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

                var reviews = reviewsDAL.Get((int)id);
                if (reviews == null)
                {
                    return RedirectToAction("Index");
                }
                GetComment(reviews);
                return View(reviews);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetComment(Reviews reviews)
        {
            if (reviews != null)
            {
                var comment = commentsDAL.Get(reviews.CommentID);

                ViewBag.CommentID = comment.Comment;
            }
        }
    }
}