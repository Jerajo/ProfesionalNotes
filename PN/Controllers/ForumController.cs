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
        #region Anonimous

        // GET: Forum
        [AllowAnonymous]
        public ActionResult Index()
        {
            var isFirstInit = IsFirstInit();
            // TODO: Filter forums in relevancy order
            return View(db.Forum.ToList());
        }

        // GET: Forum/Details/5
        [AllowAnonymous]
        public ActionResult Details(string forumName)
        {
            var isFirstInit = IsFirstInit();
            if (string.IsNullOrEmpty(forumName)) return PageNotFound();

            var forum = db.Forum.First(m => m.Name == forumName);
            if (forum == null) return HttpNotFound();

            var model = new DetailsForumViewModel
            {
                Id = forum.Id,
                Name = forum.Name,
                Desciption = forum.Desciption,
                ImagePath = forum.ImagePath,
            };

            return View(model);
        }

        #endregion

        #region UserActivities

        // GET: Forum/Subscribe/5
        public ActionResult Subscribe(string forumName)
        {
            if (!db.Forum.Any(m => m.Name == forumName)) return PageNotFound();

            using (var userService = new UserService())
            {
                var UserId = userService.UserInformation.Id;
                var forum = db.Forum.First(m => m.Name == forumName);
                var userForumSubcription = db.ForumUser.Where(m => m.UserId == UserId && m.ForumId == forum.Id).FirstOrDefault();

                if (userForumSubcription == null)
                {
                    userForumSubcription = new ForumUser
                    {
                        UserId = UserId,
                        ForumId = forum.Id,
                        Score = 0,
                        IsSubscribed = true,
                        IsReadLater = false,
                        HasRead = false,
                        Vote = null
                    };

                    forum.ForumUser.Add(userForumSubcription);
                    db.Entry(forum).State = EntityState.Modified;
                }
                else
                {
                    userForumSubcription.IsSubscribed = !userForumSubcription.IsSubscribed;
                    db.Entry(userForumSubcription).State = EntityState.Modified;
                }

                db.SaveChanges();
            }

            var preious = Request.UrlReferrer.PathAndQuery;
            var next = $"~/{Service.Language}/{Service.ForumTitle}";
            if (preious == null) return RedirectToLocal(next);

            var redirectUrl = (preious.Contains(Service.ForumTitle)) ? next : preious;

            return RedirectToLocal(redirectUrl);
        }

        #endregion

        #region Create

        // GET: Forum/Create
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

                using (var userService = new UserService())
                {
                    var userId = userService.UserInformation.Id;

                    // save data base
                    var forum = new Forum
                    {
                        UserId = userId,
                        Name = model.Name,
                        Desciption = model.Desciption,
                        ImagePath = model.ImagePath,
                        ISOLanguage = Service.Language
                    };

                    var forumUser = new ForumUser
                    {
                        UserId = userId,
                        Score = 0,
                        IsSubscribed = true,
                        IsReadLater = false,
                        HasRead = false,
                        Vote = null
                    };

                    forum.ForumUser.Add(forumUser);

                    db.Forum.Add(forum);
                    db.SaveChanges();
                }

                return RedirectToLocal($"~/{Service.Language}/{Service.ForumTitle}");
            }

            return View(model);
        }

        #endregion

        #region Edit

        // GET: Forum/Edit/5
        public ActionResult Edit(string forumName)
        {
            if (!db.Forum.Any(m => m.Name == forumName)) return PageNotFound();

            using (var userService = new UserService())
            {
                var forum = db.Forum.First(m => m.Name == forumName);

                var model = new CreateForumViewModel
                {
                    Id = forum.Id,
                    UserInformation = forum.UserInformation,
                    Name = forum.Name,
                    Desciption = forum.Desciption,
                    ImagePath = forum.ImagePath
                };

                return View(model);
            }
        }

        // POST: Forum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                var forum = db.Forum.Find(model.Id);
                forum.Name = model.Name;
                forum.Desciption = model.Desciption;
                forum.ImagePath = model.ImagePath;

                db.Entry(forum).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        #endregion

        #region Delete

        // GET: Forum/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            Forum forum = db.Forum.Find(id);
            db.Forum.Remove(forum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
