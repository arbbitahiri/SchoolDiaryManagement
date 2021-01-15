using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using SchoolDiarySystem.Models.DataAnnotations;
using System;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(Users users)
        {
            string password = Validation.CalculateHASH(users.Password);
            var result = usersDAL.Login(users.Username, password);
            if (result != null)
            {
                UserSession.GetUsers = result;
                if (result.ExpiresDate > DateTime.Now)
                {
                    if (result.Role.RoleName == UserRoles.ADMIN)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.Role.RoleName == UserRoles.TEACHER)
                    {
                        return RedirectToAction("Index", "Professor");
                    }
                    else if (result.Role.RoleName == UserRoles.DIRECTOR)
                    {
                        return RedirectToAction("Index", "Director");
                    }
                    else if (result.Role.RoleName == UserRoles.PARENT)
                    {
                        return RedirectToAction("Index", "MyKids");
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
                ModelState.AddModelError(string.Empty, "Username or password is incorrect!");
            }
            return View(users);
        }

        public ActionResult Logout()
        {
            UserSession.GetUsers = null;
            return RedirectToAction("Login", "Account");
        }
    }
}