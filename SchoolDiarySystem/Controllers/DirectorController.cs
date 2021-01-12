using System;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SchoolDiarySystem.Controllers
{
    public class DirectorController : Controller
    {
        private readonly CommentsDAL commentsDAL = new CommentsDAL();
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

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

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
    }
}