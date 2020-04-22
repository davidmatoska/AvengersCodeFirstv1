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
    public class RapportMissionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RapportMissions
        public ActionResult Index()
        {
            return View(db.RapportMissions.ToList());
        }

        // GET: RapportMissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapportMission rapportMission = db.RapportMissions.Find(id);
            if (rapportMission == null)
            {
                return HttpNotFound();
            }
            return View(rapportMission);
        }

        // GET: RapportMissions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RapportMissions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RapportMissionID,IncidentID,HerosID,CommentaireMission,DateRapport")] RapportMission rapportMission)
        {
            if (ModelState.IsValid)
            {
                db.RapportMissions.Add(rapportMission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rapportMission);
        }

        // GET: RapportMissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapportMission rapportMission = db.RapportMissions.Find(id);
            if (rapportMission == null)
            {
                return HttpNotFound();
            }
            return View(rapportMission);
        }

        // POST: RapportMissions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RapportMissionID,IncidentID,HerosID,CommentaireMission,DateRapport")] RapportMission rapportMission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rapportMission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rapportMission);
        }

        // GET: RapportMissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RapportMission rapportMission = db.RapportMissions.Find(id);
            if (rapportMission == null)
            {
                return HttpNotFound();
            }
            return View(rapportMission);
        }

        // POST: RapportMissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RapportMission rapportMission = db.RapportMissions.Find(id);
            db.RapportMissions.Remove(rapportMission);
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
