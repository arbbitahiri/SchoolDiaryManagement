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
    public class ScheduleController : Controller
    {
        private readonly ClassSchedulesDAL schedulesDAL = new ClassSchedulesDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();

        // GET: Schedule
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    var schedules = schedulesDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        if (searchString.All(char.IsDigit))
                        {
                            schedules = schedules.Where(f => f.Class.ClassNo == int.Parse(searchString)).ToList();
                        }
                        else
                        {
                            schedules = schedules.Where(f => f.Subject.SubjectTitle.ToLower() == searchString.ToLower()
                            || f.Day.ToLower() == searchString.ToLower()).ToList();
                        }
                    }

                    return View(schedules);
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
                    GetItemForSelectList();
                    var schedule = new ClassSchedules();
                    return View(schedule);
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
        public ActionResult Create(ClassSchedules schedule)
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
                            var schedules = schedulesDAL.GetAll();
                            var checkSchedules = schedules.Where(t => t.ClassID == schedule.ClassID && t.Day == schedule.Day && t.Time == schedule.Time).ToList();

                            if (checkSchedules.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Schedule you're trying to create, already exists!");
                                return View(schedule);
                            }
                            else
                            {
                                schedule.InsertBy = UserSession.GetUsers.Username;
                                schedule.LUB = UserSession.GetUsers.Username;
                                schedule.LUN++;

                                var result = schedulesDAL.Create(schedule);
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(schedule);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating schedule.");
                        return View(schedule);
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

                    var schedule = schedulesDAL.Get((int)id);
                    if (schedule == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    GetItemForSelectList();
                    return View(schedule);
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
        public ActionResult Update(int id, ClassSchedules schedule)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    if (id != schedule.ScheduleID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList();
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            schedule.LUB = UserSession.GetUsers.Username;
                            schedule.LUN = ++schedule.LUN;

                            var result = schedulesDAL.Update(schedule);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(schedule);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(schedule);
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

                    var schedule = schedulesDAL.Get((int)id);
                    return View(schedule);
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
                    schedulesDAL.Delete(id);
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
            List<string> days = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Times = times;
            ViewBag.Days = days;
            ViewBag.Class = classDAL.GetAll();
            ViewBag.Subject = subjectsDAL.GetAll();
        }

        [HttpPost]
        public ActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] {
                new DataColumn("Subject"),
                new DataColumn("Class"),
                new DataColumn("Day"),
                new DataColumn("Time"),
            });

            var schedules = schedulesDAL.GetAll();

            foreach (var item in schedules)
            {
                dt.Rows.Add(item.Subject.SubjectTitle, item.Class.ClassNo, item.Day, item.Time);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SchedulesList.xlsx");
                }
            }
        }
    }
}