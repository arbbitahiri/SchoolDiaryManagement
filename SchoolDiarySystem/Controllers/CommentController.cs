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
    public class CommentController : Controller
    {
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly ReviewsDAL reviewsDAL = new ReviewsDAL();
        private readonly int teacher = UserSession.GetUsers.TeacherID;

        // GET: Comment
        public async Task<ActionResult> Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var comments = await Task.Run(() => commentsDAL.GetAllForTeacher(teacher));

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        comments = comments.Where(f => f.CommentDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        comments = comments.Where(f => f.Student.FirstName == searchString2 || f.Student.LastName == searchString2 || f.Student.FullName == searchString2).ToList();
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
                if (UserSession.GetUsers.RoleID == 2)
                {
                    IEnumerable<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };
                    var comment = new Comments()
                    {
                        SubjectsList = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle"),
                        StudentsList = new SelectList(studentsDAL.GetAll(), "StudentID", "FullName"),
                        Times = new SelectList(times)
                    };
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
        public async Task<ActionResult> Create(Comments comment)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    try
                    {
                        var errors = ModelState.Values.SelectMany(s => s.Errors);
                        if (ModelState.IsValid)
                        {
                            comment.InsertBy = UserSession.GetUsers.Username;
                            comment.LUB = UserSession.GetUsers.Username;
                            comment.LUN++;

                            var result = await Task.Run(() => commentsDAL.Create(comment));
                            return RedirectToAction(nameof(Index));
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

        public async Task<ActionResult> Review(int? id)
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
        public async Task<ActionResult> Review(int id, Reviews review)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id != review.CommentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            review.LUB = UserSession.GetUsers.Username;
                            review.LUN = ++review.LUN;

                            var result = await Task.Run(() => reviewsDAL.Create(review));
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
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var comment = await Task.Run(() => commentsDAL.Get((int)id));
                    if (comment == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    comment.SubjectsList = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle", comment.SubjectID);
                    comment.StudentsList = new SelectList(studentsDAL.GetAll(), "StudentID", "FullName", comment.StudentID);
                    IEnumerable<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };
                    comment.Times = new SelectList(times);

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
        public async Task<ActionResult> Update(int id, Comments comment)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id != comment.CommentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            comment.LUB = UserSession.GetUsers.Username;
                            comment.LUN = ++comment.LUN;

                            var result = await Task.Run(() => commentsDAL.Update(comment));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(comment);
                        }
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

        public async Task<ActionResult> Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var comment = await Task.Run(() => commentsDAL.Get((int)id));
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

        public async Task<ActionResult> ReviewDetails(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3 || UserSession.GetUsers.RoleID == 2)
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

        private void GetTimes()
        {
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Time = new SelectList(times, "Time");
        }
    }
}