using System;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SchoolDiarySystem.Models.DataAnnotations;

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
                        users = users.Where(f => f.FirstName.ToLower() == searchString.ToLower() || f.LastName.ToLower() == searchString.ToLower()
                        || f.Username.ToLower() == searchString.ToLower() || f.FullName.ToLower() == searchString.ToLower()
                        || f.Teacher.FirstName.ToLower() == searchString.ToLower() || f.Teacher.LastName.ToLower() == searchString.ToLower()
                        || f.Teacher.FullName.ToLower() == searchString.ToLower() || f.Parent.FullName.ToLower() == searchString.ToLower()
                        || f.Parent.FirstName.ToLower() == searchString.ToLower() || f.Parent.LastName.ToLower() == searchString.ToLower()).ToList();
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
                            user.TeacherID = 0;
                            user.ParentID = 0;
                            user.Password = Validation.CalculateHASH(user.Password);

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
                    GetItemForSelectList();
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
                        GetItemForSelectList();
                        var users = await Task.Run(() => usersDAL.GetAll());
                        var checkUsers = users.Where(u => u.Username.ToLower() == user.Username.ToLower()).ToList();
                        if (checkUsers.Count > 0)
                        {
                            ModelState.AddModelError(string.Empty, "Username you're trying to create, already exists!");
                            return View(user);
                        }
                        else
                        {
                            if (ModelState.IsValid)
                            {
                                user.InsertBy = UserSession.GetUsers.Username;
                                user.LUB = UserSession.GetUsers.Username;
                                user.LUN++;
                                user.RoleID = 2;
                                user.ParentID = 0;
                                user.Password = Validation.CalculateHASH(user.Password);

                                var result = await Task.Run(() => usersDAL.Create(user));
                                return RedirectToAction(nameof(Index));
                            }
                            return View(user);
                        }
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
                    GetItemForSelectList();
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
                        GetItemForSelectList();
                        if (ModelState.IsValid)
                        {
                            user.InsertBy = UserSession.GetUsers.Username;
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN++;
                            user.RoleID = 4;
                            user.TeacherID = 0;
                            user.Password = Validation.CalculateHASH(user.Password);

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

                    GetItemForSelectList();
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

                    GetItemForSelectList();
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

                    GetItemForSelectList();
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

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            user.LUB = UserSession.GetUsers.Username;
                            user.LUN = ++user.LUN;
                            user.IsPasswordChanged = true;
                            user.LastPasswordChangeDate = DateTime.Now;
                            user.Password = Validation.CalculateHASH(user.Password);

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

        private void GetItemForSelectList()
        {
            ViewBag.Parent = parentsDAL.GetAll();
            ViewBag.Teacher = teachersDAL.GetAll();
        }
    }
}