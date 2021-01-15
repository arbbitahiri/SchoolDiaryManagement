using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class MyKidsController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ClassDAL classesDAL = new ClassDAL();
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
        private readonly TopicsDAL topicsDAL = new TopicsDAL();
        private readonly AbsencesDAL absencesDAL = new AbsencesDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        private readonly int parent = !string.IsNullOrEmpty(UserSession.GetUsers.ParentID.ToString()) ? UserSession.GetUsers.ParentID : 0;

        // GET: MyKids
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    var kids = studentsDAL.GetAll();
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

        public ActionResult IndexTopic(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    var kids = topicsDAL.GetAllForParent(parent);
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

        public ActionResult IndexAbsence(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    var absences = absencesDAL.GetAllForParent(parent);

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        absences = absences.Where(f => f.AbsenceDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        absences = absences.Where(f => f.Student.FirstName.ToLower() == searchString2.ToLower() || f.Student.LastName.ToLower() == searchString2.ToLower() || f.Student.FullName.ToLower() == searchString2.ToLower()).ToList();
                    }

                    return View(absences);
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

        public ActionResult IndexComment(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    var kids = commentsDAL.GetAllForParent(parent);
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

        public ActionResult IndexClass(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    var kids = classesDAL.GetAllForParent(parent);
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

        public ActionResult TeacherDetails(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.PARENT)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = teachersDAL.Get((int)id);
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