using SchoolDiarySystem.DAL;
using SchoolDiarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolDiarySystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomsDAL roomsDAL = new RoomsDAL();

        // GET: Room
        public ActionResult Index()
        {
            if (UserSession.GetUsers != null)
            {
                var rooms = roomsDAL.GetAll();
                return View(rooms);
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
                return View();
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var students = roomsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Details(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var students = roomsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                return View(students);
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var students = roomsDAL.Get((int)id);
                if (students == null)
                {
                    return RedirectToAction("Index");
                }
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}