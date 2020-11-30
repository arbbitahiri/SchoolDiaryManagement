using System;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class DirectorController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();

        // GET: Director
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var users = usersDAL.GetAll();
                    foreach (var user in users)
                    {
                        if (user.RoleID == 3)
                        {
                            return View(user);
                        }
                    }
                    return View();
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