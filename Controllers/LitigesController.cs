using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Avengers.Models;

namespace Avengers.Controllers
{
    public class LitigesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Litiges
        public ActionResult Index()
        {
            var litiges = db.Litiges.Include(l => l.User);
            return View(litiges.ToList());
        }

        // GET: Litiges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Litige litige = db.Litiges.Find(id);
            if (litige == null)
            {
                return HttpNotFound();
            }
            return View(litige);
        }

        // GET: Litiges/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Litiges/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LitigeID,UserId,RaisonLitige,DateLitige")] Litige litige)
        {
            if (ModelState.IsValid)
            {
                db.Litiges.Add(litige);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", litige.UserId);
            return View(litige);
        }

        // GET: Litiges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Litige litige = db.Litiges.Find(id);
            if (litige == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", litige.UserId);
            return View(litige);
        }

        // POST: Litiges/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LitigeID,UserId,RaisonLitige,DateLitige")] Litige litige)
        {
            if (ModelState.IsValid)
            {
                db.Entry(litige).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", litige.UserId);
            return View(litige);
        }

        // GET: Litiges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Litige litige = db.Litiges.Find(id);
            if (litige == null)
            {
                return HttpNotFound();
            }
            return View(litige);
        }

        // POST: Litiges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Litige litige = db.Litiges.Find(id);
            db.Litiges.Remove(litige);
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
