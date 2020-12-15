﻿using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ClassSchedulesDAL schedulesDAL = new ClassSchedulesDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();

        // GET: Schedule
        public async Task<ActionResult> Index(string searchString, string searchString2)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var schedules = await Task.Run(() => schedulesDAL.GetAll());

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        schedules = schedules.Where(f => f.Class.ClassNo == int.Parse(searchString)).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        schedules = schedules.Where(f => f.Subject.SubjectTitle.ToLower() == searchString2.ToLower()).ToList();
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
                if (UserSession.GetUsers.RoleID == 1)
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
        public async Task<ActionResult> Create(ClassSchedules schedule)
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
                            var schedules = await Task.Run(() => schedulesDAL.GetAll());
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

                                var result = await Task.Run(() => schedulesDAL.Create(schedule));
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

                    var schedule = await Task.Run(() => schedulesDAL.Get((int)id));
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
        public async Task<ActionResult> Update(int id, ClassSchedules schedule)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
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

                            var result = await Task.Run(() => schedulesDAL.Update(schedule));
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

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    await Task.Run(() => schedulesDAL.Delete(id));
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
    }
}