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
        public ActionResult Index()
        {
            return View();
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
        public async Task<ActionResult> Create(StaffAbsence staffAbsence)
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
                            var staffAbsences = await Task.Run(() => staffAbsenceDAL.GetAll());
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

                                var result = await Task.Run(() => staffAbsenceDAL.Create(staffAbsence));
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

        public async Task<ActionResult> Update(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var staffAbsence = await Task.Run(() => staffAbsenceDAL.Get((int)id));
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
        public async Task<ActionResult> Update(int id, StaffAbsence staffAbsence)
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

                            var result = await Task.Run(() => staffAbsenceDAL.Update(staffAbsence));
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
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 3)
                {
                    await Task.Run(() => staffAbsenceDAL.Delete(id));
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
            var parents = parentsDAL.GetAll();
            var teachers = teachersDAL.GetAll();
            var users = usersDAL.GetAll();

            Users _user = new Users();

            foreach (var parent in parents)
            {
                _user.Parent = parent;
            }

            foreach (var teacher in teachers)
            {
                _user.Teacher = teacher;
            }

            users.Add(_user);

            ViewBag.User = users;
        }
    }
}