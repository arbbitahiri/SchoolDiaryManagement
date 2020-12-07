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
    public class StudentController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();

        // GET: Student
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var students = await Task.Run(() => studentsDAL.GetAll());

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        students = students.Where(f => f.FirstName == searchString || f.LastName == searchString || f.FullName == searchString).ToList();
                    }

                    return View(students);
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
                    var student = new Students();
                    GetItemForSelectList();
                    return View(student);
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
        public async Task<ActionResult> Create(Students student)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {
                        GetItemForSelectList();
                        var errors = ModelState.Values.SelectMany(s => s.Errors);
                        if (ModelState.IsValid)
                        {
                            student.InsertBy = UserSession.GetUsers.Username;
                            student.LUB = UserSession.GetUsers.Username;
                            student.LUN++;

                            var result = await Task.Run(() => studentsDAL.Create(student));
                            return RedirectToAction(nameof(Index));
                        }
                        return View(student);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(student);
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

                    GetItemForSelectList();
                    var student = await Task.Run(() => studentsDAL.Get((int)id));
                    if (student == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return View(student);
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
        public async Task<ActionResult> Update(int id, Students student)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != student.StudentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var errors = ModelState.Values.SelectMany(s => s.Errors);
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            student.LUB = UserSession.GetUsers.Username;
                            student.LUN = ++student.LUN;

                            var result = await Task.Run(() => studentsDAL.Update(student));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(student);
                        }
                    }
                    return View(student);
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

                    var student = await Task.Run(() => studentsDAL.Get((int)id));
                    if (student == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(student);
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

                    var student = await Task.Run(() => studentsDAL.Get((int)id));
                    if (student == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(student);
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
                    await Task.Run(() => studentsDAL.Delete(id));
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

        private void GetItemForSelectList()
        {
            IEnumerable<string> genders = new List<string>() { "Male", "Female" };

            ViewBag.Class = classDAL.GetAll();
            ViewBag.Parent = parentsDAL.GetAll();
            ViewBag.Gender = genders;
        }
    }
}