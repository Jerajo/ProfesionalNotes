using System;
using PN.Models;
using System.IO;
using PN.Services;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PN.Controllers
{
    [Authorize]
    public class ForumController : BaseController
    {
        // GET: Forum
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Forum.ToList());
        }

        // GET: Forum/Subscribe/5
        public ActionResult Subscribe(string forumName)
        {
            if (!db.Forum.Any(m => m.Name == forumName)) return PageNotFound();

            using (var userService = new UserService())
            {
                var userInfo = userService.GetUserInformation();
                var forum = db.Forum.First(m => m.Name == forumName);
                var userForumSubcription = db.UserForumSubscription.Where(m => m.UserId == userInfo.Id && m.ForumId == forum.Id).FirstOrDefault();

                if (userForumSubcription == null)
                {
                    userForumSubcription = new UserForumSubscription
                    {
                        UserId = userInfo.Id,
                        ForumId = forum.Id,
                        Score = 0
                    };

                    forum.UserForumSubscription.Add(userForumSubcription);
                }
                else forum.UserForumSubscription.Remove(userForumSubcription);

                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();
            }

            var service = new LanguagesService();
            var preious = Request.UrlReferrer.PathAndQuery;
            var next = $"~/{service.Language}/{service.ForumTitle}";
            if (preious == null) return RedirectToLocal(next);

            var redirectUrl = (preious.Contains(service.ForumTitle)) ? next : preious;

            return RedirectToLocal(redirectUrl);
        }

        // GET: Forum/Details/5
        [AllowAnonymous]
        public ActionResult Details(string forumName)
        {
            if (string.IsNullOrEmpty(forumName)) return PageNotFound();

            var forum = db.Forum.First(m => m.Name == forumName);
            if (forum == null) return HttpNotFound();

            var model = new DetailsForumViewModel
            {
                Id = forum.Id,
                Name = forum.Name,
                Desciption = forum.Desciption,
                ImagePath = forum.ImagePath
            };

            return View(model);
        }

        // GET: Forum/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CreateForumViewModel() { ImagePath = "~/Content/images/No-image.svg" };

            return View(model);
        }

        // POST: Forum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreateForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    // generate fine name
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var extension = Path.GetExtension(model.ImageFile.FileName);
                    fileName += DateTime.Now.ToString("yy-mm-ss-ffff") + extension;

                    // set image path
                    model.ImagePath = "~/Content/UploadedImages/Forums/" + fileName;

                    // save image on server
                    fileName = Path.Combine(Server.MapPath("~/Content/UploadedImages/Forums/"), fileName);
                    model.ImageFile.SaveAs(fileName);
                }

                // save data base
                var forum = new Forum
                {
                    Name = model.Name,
                    Desciption = model.Desciption,
                    ImagePath = model.ImagePath
                };
                db.Forum.Add(forum);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Forum/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string forumName)
        {
            if (forumName == null) return PageNotFound();

            var forum = db.Forum.First(m => m.Name == forumName);
            if (forum == null) return HttpNotFound();

            var model = new CreateForumViewModel
            {
                Id = forum.Id,
                Name = forum.Name,
                Desciption = forum.Desciption,
                ImagePath = forum.ImagePath
            };

            return View(model);
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(CreateForumViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    // generate fine name
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var extension = Path.GetExtension(model.ImageFile.FileName);
                    fileName += DateTime.Now.ToString("yy-mm-ss-ffff") + extension;

                    // set image path
                    model.ImagePath = "~/Content/UploadedImages/Forums/" + fileName;

                    // save image on server
                    fileName = Path.Combine(Server.MapPath("~/Content/UploadedImages/Forums/"), fileName);
                    model.ImageFile.SaveAs(fileName);
                }

                // save data base
                var forum = new Forum
                {
                    Id = model.Id,
                    Name = model.Name,
                    Desciption = model.Desciption,
                    ImagePath = model.ImagePath
                };
                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Forum/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string forumName)
        {
            if (forumName == null) return PageNotFound();

            var forum = db.Forum.First(m => m.Name == forumName);
            if (forum == null) return HttpNotFound();

            return View(forum);
        }

        // POST: Forum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forum.Find(id);
            db.Forum.Remove(forum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
