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
    public class AbsenceController : Controller
    {
        private readonly AbsencesDAL absencesDAL = new AbsencesDAL();
        private readonly StudentsDAL studentsDAL = new StudentsDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();

        private readonly int teacher = !string.IsNullOrEmpty(UserSession.GetUsers.TeacherID.ToString()) ? UserSession.GetUsers.TeacherID : 0;

        // GET: Absence
        public ActionResult Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    var absences = absencesDAL.GetAllForTeacher(teacher);

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        absences = absences.Where(f => f.AbsenceDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        absences = absences.Where(f => f.Student.FirstName.ToLower() == searchString2.ToLower() || f.Student.LastName.ToLower() == searchString2.ToLower() || f.Student.FullName.ToLower() == searchString2.ToLower()).ToList();
                    }

                    return View(absences);
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    GetItemForSelectList();
                    var absence = new Absences();
                    return View(absence);
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
        public ActionResult Create(Absences absence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            var students = studentsDAL.GetAll();
                            foreach (var item in students)
                            {
                                if (item.StudentID == absence.StudentID)
                                {
                                    absence.ClassID = item.ClassID;
                                }
                            }

                            absence.InsertBy = UserSession.GetUsers.Username;
                            absence.LUB = UserSession.GetUsers.Username;
                            absence.LUN++;

                            var result = absencesDAL.Create(absence);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(absence);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(absence);
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
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var absence = absencesDAL.Get((int)id);
                    return View(absence);
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
        public ActionResult Update(int id, Absences absence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.TEACHER)
                {
                    if (id != absence.AbsenceID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            var students = studentsDAL.GetAll();
                            foreach (var item in students)
                            {
                                if (item.StudentID == absence.StudentID)
                                {
                                    absence.ClassID = item.ClassID;
                                }
                            }

                            absence.LUB = UserSession.GetUsers.Username;
                            absence.LUN = ++absence.LUN;

                            absencesDAL.Update(absence);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(absence);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(absence);
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
                new DataColumn("Date"),
                new DataColumn("Reasoning"),
                new DataColumn("Student"),
                new DataColumn("Class"),
                new DataColumn("Subject"),
            });

            var absences = absencesDAL.GetAll();

            foreach (var item in absences)
            {
                dt.Rows.Add(item.AbsenceDate, item.AbsenceReasoning, item.Student.FullName, item.Class.ClassNo, item.Subject.SubjectTitle);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AbsencesList.xlsx");
                }
            }
        }

        private void GetItemForSelectList()
        {
            List<string> reasons = new List<string>() { "Reasonable", "Unreasonable" };

            ViewBag.Subject = subjectsDAL.GetAllForTeacher(teacher);
            ViewBag.Class = classDAL.GetAllForTeacher(teacher);
            ViewBag.Student = studentsDAL.GetAllForTeacher(teacher);
            ViewBag.AbsenceReasoning = reasons;
        }
    }
}