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
    public class StaffAbsenceController : Controller
    {
        private readonly StaffAbsenceDAL staffAbsenceDAL = new StaffAbsenceDAL();
        private readonly UsersDAL usersDAL = new UsersDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();
        private readonly ParentsDAL parentsDAL = new ParentsDAL();

        // GET: StaffAbsence
        public ActionResult Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    var absences = staffAbsenceDAL.GetAll();
                    var teachers = teachersDAL.GetAll();

                    foreach (var teacher in teachers)
                    {
                        foreach (var us in absences.ToList())
                        {
                            if (us.User.Role.RoleName == "Teacher")
                            {
                                us.User.FirstName = teacher.FirstName;
                                us.User.LastName = teacher.LastName;
                            }
                            //else if (us.User.Role.RoleName == "Parent")
                            //{
                            //    absences.Remove(us);
                            //}
                            //else if (us.User.Role.RoleName == "Director")
                            //{
                            //    absences.Remove(us);
                            //}
                        }
                    }

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        absences = absences.Where(f => f.AbsenceDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        absences = absences.Where(f => f.User.Teacher.FirstName.ToLower() == searchString2.ToLower()
                        || f.User.Teacher.LastName.ToLower() == searchString2.ToLower() || f.User.Teacher.FullName.ToLower() == searchString2.ToLower()
                        || f.User.FirstName.ToLower() == searchString2.ToLower() || f.User.LastName.ToLower() == searchString2.ToLower()
                        || f.User.FullName.ToLower() == searchString2.ToLower()).ToList();
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
                if (UserSession.GetUsers.RoleID == 3)
                {
                    GetItemForSelectList();
                    var staffAbsence = new StaffAbsence();
                    return View(staffAbsence);
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
        public ActionResult Create(StaffAbsence staffAbsence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    try
                    {
                        GetItemForSelectList();

                        if (ModelState.IsValid)
                        {
                            var staffAbsences = staffAbsenceDAL.GetAll();
                            var checkStaffAbsences = staffAbsences.Where(t => t.UserID == staffAbsence.UserID && t.AbsenceDate == staffAbsence.AbsenceDate).ToList();
                            if (checkStaffAbsences.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Absence you're trying to create, already exists!");
                                return View(staffAbsence);
                            }
                            else
                            {
                                staffAbsence.InsertBy = UserSession.GetUsers.Username;
                                staffAbsence.LUB = UserSession.GetUsers.Username;
                                staffAbsence.LUN++;

                                var result = staffAbsenceDAL.Create(staffAbsence);
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(staffAbsence);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating schedule.");
                        return View(staffAbsence);
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
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var staffAbsence = staffAbsenceDAL.Get((int)id);
                    if (staffAbsence == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    GetItemForSelectList();
                    return View(staffAbsence);
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
        public ActionResult Update(int id, StaffAbsence staffAbsence)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id != staffAbsence.StaffAbsenceID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            staffAbsence.LUB = UserSession.GetUsers.Username;
                            staffAbsence.LUN = ++staffAbsence.LUN;

                            var result = staffAbsenceDAL.Update(staffAbsence);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(staffAbsence);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(staffAbsence);
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
                if (UserSession.GetUsers.RoleID == 3)
                {
                    staffAbsenceDAL.Delete(id);
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
            var teachers = teachersDAL.GetAll();
            var users = usersDAL.GetAll();

            foreach (var teacher in teachers)
            {
                foreach (var us in users.ToList())
                {
                    if (us.Role.RoleName == "Teacher")
                    {
                        us.FirstName = teacher.FirstName;
                        us.LastName = teacher.LastName;
                    }
                    else if (us.Role.RoleName == "Parent")
                    {
                        users.Remove(us);
                    }
                    else if (us.Role.RoleName == "Director")
                    {
                        users.Remove(us);
                    }
                }
            }

            List<string> reasons = new List<string>() { "Reasonable", "Unreasonable" };

            ViewBag.User = users;
            ViewBag.AbsenceReasoning = reasons;
        }
    }
}