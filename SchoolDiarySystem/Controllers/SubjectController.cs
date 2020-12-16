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
    public class SubjectController : Controller
    {
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Subject
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var subjects = await Task.Run(() => subjectsDAL.GetAll());

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
                if (UserSession.GetUsers.RoleID == 1)
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
        public async Task<ActionResult> Create(Subjects subject)
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
                            var subjects = await Task.Run(() => subjectsDAL.GetAll());
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

                                var result = await Task.Run(() => subjectsDAL.Create(subject));
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
                    var subject = await Task.Run(() => subjectsDAL.Get((int)id));
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
        public async Task<ActionResult> Update(int id, Subjects subject)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
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
                            var result = await Task.Run(() => subjectsDAL.Update(subject));
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

        public async Task<ActionResult> Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var subject = await Task.Run(() => subjectsDAL.Get((int)id));
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
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var subject = await Task.Run(() => subjectsDAL.Delete(id));
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
            ViewBag.Teacher = teachersDAL.GetAll();
        }
    }
}