using System;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();
        private readonly RolesDAL rolesDAL = new RolesDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: User
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var users = usersDAL.GetAll();
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CreateAdmin()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CreateTeacher()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    GetTeacher();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult CreateParent()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    GetParent();
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Account");
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var user = usersDAL.Get((int)id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                GetParentsAndTeachers(user);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ChangePassword(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var user = usersDAL.Get((int)id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                GetParentsAndTeachers(user);
                return View(user);
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

                var user = usersDAL.Get((int)id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                return View(user);
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

                var user = usersDAL.Get((int)id);
                if (user == null)
                {
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetParentsAndTeachers(Users users)
        {
            ViewBag.ParentID = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName", users.ParentID);
            ViewBag.TeacherID = new SelectList(teachersDAL.GetAll(), "TeacherID", "FullName", users.TeacherID);
        }

        private void GetParent()
        {
            ViewBag.ParentID = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName");
        }

        private void GetTeacher()
        {
            ViewBag.TeacherID = new SelectList(teachersDAL.GetAll(), "TeacherID", "FullName");
        }
    }
}