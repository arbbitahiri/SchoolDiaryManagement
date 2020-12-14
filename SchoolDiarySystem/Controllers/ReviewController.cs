﻿using SchoolDiarySystem.DAL;
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
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var reviews = await Task.Run(() => reviewsDAL.GetAll());

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

        public async Task<ActionResult> ReviewComment(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
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
        public async Task<ActionResult> ReviewComment(Reviews model)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var error = ModelState.Values.SelectMany(e => e.Errors);
                    //if (id != review.CommentID)
                    //{
                    //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    //}

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            model.LUB = UserSession.GetUsers.Username;
                            model.LUN = ++model.LUN;

                            var result = await Task.Run(() => reviewsDAL.Create(model));
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

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
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
        public async Task<ActionResult> Update(int id, Reviews review)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
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

        public async Task<ActionResult> CommentList(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var comments = await Task.Run(() => commentsDAL.GetAll());

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