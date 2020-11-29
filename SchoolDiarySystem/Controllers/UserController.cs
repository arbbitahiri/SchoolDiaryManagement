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
    }
}