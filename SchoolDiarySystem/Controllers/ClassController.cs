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
    public class ClassController : Controller
    {
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly RoomsDAL roomsDAL = new RoomsDAL();
        private readonly TeachersDAL teachersDAL = new TeachersDAL();

        // GET: Class
        public ActionResult Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var classes = classDAL.GetAll();

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        classes = classes.Where(f => f.ClassNo == int.Parse(searchString)).ToList();
                    }

                    return View(classes);
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
                    var _class = new Class();
                    ViewBag.Teacher = teachersDAL.GetAll();
                    ViewBag.Room = roomsDAL.GetAll();
                    return View(_class);
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
        public ActionResult Create(Class _class)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {
                        ViewBag.Teacher = teachersDAL.GetAll();
                        ViewBag.Room = roomsDAL.GetAll();

                        if (ModelState.IsValid)
                        {
                            var classes = classDAL.GetAll();
                            var checkClasses = classes.Where(c => c.ClassNo == _class.ClassNo).ToList();
                            if (checkClasses.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Class you're trying to create, already exists!");
                                return View(_class);
                            }
                            else
                            {
                                _class.InsertBy = UserSession.GetUsers.Username;
                                _class.LUB = UserSession.GetUsers.Username;
                                _class.LUN++;

                                var result = classDAL.Create(_class);
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(_class);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(_class);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var _class = classDAL.Get((int)id);
                    if (_class == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    ViewBag.Teacher = teachersDAL.GetAll();
                    ViewBag.Room = roomsDAL.GetAll();

                    return View(_class);
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
        public ActionResult Update(int id, Class _class)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != _class.ClassID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    ViewBag.Teacher = teachersDAL.GetAll();
                    ViewBag.Room = roomsDAL.GetAll();
                    //var errors = ModelState.Values.SelectMany(m => m.Errors);
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _class.LUB = UserSession.GetUsers.Username;
                            _class.LUN = ++_class.LUN;

                            var result = classDAL.Update(_class);
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(_class);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(_class);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var _class = classDAL.Get((int)id);
                    return View(_class);
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
                if (UserSession.GetUsers.RoleID == 1)
                {
                    classDAL.Delete(id);
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
    }
}