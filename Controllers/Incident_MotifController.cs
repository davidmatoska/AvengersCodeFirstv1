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
    public class Incident_MotifController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Incident_Motif
        public ActionResult Index()
        {
            
            return View(db.Incident_Motifs.ToList());
        }

        // GET: Incident_Motif/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Motif incident_Motif = db.Incident_Motifs.Include(s => s.Files).SingleOrDefault(s => s.Incident_MotifID == id);
            
            if (incident_Motif == null)
            {
                return HttpNotFound();
            }
            return View(incident_Motif);
        }

        // GET: Incident_Motif/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Incident_Motif/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Incident_MotifID,Motif")] Incident_Motif incident_Motif, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var symbole = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Symbole,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        symbole.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    incident_Motif.Files = new List<File> { symbole };
                }
                db.Incident_Motifs.Add(incident_Motif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incident_Motif);
        }

        // GET: Incident_Motif/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Motif incident_Motif = db.Incident_Motifs.Include(s => s.Files).SingleOrDefault(s => s.Incident_MotifID == id);
            if (incident_Motif == null)
            {
                return HttpNotFound();
            }
            return View(incident_Motif);
        }

        // POST: Incident_Motif/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Incident_MotifUpdate = db.Incident_Motifs.Find(id);
            if (TryUpdateModel(Incident_MotifUpdate, "",
                new string[] { "Motif", }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (Incident_MotifUpdate.Files.Any(f => f.FileType == FileType.Symbole))
                        {
                            db.Files.Remove(Incident_MotifUpdate.Files.First(f => f.FileType == FileType.Symbole));
                        }
                        var symbole = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Symbole,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            symbole.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        Incident_MotifUpdate.Files = new List<File> { symbole };
                    }
                    db.Entry(Incident_MotifUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(Incident_MotifUpdate);
        }

        // GET: Incident_Motif/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incident_Motif incident_Motif = db.Incident_Motifs.Find(id);
            if (incident_Motif == null)
            {
                return HttpNotFound();
            }
            return View(incident_Motif);
        }

        // POST: Incident_Motif/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incident_Motif incident_Motif = db.Incident_Motifs.Find(id);
            db.Incident_Motifs.Remove(incident_Motif);
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
