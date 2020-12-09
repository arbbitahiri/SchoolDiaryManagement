using SchoolDiarySystem.Models;
using SchoolDiarySystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SchoolDiarySystem.Controllers
{
    public class MyKidsController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ClassDAL classesDAL = new ClassDAL();
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly TopicsDAL topicsDAL = new TopicsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        private readonly int parent = !string.IsNullOrEmpty(UserSession.GetUsers.ParentID.ToString()) ? UserSession.GetUsers.ParentID : 0;

        // GET: MyKids
        public async Task<ActionResult> Index()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => studentsDAL.GetAll());
                    kids = kids.Where(k => k.ParentID == UserSession.GetUsers.ParentID).ToList();

                    return View(kids);
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

        public async Task<ActionResult> IndexClass(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => classesDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.ClassNo == int.Parse(searchString)).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        kids = kids.Where(f => f.Student.FirstName.ToLower() == searchString2.ToLower() || f.Student.LastName.ToLower() == searchString2.ToLower()
                        || f.Student.FullName.ToLower() == searchString2.ToLower()).ToList();
                    }
                    return View(kids);
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

        public async Task<ActionResult> IndexComment(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => commentsDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString3))
                    {
                        kids = kids.Where(f => f.Student.FirstName.ToLower() == searchString3.ToLower() || f.Student.LastName.ToLower() == searchString3.ToLower()
                        || f.Student.FullName.ToLower() == searchString3.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        kids = kids.Where(f => f.Subject.SubjectTitle.ToLower() == searchString2.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.CommentDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }
                    return View(kids);
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

        public async Task<ActionResult> IndexTopic(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => topicsDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString3))
                    {
                        kids = kids.Where(f => f.Student.FirstName.ToLower() == searchString3.ToLower() || f.Student.LastName.ToLower() == searchString3.ToLower()
                        || f.Student.FullName.ToLower() == searchString3.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        kids = kids.Where(f => f.Subject.SubjectTitle.ToLower() == searchString2.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.TopicDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }
                    return View(kids);
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


        public async Task<ActionResult> TeacherDetails(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = await Task.Run(() => teachersDAL.Get((int)id));
                    if (teacher == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(teacher);
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