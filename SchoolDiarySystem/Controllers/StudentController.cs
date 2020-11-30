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
                var students = await Task.Run(() => studentsDAL.GetAll());

                if (!string.IsNullOrEmpty(searchString))
                {
                    students = students.Where(f => f.FirstName == searchString && f.LastName == searchString).ToList();
                }

                return View(students);
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
                var student = new Students
                {
                    ClassesList = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo"),
                    ParentsList = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName"),
                };
                GetGenders();
                return View(student);
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
                try
                {
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
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
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

                student.ClassesList = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo", student.ParentID);
                student.ParentsList = new SelectList(parentsDAL.GetAll(), "ParentID", "FullName", student.ClassID);

                GetGenders();
                return View(student);
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
                if (id != student.StudentID)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                //if (ModelState.IsValid)
                //{
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
                //}
                //return View(_class);
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
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (UserSession.GetUsers != null)
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
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                await Task.Run(() => studentsDAL.Delete(id));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetGenders()
        {
            List<string> genders = new List<string>() { "Male", "Female" };
            ViewBag.GenderID = new SelectList(genders, "GenderID");
        }
    }
}