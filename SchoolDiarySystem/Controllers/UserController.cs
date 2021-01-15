using ClosedXML.Excel;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using SchoolDiarySystem.Models.DataAnnotations;
using System;
using System.Data;
using System.IO;
using System.Linq;
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
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    var users = usersDAL.GetAll();

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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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
        public ActionResult CreateAdmin(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var users = usersDAL.GetAll();

                            var checkUsers = users.Where(u => u.Username.ToLower() == user.Username.ToLower()).ToList();
                            if (checkUsers.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Username you're trying to create, already exists!");
                                return View(user);
                            }
                            else
                            {
                                var checkAdmin = users.Where(u => u.FirstName == user.FirstName && u.LastName == user.LastName).ToList();
                                if (checkAdmin.Count > 0)
                                {
                                    ModelState.AddModelError(string.Empty, $"{user.FullName} already has an account!");
                                    return View(user);
                                }
                                else
                                {
                                    user.InsertBy = UserSession.GetUsers.Username;
                                    user.LUB = UserSession.GetUsers.Username;
                                    user.LUN++;
                                    user.RoleID = 1;
                                    user.TeacherID = 0;
                                    user.ParentID = 0;
                                    user.Password = Validation.CalculateHASH(user.Password);

                                    var result = usersDAL.Create(user);
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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
        public ActionResult CreateTeacher(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    try
                    {
                        GetItemForSelectList();

                        if (ModelState.IsValid)
                        {
                            var users = usersDAL.GetAll();

                            var checkUsers = users.Where(u => u.Username.ToLower() == user.Username.ToLower()).ToList();
                            if (checkUsers.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Username you're trying to create, already exists!");
                                return View(user);
                            }
                            else
                            {
                                var checkTeacher = users.Where(u => u.TeacherID == user.TeacherID).ToList();
                                if (checkTeacher.Count > 0)
                                {
                                    ModelState.AddModelError(string.Empty, $"{user.Teacher.FullName} already has an account!");
                                    return View(user);
                                }
                                else
                                {
                                    user.InsertBy = UserSession.GetUsers.Username;
                                    user.LUB = UserSession.GetUsers.Username;
                                    user.LUN++;
                                    user.RoleID = 2;
                                    user.ParentID = 0;
                                    user.Password = Validation.CalculateHASH(user.Password);

                                    var result = usersDAL.Create(user);
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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
        public ActionResult CreateParent(Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    try
                    {
                        GetItemForSelectList();

                        if (ModelState.IsValid)
                        {
                            var users = usersDAL.GetAll();

                        var checkUsers = users.Where(u => u.Username.ToLower() == user.Username.ToLower()).ToList();
                            if (checkUsers.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Username you're trying to create, already exists!");
                                return View(user);
                            }
                            else
                            {
                                var checkTeacher = users.Where(u => u.ParentID == user.ParentID).ToList();
                                if (checkTeacher.Count > 0)
                                {
                                    ModelState.AddModelError(string.Empty, $"{user.Parent.FullName} already has an account!");
                                    return View(user);
                                }
                                else
                                {
                                    user.InsertBy = UserSession.GetUsers.Username;
                                    user.LUB = UserSession.GetUsers.Username;
                                    user.LUN++;
                                    user.RoleID = 4;
                                    user.TeacherID = 0;
                                    user.Password = Validation.CalculateHASH(user.Password);

                                    var result = usersDAL.Create(user);
                                    return RedirectToAction(nameof(Index));
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var user = usersDAL.Get((int)id);
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
        public ActionResult Update(int id, Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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

                            var result = usersDAL.Update(user);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult ChangePassword(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var user = usersDAL.Get((int)id);
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
        public ActionResult ChangePassword(int id, Users user)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
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

                            var result = usersDAL.ChangePassword(user);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(user);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var user = usersDAL.Get((int)id);
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
        public ActionResult Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.DIRECTOR)
                {
                    if (id != 3)
                    {
                        usersDAL.Delete(id);
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

        [HttpPost]
        public ActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Name"),
                new DataColumn("Username"),
                new DataColumn("Role"),
                new DataColumn("Expire Date"),
                new DataColumn("Last Login Date"),
                new DataColumn("Last Login Time"),
            });

            var users = usersDAL.GetAll();

            foreach (var item in users)
            {
                dt.Rows.Add(item.FullName, item.Username, item.Role.RoleName, item.ExpiresDate, item.LastLoginDate, item.LastLoginTime);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "UsersList.xlsx");
                }
            }
        }

        private void GetItemForSelectList()
        {
            ViewBag.Parent = parentsDAL.GetAll();
            ViewBag.Teacher = teachersDAL.GetAll();
        }
    }
}