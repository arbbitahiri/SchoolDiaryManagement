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
    public class TopicController : Controller
    {
        private readonly TopicsDAL topicsDAL = new TopicsDAL();
        private readonly ClassDAL classDAL = new ClassDAL();
        private readonly SubjectsDAL subjectsDAL = new SubjectsDAL();

        // GET: Topic
        public async Task<ActionResult> Index(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                var topics = await Task.Run(() => topicsDAL.GetAll());

                if (!string.IsNullOrEmpty(searchString))
                {
                    topics = topics.Where(f => f.TopicDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                }

                if (!string.IsNullOrEmpty(searchString2))
                {
                    topics = topics.Where(f => f.Subject.SubjectTitle == searchString2).ToList();
                }

                if (!string.IsNullOrEmpty(searchString3))
                {
                    topics = topics.Where(f => f.Time == int.Parse(searchString2)).ToList();
                }

                return View(topics);
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
                var topic = new Topics()
                {
                    ClassesList = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo"),
                    SubjectsList = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle")
                };
                GetTimes();
                return View(topic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Topics topic)
        {
            if (UserSession.GetUsers != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        topic.InsertBy = UserSession.GetUsers.Username;
                        topic.LUB = UserSession.GetUsers.Username;
                        topic.LUN++;

                        var result = await Task.Run(() => topicsDAL.Create(topic));
                        return RedirectToAction(nameof(Index));
                    }
                    return View(topic);
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                    return View(topic);
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

                var topic = await Task.Run(() => topicsDAL.Get((int)id));
                if (topic == null)
                {
                    return RedirectToAction("Index");
                }
                topic.SubjectsList = new SelectList(subjectsDAL.GetAll(), "SubjectID", "SubjectTitle", topic.SubjectID);
                topic.ClassesList = new SelectList(classDAL.GetAll(), "ClassID", "ClassNo", topic.ClassID);
                GetTimes();

                return View(topic);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, Topics topic)
        {
            if (UserSession.GetUsers != null)
            {
                if (id != topic.TopicID)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        topic.LUB = UserSession.GetUsers.Username;
                        topic.LUN = ++topic.LUN;

                        var result = await Task.Run(() => topicsDAL.Update(topic));
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while updating class.");
                        return View(topic);
                    }
                }
                return View(topic);
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

                var topic = await Task.Run(() => topicsDAL.Get((int)id));
                if (topic == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(topic);
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
                if (id == null)
                {
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                }

                var topic = await Task.Run(() => topicsDAL.Get((int)id));
                if (topic == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(topic);
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
                await Task.Run(() => topicsDAL.Delete(id));
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private void GetTimes()
        {
            List<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Time = new SelectList(times, "Time");
        }
    }
}