﻿using ClosedXML.Excel;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class SubjectController : Controller
    {
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Subject
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    var subjects = subjectsDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        subjects = subjects.Where(f => f.SubjectTitle.ToLower() == searchString.ToLower()).ToList();
                    }

                    return View(subjects);
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
                    var subject = new Subjects();
                    GetItemForSelectList();
                    return View(subject);
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
        public ActionResult Create(Subjects subject)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    try
                    {
                        GetItemForSelectList();
                        if (ModelState.IsValid)
                        {
                            var subjects = subjectsDAL.GetAll();
                            var checkSubjects = subjects.Where(s => s.SubjectTitle == subject.SubjectTitle && s.TeacherID == subject.TeacherID).ToList();
                            if (checkSubjects.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Subject you're trying to create, already exists!");
                                return View(subject);
                            }
                            else
                            {
                                subject.InsertBy = UserSession.GetUsers.Username;
                                subject.LUB = UserSession.GetUsers.Username;
                                subject.LUN++;

                                var result = subjectsDAL.Create(subject);
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(subject);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(subject);
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
                    var subject = subjectsDAL.Get((int)id);
                    if (subject == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(subject);
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
        public ActionResult Update(int id, Subjects subject)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    if (id != subject.SubjectID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            subject.LUB = UserSession.GetUsers.Username;
                            subject.LUN = ++subject.LUN;
                            var result = subjectsDAL.Update(subject);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(subject);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(subject);
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

                    var subject = subjectsDAL.Get((int)id);
                    return View(subject);
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
                    var subject = subjectsDAL.Delete(id);
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
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Subject"),
                new DataColumn("Book"),
                new DataColumn("Book Author"),
                new DataColumn("Teacher"),
            });

            var subjects = subjectsDAL.GetAll();

            foreach (var item in subjects)
            {
                dt.Rows.Add(item.SubjectTitle, item.Book, item.BookAuthor, item.Teacher.FullName);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SubjectsList.xlsx");
                }
            }
        }

        private void GetItemForSelectList()
        {
            ViewBag.Teacher = teachersDAL.GetAll();
        }
    }
}