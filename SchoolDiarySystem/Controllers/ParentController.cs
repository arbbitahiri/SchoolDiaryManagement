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
    public class ParentController : Controller
    {
        private readonly ParentsDAL parentsDAL = new ParentsDAL();

        // GET: Parent
        public async Task<ActionResult> Index(string searchString)
        {
            if (UserSession.GetUsers != null)
            {
                var parents = await Task.Run(() => parentsDAL.GetAll());

                if (!string.IsNullOrEmpty(searchString))
                {
                    parents = parents.Where(f => f.FirstName == searchString || f.LastName == searchString).ToList();
                }

                return View(parents);
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


        [HttpPost]
        public async Task<ActionResult> Create(Parents parent)
        {
            if (UserSession.GetUsers != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        parent.InsertBy = UserSession.GetUsers.Username;
                        parent.LUB = UserSession.GetUsers.Username;
                        parent.LUN++;

                        var result = await Task.Run(() => parentsDAL.Create(parent));
                        return RedirectToAction(nameof(Index));
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

                var parent = await Task.Run(() => parentsDAL.Get((int)id));
                if (parent == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(parent);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, Parents parent)
        {
            if (UserSession.GetUsers != null)
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

                        var result = await Task.Run(() => parentsDAL.Update(parent));
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                        return View(parent);
                    }
                }
                return View(parent);
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

                var parent = await Task.Run(() => parentsDAL.Get((int)id));
                if (parent == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(parent);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> DeleteAsync(int? id)
        {
            if (UserSession.GetUsers != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var parent = await Task.Run(() => parentsDAL.Get((int)id));
                if (parent == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(parent);
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
                await Task.Run(() => parentsDAL.Delete(id));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}