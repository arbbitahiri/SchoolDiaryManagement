using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewsDAL reviewsDAL = new ReviewsDAL();
        private readonly CommentsDAL commentsDAL = new CommentsDAL();

        // GET: Review
        public async Task<ActionResult> Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                var reviews = await Task.Run(() => reviewsDAL.GetAll());

                if (!string.IsNullOrEmpty(searchString))
                {
                    reviews = reviews.Where(f => f.ReviewDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                }

                if (!string.IsNullOrEmpty(searchString2))
                {
                    reviews = reviews.Where(f => f.Comment.Subject.SubjectTitle == searchString2).ToList();
                }

                return View(reviews);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var review = await Task.Run(() => reviewsDAL.Get((int)id));
                if (review == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                GetComment(review);
                return View(review);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, Reviews review)
        {
            if (UserSession.GetUsers != null)
            {
                if (id != review.ReviewID)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        review.LUB = UserSession.GetUsers.Username;
                        review.LUN = ++review.LUN;

                        var result = await Task.Run(() => reviewsDAL.Update(review));
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                        return View(review);
                    }
                }
                return View(review);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var review = await Task.Run(() => reviewsDAL.Get((int)id));
                if (review == null)
                {
                    return RedirectToAction("Index");
                }
                GetComment(review);
                return View(review);
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