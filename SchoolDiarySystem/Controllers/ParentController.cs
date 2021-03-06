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
    public class ParentController : Controller
    {
        private readonly ParentsDAL parentsDAL = new ParentsDAL();

        // GET: Parent
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    var parents = parentsDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        parents = parents.Where(f => f.FirstName.ToLower() == searchString.ToLower()
                        || f.LastName.ToLower() == searchString.ToLower() || f.FullName.ToLower() == searchString.ToLower()).ToList();
                    }

                    return View(parents);
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
                    return View();
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
        public ActionResult Create(Parents parent)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            parent.InsertBy = UserSession.GetUsers.Username;
                            parent.LUB = UserSession.GetUsers.Username;
                            parent.LUN++;

                            var result = parentsDAL.Create(parent);
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(parent);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(parent);
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

                    var parent = parentsDAL.Get((int)id);
                    if (parent == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(parent);
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
        public ActionResult Update(int id, Parents parent)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.Role.RoleName == UserRoles.ADMIN)
                {
                    if (id != parent.ParentID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            parent.LUB = UserSession.GetUsers.Username;
                            parent.LUN = ++parent.LUN;

                            var result = parentsDAL.Update(parent);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(parent);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(parent);
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

                    var result = parentsDAL.Get((int)id);
                    return View(result);
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
                    parentsDAL.Delete(id);
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
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Parent"),
                new DataColumn("City"),
            });

            var parents = parentsDAL.GetAll();

            foreach (var item in parents)
            {
                dt.Rows.Add(item.FullName, item.City);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ParentsList.xlsx");
                }
            }
        }
    }
}