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
    public class HerosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Heros
        public ActionResult Index()
        {
            var heros = db.Heros.Include(h => h.Civil);
            return View(heros.ToList());
        }

        // GET: Heros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heros heros = db.Heros.Include(s => s.Files).SingleOrDefault(s => s.HerosID == id);
            
            if (heros == null)
            {
                return HttpNotFound();
            }
            return View(heros);
        }

        // GET: Heros/Create
        public ActionResult Create()
        {
            ViewBag.HerosID = new SelectList(db.Civils, "CivilID", "NomComplet");
            return View();
        }

        // POST: Heros/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HerosID,Pseudonyme,Telephone_Secret,Image_Heros,Disponible")] Heros heros, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var hero = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Hero,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        hero.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    heros.Files = new List<File> { hero };
                }
                db.Heros.Add(heros);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HerosID = new SelectList(db.Civils, "CivilID", "NomComplet", heros.HerosID);
            return View(heros);
        }

        // GET: Heros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heros heros = db.Heros.Include(s => s.Files).SingleOrDefault(s => s.HerosID == id);
            if (heros == null)
            {
                return HttpNotFound();
            }
            ViewBag.HerosID = new SelectList(db.Civils, "CivilID", "NomComplet", heros.HerosID);
            return View(heros);
        }

        // POST: Heros/Edit/5
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
            var HeroUpdate = db.Heros.Find(id);
            if (TryUpdateModel(HeroUpdate, "",
                new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (HeroUpdate.Files.Any(f => f.FileType == FileType.Hero))
                        {
                            db.Files.Remove(HeroUpdate.Files.First(f => f.FileType == FileType.Hero));
                        }
                        var hero = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Hero,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            hero.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        HeroUpdate.Files = new List<File> { hero };
                    }
                    db.Entry(HeroUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(HeroUpdate);
        }

        // GET: Heros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Heros heros = db.Heros.Find(id);
            if (heros == null)
            {
                return HttpNotFound();
            }
            return View(heros);
        }

        // POST: Heros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Heros heros = db.Heros.Find(id);
            db.Heros.Remove(heros);
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
