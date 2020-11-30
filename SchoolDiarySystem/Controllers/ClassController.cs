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
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var classes = await Task.Run(() => classDAL.GetAll());

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
                    var _class = new Class
                    {
                        TeacherList = new SelectList(teachersDAL.GetAll(), "TeacherID", "FullName"),
                        RoomList = new SelectList(roomsDAL.GetAll(), "RoomID", "RoomType")
                    };
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
        public async Task<ActionResult> Create(Class _class)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            _class.InsertBy = UserSession.GetUsers.Username;
                            _class.LUB = UserSession.GetUsers.Username;
                            _class.LUN++;

                            var result = await Task.Run(() => classDAL.Create(_class));
                            return RedirectToAction(nameof(Index));
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

                    var _class = await Task.Run(() => classDAL.Get((int)id));
                    if (_class == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    _class.TeacherList = new SelectList(teachersDAL.GetAll(), "TeacherID", "FullName", _class.TeacherID);
                    _class.RoomList = new SelectList(roomsDAL.GetAll(), "RoomID", "RoomType", _class.RoomID);

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
        public async Task<ActionResult> Update(int id, Class _class)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != _class.ClassID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    //if (ModelState.IsValid)
                    //{
                    try
                    {
                        _class.LUB = UserSession.GetUsers.Username;
                        _class.LUN = ++_class.LUN;

                        var result = await Task.Run(() => classDAL.Update(_class));
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                        return View(_class);
                    }
                    //}
                    //return View(_class);
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

        public async Task<ActionResult> Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var _class = await Task.Run(() => classDAL.Get((int)id));
                    if (_class == null)
                    {
                        return RedirectToAction(nameof(Index));
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

        public async Task<ActionResult> Delete(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var _class = await Task.Run(() => classDAL.Get((int)id));
                    if (_class == null)
                    {
                        return RedirectToAction(nameof(Index));
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

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    await Task.Run(() => classDAL.Delete(id));
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