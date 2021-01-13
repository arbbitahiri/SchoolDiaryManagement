using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.IO;
using System.Data;

namespace SchoolDiarySystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Teacher
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var teachers = teachersDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        teachers = teachers.Where(f => f.FirstName.ToLower() == searchString.ToLower() || f.LastName.ToLower() == searchString.ToLower()
                        || f.FullName.ToLower() == searchString.ToLower()).ToList();
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
                    var teacher = new Teachers();
                    GetItemForSelectList();
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
        public ActionResult Create(Teachers teacher)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {
                        GetItemForSelectList();
                        if (ModelState.IsValid)
                        {
                            teacher.InsertBy = UserSession.GetUsers.Username;
                            teacher.LUB = UserSession.GetUsers.Username;
                            teacher.LUN++;

                            var result = teachersDAL.Create(teacher);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Update(int? id)
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
                    var teacher = teachersDAL.Get((int)id);
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
        public ActionResult Update(int id, Teachers teacher)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != teacher.TeacherID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            teacher.LUB = UserSession.GetUsers.Username;
                            teacher.LUN = ++teacher.LUN;
                            var result = teachersDAL.Update(teacher);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(teacher);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
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

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = teachersDAL.Get((int)id);
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

        public ActionResult Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var teacher = teachersDAL.Get((int)id);
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
        public ActionResult Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var result = teachersDAL.Delete(id);
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
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Name"),
                new DataColumn("Gender"),
                new DataColumn("City"),
                new DataColumn("Birthday"),
                new DataColumn("E-mail"),
                new DataColumn("Phone No."),
            });

            var teachers = teachersDAL.GetAll();

            foreach (var item in teachers)
            {
                dt.Rows.Add(item.FullName, item.Gender, item.City, item.DayofBirth, item.Email, item.PhoneNo);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TeachersList.xlsx");
                }
            }
        }

        private void GetItemForSelectList()
        {
            IEnumerable<string> genders = new List<string>() { "Male", "Female" };
            ViewBag.Gender = genders;
        }
    }
}