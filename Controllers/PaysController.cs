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
    public class PaysController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pays
        public ActionResult Index()
        {
            return View(db.Pays.ToList());
        }

        // GET: Pays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pays pays = db.Pays.Include(s => s.Files).SingleOrDefault(s => s.PaysID == id);
            
            if (pays == null)
            {
                return HttpNotFound();
            }
            return View(pays);
        }

        // GET: Pays/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pays/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaysID,Pays_nom")] Pays pays, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var drapeau = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Drapeau,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        drapeau.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    pays.Files = new List<File> { drapeau };
                }
                db.Pays.Add(pays);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pays);
        }

        // GET: Pays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pays pays = db.Pays.Include(s => s.Files).SingleOrDefault(s => s.PaysID == id);
            if (pays == null)
            {
                return HttpNotFound();
            }
            return View(pays);
        }

        // POST: Pays/Edit/5
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
            var PaysUpdate = db.Pays.Find(id);
            if (TryUpdateModel(PaysUpdate, "",
                new string[] { "Pays_Nom"}))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (PaysUpdate.Files.Any(f => f.FileType == FileType.Drapeau))
                        {
                            db.Files.Remove(PaysUpdate.Files.First(f => f.FileType == FileType.Drapeau));
                        }
                        var drapeau = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Drapeau,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            drapeau.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        PaysUpdate.Files = new List<File> { drapeau };
                    }
                    db.Entry(PaysUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(PaysUpdate);
        }

        // GET: Pays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pays pays = db.Pays.Find(id);
            if (pays == null)
            {
                return HttpNotFound();
            }
            return View(pays);
        }

        // POST: Pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pays pays = db.Pays.Find(id);
            db.Pays.Remove(pays);
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
