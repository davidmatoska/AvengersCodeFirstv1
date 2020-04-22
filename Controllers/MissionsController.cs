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
    public class MissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Missions
        public ActionResult Index()
        {
            var missions = db.Missions.Include(m => m.Heros).Include(m => m.Incident).Include(m => m.Mechant);
            return View(missions.ToList());
        }

        // GET: Missions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // GET: Missions/Create
        public ActionResult Create()
        {
            ViewBag.HerosID = new SelectList(db.Heros, "HerosID", "Pseudonyme");
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "Contexte");
            ViewBag.MechantID = new SelectList(db.Mechants, "MechantID", "Pseudonyme");
            return View();
        }

        // POST: Missions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionID,IncidentID,Statut_Mission,HerosID,MechantID,Commentaire,Date_Mission")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Missions.Add(mission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HerosID = new SelectList(db.Heros, "HerosID", "Pseudonyme", mission.HerosID);
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "Contexte", mission.IncidentID);
            ViewBag.MechantID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", mission.MechantID);
            return View(mission);
        }

        // GET: Missions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            ViewBag.HerosID = new SelectList(db.Heros, "HerosID", "Pseudonyme", mission.HerosID);
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "Contexte", mission.IncidentID);
            ViewBag.MechantID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", mission.MechantID);
            return View(mission);
        }

        // POST: Missions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionID,IncidentID,Statut_Mission,HerosID,MechantID,Commentaire,Date_Mission")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HerosID = new SelectList(db.Heros, "HerosID", "Pseudonyme", mission.HerosID);
            ViewBag.IncidentID = new SelectList(db.Incidents, "IncidentID", "Contexte", mission.IncidentID);
            ViewBag.MechantID = new SelectList(db.Mechants, "MechantID", "Pseudonyme", mission.MechantID);
            return View(mission);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mission mission = db.Missions.Find(id);
            db.Missions.Remove(mission);
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
