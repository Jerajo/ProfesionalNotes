using System;
using PN.Models;
using System.IO;
using System.Net;
using PN.Services;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

namespace PN.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Tag/Index/5
        public ActionResult Index(int? id)
        {
            List<Tag> model;
            if (id == null) model = db.Tag.Take(20).ToList();
            else model = db.Forum.Find(id.Value).Tag;
            return View(model);
        }

        // GET: Tag/Subscribe/5
        public ActionResult Subscribe(int? id)
        {
            if (id == null) return HttpNotFound();

            return RedirectToAction("Index", new { id = id.Value });
        }

        // GET: Tag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tag = db.Tag.Find(id);
            if (tag == null) return HttpNotFound();

            return View(tag);
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            var model = new CreateTagViewModel { Forums = db.Forum.ToList() };

            return View(model);
        }

        // POST: Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTagViewModel model)
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
                    model.ImagePath = "~/Content/UploadedImages/Tags/" + fileName;

                    // save image on server
                    fileName = Path.Combine(Server.MapPath("~/Content/UploadedImages/Tags/"), fileName);
                    model.ImageFile.SaveAs(fileName);
                }

                // save data base
                var tag = new Tag
                {
                    Name = model.Name,
                    Desciption = model.Desciption,
                    ImagePath = model.ImagePath,
                };

                // Add Forum relation
                var forum = db.Forum.Find(model.SelectedForumId);
                tag.Forum.Add(forum);

                db.Tag.Add(tag);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            
            var tag = db.Tag.Find(id);
            if (tag == null) return HttpNotFound();

            var model = new CreateTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Desciption = tag.Desciption,
                ImagePath = tag.ImagePath,
                SelectedForumId = tag.Forum.FirstOrDefault()?.Id ?? 0,
                Forums = db.Forum.ToList()
            };

            return View(model);
        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateTagViewModel model)
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
                    model.ImagePath = "~/Content/UploadedImages/Tags/" + fileName;

                    // save image on server
                    fileName = Path.Combine(Server.MapPath("~/Content/UploadedImages/Tags/"), fileName);
                    model.ImageFile.SaveAs(fileName);
                }

                // save data base
                var tag = new Tag
                {
                    Id = model.Id,
                    Name = model.Name,
                    Desciption = model.Desciption,
                    ImagePath = model.ImagePath
                };

                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();

                // Add Forum relation
                if (!tag.Forum.Any(o => o.Id == model.SelectedForumId))
                {
                    var r = db.Database.SqlQuery(typeof(int), $"INSERT INTO ForumTag VALUES({model.SelectedForumId}, {tag.Id})");
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tag = db.Tag.Find(id);
            if (tag == null) return HttpNotFound();

            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tag.Find(id);
            db.Tag.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
