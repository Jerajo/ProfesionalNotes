using System;
using PN.Models;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq.Dynamic;

namespace PN.Controllers
{
    [Authorize]
    public class PostController : BaseController
    {
        [Authorize(Roles = "admin")]
        public ActionResult Posts()
        {
            var model = db.Post.Take(20).ToList();
            return View();
        }

        // GET: Post/Index/5
        [AllowAnonymous]
        public ActionResult Index(string forumName, string tagName)
        {
            if (!db.Tag.Any(m => m.Name == tagName)) PageNotFound();

            //var posts = db.Post.Where(m => m.TagPost.Any(o => o.Tag.Name == tagName)).ToList();
            var postList = db.TagPost.Join(db.Post, m => m.PostId, o => o.Id, (m,o) => new PostLink { Place = m.Place, Title = o.Title }).ToList();

            var model = new HomePostTagViewModel()
            {
                Tag = db.Tag.First(m => m.Name == tagName),
                PostLinks = postList
            };

            ViewBag.forumName = forumName;
            ViewBag.tagName = tagName;

            return View(model);
        }

        // GET: Post/Details/5
        [AllowAnonymous]
        public ActionResult Read(string forumName, string tagName, string postTitle)
        {
            if (!db.Post.Any(m => m.Title == postTitle)) return PageNotFound();

            var post = db.Post.First(m => m.Title == postTitle);
            if (post == null) return HttpNotFound();

            var postList = db.TagPost.Join(db.Post, m => m.PostId, o => o.Id, (m, o) => new PostLink { Place = m.Place, Title = o.Title }).ToList();

            var model = new HomePostTagViewModel()
            {
                Title = post.Title,
                Body = post.Body,
                Tag = db.Tag.First(m => m.Name == tagName),
                PostLinks = postList
            };

            ViewBag.forumName = forumName;
            ViewBag.tagName = tagName;
            ViewBag.postTitle = postTitle;

            return View(model);
        }

        // GET: Post/Create
        public ActionResult Create(string forumName, string tagName)
        {
            if (tagName == null) return PageNotFound();

            var count = db.Post.Where(o => o.TagPost.Any(b => b.Tag.Name == tagName)).Count();
            var model = new CreatePostViewModel
            {
                Tags = db.Tag.ToList(),
                PlacesCount = count + 1
            };

            ViewBag.forumName = forumName;
            ViewBag.tagName = tagName;

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
        public ActionResult Edit(string forumName, string tagName, string postTitle)
        {
            if (postTitle == null) return PageNotFound();

            var post = db.Post.First(m => m.Title == postTitle);
            if (post == null) return HttpNotFound();

            var count = db.Post.Where(o => o.TagPost.Any(b => b.Tag.Name == tagName)).Count();
            var model = new CreatePostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Tags = db.Tag.ToList(),
                PlacesCount = count + 1
            };

            ViewBag.forumName = forumName;
            ViewBag.tagName = tagName;
            ViewBag.postTitle = postTitle;

            return View(model);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                // save post
                var post = new Post
                {
                    Id = model.Id,
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

                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                // Add Forum relation
                var tag = db.Tag.Find(model.SelectedTadId);
                if (!post.TagPost.Any(o => o.Tag == tag))
                {
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Post/Delete/5
        public ActionResult Delete(string forumName, string tagName, string postTitle)
        {
            if (db.Post.Any(m => m.Title == postTitle)) return PageNotFound();

            var post = db.Post.First(m => m.Title == postTitle);
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
