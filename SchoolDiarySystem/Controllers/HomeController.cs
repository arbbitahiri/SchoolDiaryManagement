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
    public class HomeController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Users users, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(users);
            }

            var result = await Task.Run(() => usersDAL.Login(users.Username, users.Password));
            if (result != null)
            {
                UserSession.GetUsers = result;
                if (result.ExpiresDate > DateTime.Now)
                {
                    if (result.RoleID == 1)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.RoleID == 2)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.RoleID == 3)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.RoleID == 4)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "You don't have access!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Your user has expired!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Username or Password is incorrect!");
                return View(users);
            }
            return View(users);
        }
    }
}