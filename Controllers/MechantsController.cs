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
    public class MechantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mechants
        public ActionResult Index()
        {
            var mechants = db.Mechants.Include(m => m.Civil);
            return View(mechants.ToList());
        }

        // GET: Mechants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mechant mechant = db.Mechants.Find(id);
            if (mechant == null)
            {
                return HttpNotFound();
            }
            return View(mechant);
        }

        // GET: Mechants/Create
        public ActionResult Create()
        {
            ViewBag.MechantID = new SelectList(db.Civils, "CivilID", "Prenom");
            return View();
        }

        // POST: Mechants/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MechantID,CivilID,Pseudonyme,Image_Mechant,Disponible")] Mechant mechant)
        {
            if (ModelState.IsValid)
            {
                db.Mechants.Add(mechant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MechantID = new SelectList(db.Civils, "CivilID", "Prenom", mechant.MechantID);
            return View(mechant);
        }

        // GET: Mechants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mechant mechant = db.Mechants.Find(id);
            if (mechant == null)
            {
                return HttpNotFound();
            }
            ViewBag.MechantID = new SelectList(db.Civils, "CivilID", "Prenom", mechant.MechantID);
            return View(mechant);
        }

        // POST: Mechants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MechantID,CivilID,Pseudonyme,Image_Mechant,Disponible")] Mechant mechant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mechant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MechantID = new SelectList(db.Civils, "CivilID", "Prenom", mechant.MechantID);
            return View(mechant);
        }

        // GET: Mechants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mechant mechant = db.Mechants.Find(id);
            if (mechant == null)
            {
                return HttpNotFound();
            }
            return View(mechant);
        }

        // POST: Mechants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mechant mechant = db.Mechants.Find(id);
            db.Mechants.Remove(mechant);
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
