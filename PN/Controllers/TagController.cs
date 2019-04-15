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
    public class TagController : BaseController
    {
        // GET: Tag/Index/5
        public ActionResult Index(string forumName, string tagName)
        {
            List<Tag> model;
            if (forumName == null) model = db.Tag.Take(20).ToList();
            else model = db.Forum.First(m => m.Name == forumName).Tag;

            ViewBag.forumName = forumName;

            return View(model);
        }

        // GET: Tag/Subscribe/5
        public ActionResult Subscribe(string forumName, string tagName)
        {
            if (!db.Forum.Any(m => m.Name == forumName) || !db.Tag.Any(m => m.Name == tagName))
                return PageNotFound();

            if (tagName == null) return PageNotFound();
            var service = new LanguagesService();
            var preious = Request.UrlReferrer.PathAndQuery;
            var next = $"~/{service.Language}/{service.TagTitle}/{forumName}";
            if (preious == null) return RedirectToLocal(next);

            var redirectUrl = (preious.Contains(service.TagTitle)) ? next : preious;

            return RedirectToLocal(redirectUrl);
        }

        // GET: Tag/Details/5
        public ActionResult Details(string forumName, string tagName)
        {
            if (!db.Forum.Any(m => m.Name == forumName) || !db.Tag.Any(m => m.Name == tagName))
                return PageNotFound();

            var tag = db.Tag.First(m => m.Name == tagName);
            var model = new DetailsTagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Desciption = tag.Desciption,
                ImagePath = tag.ImagePath,
                Forums = tag.Forum
            };

            ViewBag.forumName = forumName;

            return View(model);
        }

        // GET: Tag/Create
        public ActionResult Create(string forumName, string tagName)
        {
            if (!db.Forum.Any(m => m.Name == forumName)) return PageNotFound();

            var model = new CreateTagViewModel
            {
                Forums = db.Forum.ToList(),
                ImagePath = "~/Content/images/No-image.svg",
                SelectedForumId = db.Forum.First(m => m.Name == forumName).Id
            };

            ViewBag.forumName = forumName;

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

                var service = new LanguagesService();
                return RedirectToLocal($"~/{service.Language}/{service.TagTitle}/{forum.Name}");
            }

            return View(model);
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(string forumName, string tagName)
        {
            if (tagName == null) return PageNotFound();

            var tag = db.Tag.First(m => m.Name == tagName);
            if (tag == null) return PageNotFound();

            ViewBag.forumName = forumName;
            ViewBag.tagName = tagName;

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

                var service = new LanguagesService();
                var forum = db.Forum.Find(model.SelectedForumId);
                return RedirectToLocal($"~/{service.Language}/{service.TagTitle}/{forum.Name}");
            }

            return View(model);
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(string tagName)
        {
            if (tagName == null) return PageNotFound();

            var tag = db.Tag.First(m => m.Name == tagName);
            if (tag == null) return PageNotFound();

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
    }
}
