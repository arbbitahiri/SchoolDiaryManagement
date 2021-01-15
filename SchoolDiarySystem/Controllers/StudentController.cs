using ClosedXML.Excel;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();

        // GET: Student
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    var students = studentsDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        students = students.Where(f => f.FirstName.ToLower() == searchString.ToLower()
                        || f.LastName.ToLower() == searchString.ToLower() || f.FullName.ToLower() == searchString.ToLower()).ToList();
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
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
        public ActionResult Create(Students student)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
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

                            var result = studentsDAL.Create(student);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var student = studentsDAL.Get((int)id);
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
        public ActionResult Update(int id, Students student)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
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

                            var result = studentsDAL.Update(student);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(student);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var student = studentsDAL.Get((int)id);
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
        public ActionResult Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    studentsDAL.Delete(id);
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

        [HttpPost]
        public ActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] {
                new DataColumn("Name"),
                new DataColumn("Gender"),
                new DataColumn("Class"),
                new DataColumn("Parent"),
                new DataColumn("Birthday"),
            });

            var students = studentsDAL.GetAll();

            foreach (var item in students)
            {
                dt.Rows.Add(item.FullName, item.Gender, item.Class.ClassNo, item.Parent.FullName, item.DayofBirth);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentsList.xlsx");
                }
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