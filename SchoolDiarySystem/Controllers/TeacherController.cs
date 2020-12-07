using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Teacher
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var teachers = await Task.Run(() => teachersDAL.GetAll());

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        teachers = teachers.Where(f => f.FirstName == searchString || f.LastName == searchString).ToList();
                    }

                    return View(teachers);
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

        public ActionResult Create()
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    IEnumerable<string> genders = new List<string>() { "Male", "Female" };
                    var teacher = new Teachers()
                    {
                        GenderEnumeration = new SelectList(genders)
                    };
                    return View(teacher);
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
        public async Task<ActionResult> Create(Teachers teacher)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {

                        if (ModelState.IsValid)
                        {
                            teacher.InsertBy = UserSession.GetUsers.Username;
                            teacher.LUB = UserSession.GetUsers.Username;
                            teacher.LUN++;

                            var result = await Task.Run(() => teachersDAL.Create(teacher));
                            return RedirectToAction(nameof(Index));
                        }
                        return View(teacher);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating teacher.");
                        return View(teacher);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = await Task.Run(() => teachersDAL.Get((int)id));
                    if (teacher == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    IEnumerable<string> genders = new List<string>() { "Male", "Female" };
                    teacher.GenderEnumeration = new SelectList(genders);
                    return View(teacher);
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
        public async Task<ActionResult> Update(int id, Teachers teacher)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != teacher.TeacherID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            teacher.LUB = UserSession.GetUsers.Username;
                            teacher.LUN = ++teacher.LUN;
                            var result = await Task.Run(() => teachersDAL.Update(teacher));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(teacher);
                        }
                    }
                    return View(teacher);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = await Task.Run(() => teachersDAL.Get((int)id));
                    if (teacher == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(teacher);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = await Task.Run(() => teachersDAL.Get((int)id));
                    if (teacher == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(teacher);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var result = await Task.Run(() => teachersDAL.Delete(id));
                    return RedirectToAction(nameof(Index));
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