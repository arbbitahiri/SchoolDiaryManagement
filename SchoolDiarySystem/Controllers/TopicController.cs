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
        private readonly int teacher = !string.IsNullOrEmpty(UserSession.GetUsers.TeacherID.ToString()) ? UserSession.GetUsers.TeacherID : 0;

        // GET: Topic
        public async Task<ActionResult> Index(string searchString, string searchString2, string searchString3)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var topics = await Task.Run(() => topicsDAL.GetAllForTeacher(teacher));

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        topics = topics.Where(f => f.TopicDate.Date == Convert.ToDateTime(searchString).Date).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString2))
                    {
                        topics = topics.Where(f => f.Subject.SubjectTitle.ToLower() == searchString2.ToLower()).ToList();
                    }

                    if (!string.IsNullOrEmpty(searchString3))
                    {
                        topics = topics.Where(f => f.Time == int.Parse(searchString3)).ToList();
                    }

                    return View(topics);
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
                if (UserSession.GetUsers.RoleID == 2)
                {
                    var topic = new Topics();
                    GetItemForSelectList(teacher);
                    return View(topic);
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
        public async Task<ActionResult> Create(Topics topic)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    try
                    {
                        GetItemForSelectList(teacher);
                        var topics = await Task.Run(() => topicsDAL.GetAll());
                        var checkTopics = topics.Where(t => t.ClassID == topic.ClassID && t.SubjectID == topic.SubjectID
                            && t.Time == topic.Time && t.TopicDate == topic.TopicDate).ToList();
                        if (checkTopics.Count > 0)
                        {
                            ModelState.AddModelError(string.Empty, "Topic you're trying to create, already exists!");
                            return View(topic);
                        }
                        else
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
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "An error occured while creating class.");
                        return View(topic);
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
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList(teacher);
                    var topic = await Task.Run(() => topicsDAL.Get((int)id));
                    if (topic == null)
                    {
                        return RedirectToAction("Index");
                    }
                    return View(topic);
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
        public async Task<ActionResult> Update(int id, Topics topic)
        {
            if (UserSession.GetUsers != null)
            {
                if (UserSession.GetUsers.RoleID == 2)
                {
                    if (id != topic.TopicID)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    GetItemForSelectList(teacher);
                    var errors = ModelState.Values.SelectMany(s => s.Errors);
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
                if (UserSession.GetUsers.RoleID == 2 || UserSession.GetUsers.RoleID == 4)
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
                if (UserSession.GetUsers.RoleID == 2)
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
                if (UserSession.GetUsers.RoleID == 2)
                {
                    await Task.Run(() => topicsDAL.Delete(id));
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

        private void GetItemForSelectList(int teacherID)
        {
            IEnumerable<int> times = new List<int>() { 1, 2, 3, 4, 5, 6 };

            ViewBag.Class = classDAL.GetAllForTeacher(teacherID);
            ViewBag.Subject = subjectsDAL.GetAllForTeacher(teacherID);
            ViewBag.Times = times;
        }
    }
}