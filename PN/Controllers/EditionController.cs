using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PN.Models;

namespace PN.Controllers
{
    public class EditionController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Editions
        public ActionResult Index()
        {
            return View(db.Edition.ToList());
        }

        // GET: Editions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Edition.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // GET: Editions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Editions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Posted")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Edition.Add(edition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(edition);
        }

        // GET: Editions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Edition.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Editions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Posted")] Edition edition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(edition);
        }

        // GET: Editions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Edition edition = db.Edition.Find(id);
            if (edition == null)
            {
                return HttpNotFound();
            }
            return View(edition);
        }

        // POST: Editions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Edition edition = db.Edition.Find(id);
            db.Edition.Remove(edition);
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
