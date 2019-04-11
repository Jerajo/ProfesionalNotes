using PN.Models;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace PN.Controllers
{
    public class PostController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Post/Index/5
        public ActionResult Index(int? id)
        {
            List<Post> model;
            if (id == null) model = db.Post.Take(20).ToList();
            else
            {
                ViewBag.TagId = id.Value;
                var relations = db.Tag.Find(id.Value).TagPost;
                model = new List<Post>();
                foreach (var rel in relations)
                {
                    model.Add(db.Post.Find(rel.PostId));
                }
            }
            return View(model);
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = db.Post.Find(id);
            if (post == null) return HttpNotFound();

            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var count = db.Post.Where(o => o.TagPost.Any(b => b.TagId == id)).Count();
            var model = new CreatePostViewModel
            {
                Tags = db.Tag.ToList(),
                PlacesCount = count + 1
            };

            return View(model);
        }

        // POST: Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // save post
                var post = new Post
                {
                    Title = model.Title,
                    Body = model.Body,
                    Posted = DateTime.Now
                };

                // save tagPot
                var tagPost = new TagPost
                {
                    TagId = model.SelectedTadId,
                    PostId = post.Id,
                    Place = model.SelectedPlace
                };
                post.TagPost.Add(tagPost);

                db.Post.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Posted")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Post.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Post.Find(id);
            db.Post.Remove(post);
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
