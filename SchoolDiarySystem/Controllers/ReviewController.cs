using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewsDAL reviewsDAL = new ReviewsDAL();
        private readonly CommentsDAL commentsDAL = new CommentsDAL();

        // GET: Review
        public ActionResult Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    var reviews = reviewsDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        reviews = reviews.Where(f => f.ReviewDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        reviews = reviews.Where(f => f.Comment.Subject.SubjectTitle.ToLower() == searchString2.ToLower()).ToList();
                    }

                    return View(reviews);
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

        public ActionResult ReviewComment(int? commentID)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (commentID == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var review = reviewsDAL.Get((int)commentID);
                    review.CommentID = (int)commentID;
                    if (review == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(review);
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

        [HttpPost]
        public ActionResult ReviewComment(int commentID, Reviews model)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    var error = ModelState.Values.SelectMany(e => e.Errors);
                    if (commentID != model.CommentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            model.CommentID = commentID;
                            model.LUB = UserSession.GetUsers.Username;
                            model.InsertBy = UserSession.GetUsers.Username;
                            model.LUN = ++model.LUN;

                            var result = reviewsDAL.Create(model);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(model);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(model);
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

        public ActionResult Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var review = reviewsDAL.Get((int)id);
                    if (review == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(review);
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

        [HttpPost]
        public ActionResult Update(int id, Reviews review)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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

                            var result = reviewsDAL.Update(review);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(review);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(review);
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

        public ActionResult CommentList(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    var comments = commentsDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        comments = comments.Where(f => f.CommentDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        comments = comments.Where(f => f.Subject.SubjectTitle == searchString2).ToList();
                    }

                    return View(comments);
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
    }
}