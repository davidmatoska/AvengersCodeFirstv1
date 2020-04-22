using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
            RapportMission rapportMission = db.RapportMissions.Include(s => s.Files).SingleOrDefault(s => s.RapportMissionID == id);
           
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
        public ActionResult Create([Bind(Include = "RapportMissionID,IncidentID,HerosID,CommentaireMission,DateRapport")] RapportMission rapportMission, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var preuve = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Preuve,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        preuve.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    rapportMission.Files = new List<File> { preuve };
                }
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
            RapportMission rapportMission = db.RapportMissions.Include(s => s.Files).SingleOrDefault(s => s.RapportMissionID == id);
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
        public ActionResult Edit(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var RapportUpdate = db.RapportMissions.Find(id);
            if (TryUpdateModel(RapportUpdate, "",
                new string[] { " " }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (RapportUpdate.Files.Any(f => f.FileType == FileType.Preuve))
                        {
                            db.Files.Remove(RapportUpdate.Files.First(f => f.FileType == FileType.Preuve));
                        }
                        var preuve = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Preuve,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            preuve.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        RapportUpdate.Files = new List<File> { preuve };
                    }
                    db.Entry(RapportUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(RapportUpdate);
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
