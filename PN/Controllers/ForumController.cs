using System;
using PN.Models;
using System.IO;
using System.Net;
using PN.Services;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace PN.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Forum
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Forum.ToList());
        }

        // GET: Forum/Subscribe/5
        public ActionResult Subscribe(int? id)
        {
            if (id == null) return HttpNotFound();

            var userService = new UserService();
            var userInfo = userService.GetUserInformation();

            if (!db.UserForumSubscription.Any(o => o.UserId == userInfo.Id && o.ForumId == id.Value))
            {
                var subscription = new UserForumSubscription
                {
                    UserId = userInfo.Id,
                    ForumId = id.Value,
                    Score = 0
                };

                db.UserForumSubscription.Add(subscription);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id.Value });
            }
            return HttpNotFound();
        }

        // GET: Forum/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
            return View(forum);
        }

        // GET: Forum/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CreateForumViewModel();

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
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var forum = db.Forum.Find(id);
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Forum forum = db.Forum.Find(id);
            if (forum == null)
            {
                return HttpNotFound();
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
