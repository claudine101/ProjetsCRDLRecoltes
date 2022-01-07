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
    public class HistoriqueStationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var historique_station = db.historique_station.Include(h => h.station_lavage);
            return View(historique_station.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            historique_station historique_station = db.historique_station.Find(id);
            if (historique_station == null)
            {
                return HttpNotFound();
            }
            return View(historique_station);
        }

        //
        // GET: /HistoriqueStation/Create

        public ActionResult Create()
        {
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            return View();
        }

        //
        // POST: /HistoriqueStation/Create

        [HttpPost]
        public ActionResult Create(historique_station historique_station)
        {
            if (ModelState.IsValid)
            {
                db.historique_station.Add(historique_station);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", historique_station.ID_station);
            return View(historique_station);
        }

        //
        // GET: /HistoriqueStation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            historique_station historique_station = db.historique_station.Find(id);
            if (historique_station == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", historique_station.ID_station);
            return View(historique_station);
        }

        //
        // POST: /HistoriqueStation/Edit/5

        [HttpPost]
        public ActionResult Edit(historique_station historique_station)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique_station).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", historique_station.ID_station);
            return View(historique_station);
        }

        //
        // GET: /HistoriqueStation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            historique_station historique_station = db.historique_station.Find(id);
            if (historique_station == null)
            {
                return HttpNotFound();
            }
            return View(historique_station);
        }

        //
        // POST: /HistoriqueStation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            historique_station historique_station = db.historique_station.Find(id);
            db.historique_station.Remove(historique_station);
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