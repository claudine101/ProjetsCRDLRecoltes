using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEMPLATE.Models;

namespace TEMPLATE.Controllers
{
    public class CommuneController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var communes = db.communes.Include(c => c.province);
            return View(communes.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            commune commune = db.communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            return View(commune);
        }


        public ActionResult Create()
        {
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            return View();
        }

        [HttpPost]
        public ActionResult Create(commune commune)
        {
            if (ModelState.IsValid)
            {
                db.communes.Add(commune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province", commune.ID_province);
            return View(commune);
        }

        //
        // GET: /Commune/Edit/5

        public ActionResult Edit(int id = 0)
        {
            commune commune = db.communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province", commune.ID_province);
            return View(commune);
        }

        //
        // POST: /Commune/Edit/5

        [HttpPost]
        public ActionResult Edit(commune commune)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commune).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province", commune.ID_province);
            return View(commune);
        }

        //
        // GET: /Commune/Delete/5

        public ActionResult Delete(int id = 0)
        {
            commune commune = db.communes.Find(id);
            if (commune == null)
            {
                return HttpNotFound();
            }
            return View(commune);
        }

        //
        // POST: /Commune/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            commune commune = db.communes.Find(id);
            db.communes.Remove(commune);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}