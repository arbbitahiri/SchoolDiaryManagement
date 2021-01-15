using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly ReviewsDAL reviewsDAL = new ReviewsDAL();

        private readonly int teacher = !string.IsNullOrEmpty(UserSession.GetUsers.TeacherID.ToString()) ? UserSession.GetUsers.TeacherID : 0;

        // GET: Comment
        public ActionResult Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    var comments = commentsDAL.GetAllForTeacher(teacher);

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        comments = comments.Where(f => f.CommentDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        comments = comments.Where(f => f.Student.FirstName.ToLower() == searchString2.ToLower()
                        || f.Student.LastName.ToLower() == searchString2.ToLower() || f.Student.FullName.ToLower() == searchString2.ToLower()).ToList();
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

        public ActionResult Create()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    var comment = new Comments();
                    GetItemForSelectList();
                    return View(comment);
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
        public ActionResult Create(Comments comment)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    try
                    {
                        GetItemForSelectList();
                        var errors = ModelState.Values.SelectMany(s => s.Errors);
                        if (ModelState.IsValid)
                        {
                            comment.InsertBy = UserSession.GetUsers.Username;
                            comment.LUB = UserSession.GetUsers.Username;
                            comment.LUN++;

                            var result = commentsDAL.Create(comment);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(comment);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(comment);
                    }
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var comment = commentsDAL.Get((int)id);
                    if (comment == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return View(comment);
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
        public ActionResult Update(int id, Comments comment)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    if (id != comment.CommentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            comment.LUB = UserSession.GetUsers.Username;
                            comment.LUN = ++comment.LUN;

                            var result = commentsDAL.Update(comment);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(comment);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(comment);
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

        public ActionResult ReviewDetails(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR || UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var review = reviewsDAL.Get((int)id);
                    if (review == null)
                    {
                        return RedirectToAction("Index");
                    }
                    GetComment(review);
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

        private void GetComment(Reviews reviews)
        {
            if (reviews != null)
            {
                var comment = commentsDAL.Get(reviews.CommentID);

                ViewBag.CommentID = comment.Content;
            }
        }

        private void GetItemForSelectList()
        {
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Subject = subjectsDAL.GetAllForTeacher(teacher);
            ViewBag.Student = studentsDAL.GetAllForTeacher(teacher);
            ViewBag.Times = times;
        }
    }
}