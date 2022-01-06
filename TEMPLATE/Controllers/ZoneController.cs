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
    public class ZoneController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var zones = db.zones.Include(z => z.commune);
            return View(zones.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            zone zone = db.zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }


        public ActionResult Create()
        {
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            return View();
        }

        //
        // POST: /Zone/Create

        [HttpPost]
        public ActionResult Create(zone zone)
        {
            if (ModelState.IsValid)
            {
                db.zones.Add(zone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune", zone.ID_commune);
            return View(zone);
        }

        //
        // GET: /Zone/Edit/5

        public ActionResult Edit(int id = 0)
        {
            zone zone = db.zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune", zone.ID_commune);
            return View(zone);
        }

        //
        // POST: /Zone/Edit/5

        [HttpPost]
        public ActionResult Edit(zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune", zone.ID_commune);
            return View(zone);
        }

        //
        // GET: /Zone/Delete/5

        public ActionResult Delete(int id = 0)
        {
            zone zone = db.zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        //
        // POST: /Zone/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            zone zone = db.zones.Find(id);
            db.zones.Remove(zone);
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