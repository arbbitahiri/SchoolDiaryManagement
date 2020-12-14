using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;

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
        public async Task<ActionResult> Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var absences = await Task.Run(() => absencesDAL.GetAllForTeacher(teacher));

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
                if (UserSession.GetUsers.RoleID == 2)
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
        public async Task<ActionResult> Create(Absences absence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            absence.InsertBy = UserSession.GetUsers.Username;
                            absence.LUB = UserSession.GetUsers.Username;
                            absence.LUN++;

                            var result = await Task.Run(() => absencesDAL.Create(absence));
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

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    var absence = await Task.Run(() => absencesDAL.Get((int)id));
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
        public async Task<ActionResult> Update(int id, Absences absence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
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
                            absence.LUB = UserSession.GetUsers.Username;
                            absence.LUN = ++absence.LUN;

                            var result = await Task.Run(() => absencesDAL.Update(absence));
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