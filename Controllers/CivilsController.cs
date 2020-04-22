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
    public class CivilsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Civils
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PaysSortParm = String.IsNullOrEmpty(sortOrder) ? "Pays_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var civils = db.Civils.Include(c => c.Heros).Include(c => c.Mechant).Include(c => c.Pays).Include(c => c.User);
            if (!String.IsNullOrEmpty(searchString))
            {
                civils = civils.Where(s => s.Nom.Contains(searchString)
                                       || s.Prenom.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    civils = civils.OrderByDescending(s => s.Nom);
                    break;
                case "Date":
                    civils = civils.OrderBy(s => s.Date_de_naissance);
                    break;
                case "date_desc":
                    civils = civils.OrderByDescending(s => s.Date_de_naissance);
                    break;
                
                case "Pays_desc":
                    civils = civils.OrderByDescending(s => s.Pays.Pays_nom);
                    break;
                default:
                    civils = civils.OrderBy(s => s.Nom);
                    break;
            }
            return View(civils.ToList());
        }

        // GET: Civils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Civil civil = db.Civils.Find(id);
            if (civil == null)
            {
                return HttpNotFound();
            }
            return View(civil);
        }

        // GET: Civils/Create
        public ActionResult Create()
        {
            ViewBag.CivilID = new SelectList(db.Heros, "HerosID", "Pseudonyme");
            ViewBag.CivilID = new SelectList(db.Mechants, "MechantID", "Pseudonyme");
            ViewBag.PaysID = new SelectList(db.Pays, "PaysID", "Pays_nom");
            ViewBag.CivilID = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Civils/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CivilID,Prenom,Nom,Civilite,Adresse,PaysID,Date_de_naissance")] Civil civil)
        {
            if (ModelState.IsValid)
            {
                db.Civils.Add(civil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CivilID = new SelectList(db.Heros, "HerosID", "Pseudonyme", civil.CivilID);
            ViewBag.CivilID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", civil.CivilID);
            ViewBag.PaysID = new SelectList(db.Pays, "PaysID", "Pays_nom", civil.PaysID);
            ViewBag.CivilID = new SelectList(db.Users, "Id", "Email", civil.CivilID);
            return View(civil);
        }

        // GET: Civils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Civil civil = db.Civils.Find(id);
            if (civil == null)
            {
                return HttpNotFound();
            }
            ViewBag.CivilID = new SelectList(db.Heros, "HerosID", "Pseudonyme", civil.CivilID);
            ViewBag.CivilID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", civil.CivilID);
            ViewBag.PaysID = new SelectList(db.Pays, "PaysID", "Pays_nom", civil.PaysID);
            ViewBag.CivilID = new SelectList(db.Users, "Id", "Email", civil.CivilID);
            return View(civil);
        }

        // POST: Civils/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CivilID,Prenom,Nom,Civilite,Adresse,PaysID,Date_de_naissance")] Civil civil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(civil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CivilID = new SelectList(db.Heros, "HerosID", "Pseudonyme", civil.CivilID);
            ViewBag.CivilID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", civil.CivilID);
            ViewBag.PaysID = new SelectList(db.Pays, "PaysID", "Pays_nom", civil.PaysID);
            ViewBag.CivilID = new SelectList(db.Users, "Id", "Email", civil.CivilID);
            return View(civil);
        }

        // GET: Civils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Civil civil = db.Civils.Find(id);
            if (civil == null)
            {
                return HttpNotFound();
            }
            return View(civil);
        }

        // POST: Civils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Civil civil = db.Civils.Find(id);
            db.Civils.Remove(civil);
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
