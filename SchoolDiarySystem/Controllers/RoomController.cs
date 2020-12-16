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
    public class RoomController : Controller
    {
        private readonly RoomsDAL roomsDAL = new RoomsDAL();

        // GET: Room
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    var rooms = await Task.Run(() => roomsDAL.GetAll());

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        rooms = rooms.Where(f => f.RoomType.ToLower() == searchString.ToLower()).ToList();
                    }

                    return View(rooms);
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
        public async Task<ActionResult> Create(Rooms room)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            var rooms = await Task.Run(() => roomsDAL.GetAll());
                            var checkRooms = rooms.Where(r => r.RoomNo == room.RoomNo).ToList();
                            if (checkRooms.Count > 0)
                            {
                                ModelState.AddModelError(string.Empty, "Room you're trying to create, already exists!");
                                return View(room);
                            }
                            else
                            {
                                room.InsertBy = UserSession.GetUsers.Username;
                                room.LUB = UserSession.GetUsers.Username;
                                room.LUN++;

                                var result = await Task.Run(() => roomsDAL.Create(room));
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid attempt");
                        }
                        return View(room);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(room);
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

                    var room = await Task.Run(() => roomsDAL.Get((int)id));
                    if (room == null)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    return View(room);
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
        public async Task<ActionResult> Update(int id, Rooms room)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 1)
                {
                    if (id != room.RoomID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            room.LUB = UserSession.GetUsers.Username;
                            room.LUN = ++room.LUN;

                            var result = await Task.Run(() => roomsDAL.Update(room));
                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                            return View(room);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid attempt");
                    }
                    return View(room);
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
                    var room = await Task.Run(() => roomsDAL.Get((int)id));
                    return View(room);
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
            if (UserSession.GetUsers.RoleID == 1)
            {
                if (UserSession.GetUsers != null)
                {
                    await Task.Run(() => roomsDAL.Delete(id));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return Content("You're not allowed to view this page!");
            }
        }
    }
}