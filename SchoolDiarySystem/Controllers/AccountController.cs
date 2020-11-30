using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace SchoolDiarySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();

        public ActionResult Login() => View();

        [HttpPost]
        public async Task<ActionResult> Login(Users users)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(users);
            //}
            var result = await Task.Run(() => usersDAL.Login(users.Username, users.Password));
            if (result != null)
            {
                UserSession.GetUsers = result;

                if (result.ExpiresDate > DateTime.Now)
                {
                    if (result.RoleID == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (result.RoleID == 2)
                    {
                        return RedirectToAction("Index", "Teacher");
                    }
                    else if (result.RoleID == 3)
                    {
                        return RedirectToAction("Index", "Director");
                    }
                    else if (result.RoleID == 4)
                    {
                        return RedirectToAction("Index", "Parent");
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
            return View(users);
        }

        public ActionResult Logout()
        {
            UserSession.GetUsers = null;
            return RedirectToAction("Login", "Account");
        }
    }
}