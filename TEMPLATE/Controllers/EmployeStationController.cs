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
    public class EmployeStationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var employe_station_lavage = db.employe_station_lavage.Include(e => e.station_lavage);
            return View(employe_station_lavage.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            employe_station_lavage employe_station_lavage = db.employe_station_lavage.Find(id);
            if (employe_station_lavage == null)
            {
                return HttpNotFound();
            }
            return View(employe_station_lavage);
        }


        public ActionResult Create()
        {
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            return View();
        }

        [HttpPost]
        public ActionResult Create(employe_station_lavage employe_station_lavage)
        {
            if (ModelState.IsValid)
            {
                db.employe_station_lavage.Add(employe_station_lavage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", employe_station_lavage.ID_station);
            return View(employe_station_lavage);
        }

    
        public ActionResult Edit(int id = 0)
        {
            employe_station_lavage employe_station_lavage = db.employe_station_lavage.Find(id);
            if (employe_station_lavage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", employe_station_lavage.ID_station);
            return View(employe_station_lavage);
        }

        [HttpPost]
        public ActionResult Edit(employe_station_lavage employe_station_lavage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe_station_lavage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", employe_station_lavage.ID_station);
            return View(employe_station_lavage);
        }


        public ActionResult Delete(int id = 0)
        {
            employe_station_lavage employe_station_lavage = db.employe_station_lavage.Find(id);
            if (employe_station_lavage == null)
            {
                return HttpNotFound();
            }
            return View(employe_station_lavage);
        }

        //
        // POST: /EmployeStation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            employe_station_lavage employe_station_lavage = db.employe_station_lavage.Find(id);
            db.employe_station_lavage.Remove(employe_station_lavage);
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