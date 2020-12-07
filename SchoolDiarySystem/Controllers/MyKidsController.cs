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
        private readonly int parent = UserSession.GetUsers.ParentID;

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

        public async Task<ActionResult> IndexClass(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => classesDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.Student.FirstName == searchString || f.Student.LastName == searchString
                        || f.Student.FullName == searchString).ToList();
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

        public async Task<ActionResult> IndexComment(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => commentsDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.Student.FirstName == searchString || f.Student.LastName == searchString
                        || f.Student.FullName == searchString).ToList();
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

        public async Task<ActionResult> IndexTopic(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 4)
                {
                    var kids = await Task.Run(() => topicsDAL.GetAllForParent(parent));
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        kids = kids.Where(f => f.Student.FirstName == searchString || f.Student.LastName == searchString
                        || f.Student.FullName == searchString).ToList();
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
    }
}