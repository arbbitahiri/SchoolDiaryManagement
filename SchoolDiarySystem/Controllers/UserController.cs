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
    public class UserController : Controller
    {
        private readonly UsersDAL usersDAL = new UsersDAL();
        private readonly RolesDAL rolesDAL = new RolesDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: User
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var users = await Task.Run(() => usersDAL.GetAll());

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        users = users.Where(f => f.FirstName == searchString || f.LastName == searchString
                        || f.Username == searchString || f.FullName == searchString).ToList();
                    }

                    return View(users);
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
                    return Content("You're not allowed here!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAdmin(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            user.InsertBy = UserSession.GetUsers.Username;
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN++;
                            user.RoleID = 1;

                            var result = await Task.Run(() => usersDAL.Create(user));
                            return RedirectToAction(nameof(Index));
                        }
                        return View(user);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(user);
                    }
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
                    return Content("You're not allowed here!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTeacher(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            user.InsertBy = UserSession.GetUsers.Username;
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN++;
                            user.RoleID = 2;

                            var result = await Task.Run(() => usersDAL.Create(user));
                            return RedirectToAction(nameof(Index));
                        }
                        return View(user);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(user);
                    }
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
                    return Content("You're not allowed here!");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateParent(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            user.InsertBy = UserSession.GetUsers.Username;
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN++;
                            user.RoleID = 4;

                            var result = await Task.Run(() => usersDAL.Create(user));
                            return RedirectToAction(nameof(Index));
                        }
                        return View(user);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(user);
                    }
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

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var user = await Task.Run(() => usersDAL.Get((int)id));
                    if (user == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    GetParentsAndTeachers(user);
                    return View(user);
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

        [HttpPost]
        public async Task<ActionResult> Update(int id, Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id != user.UserID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN = ++user.LUN;
                            if (user.RoleID == 1)
                            {
                                user.TeacherID = 0;
                                user.ParentID = 0;
                            }
                            else if (user.RoleID == 2)
                            {
                                user.ParentID = 0;
                            }
                            else if (user.RoleID == 4)
                            {
                                user.TeacherID = 0;
                            }

                            var result = await Task.Run(() => usersDAL.Update(user));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(user);
                        }
                    }
                    return View(user);
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

        public async Task<ActionResult> ChangePassword(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var user = await Task.Run(() => usersDAL.Get((int)id));
                    if (user == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    GetParentsAndTeachers(user);
                    return View(user);
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

        [HttpPost]
        public async Task<ActionResult> ChangePassword(int id, Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id != user.UserID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN = ++user.LUN;
                            user.IsPasswordChanged = true;

                            var result = await Task.Run(() => usersDAL.ChangePassword(user));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(user);
                        }
                    }
                    return View(user);
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

        public async Task<ActionResult> Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var user = await Task.Run(() => usersDAL.Get((int)id));
                    if (user == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(user);
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

        public async Task<ActionResult> Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var user = await Task.Run(() => usersDAL.Get((int)id));
                    if (user == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(user);
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

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id != 3)
                    {
                        await Task.Run(() => usersDAL.Delete(id));
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return Content("You can't delete your own user!");
                    }
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