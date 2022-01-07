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
    public class StationLavageController : Controller
    {
        private RecolteEntities db = new RecolteEntities();
        //POUR TOUS LES DEUX(actif et inactifs)
        public ActionResult Index()
        {
            var station = db.station_lavage.Include(a => a.zone);
            var historique = db.historique_station;
            ViewData["station"] = station.ToList();
            ViewData["historique"] = historique.ToList();
            return View();
        }
       
        //POUR STATTION ACTIFS
        public ActionResult Indexe()
        {
            var station = db.station_lavage.Include(a => a.zone);
            var historique = db.historique_station;
            ViewData["station"] = station.ToList();
            ViewData["historique"] = historique.ToList();
            return View();
        }


        public ActionResult Details(int id = 0)
        {
            station_lavage station_lavage = db.station_lavage.Find(id);
            if (station_lavage == null)
            {
                return HttpNotFound();
            }
            return View(station_lavage);
        }
      
        public ActionResult Create()
        {
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            return View();
        }
  
        [HttpPost]
        public ActionResult Create(station_lavage station_lavage)
        {
            if (ModelState.IsValid)
            {
                db.station_lavage.Add(station_lavage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Zone = new SelectList(db.zones, "ID_zone", "NOM_zone", station_lavage.ID_Zone);
            return View(station_lavage);
        }

        public ActionResult Edit(int id = 0)
        {
            station_lavage station_lavage = db.station_lavage.Find(id);
            if (station_lavage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            return View(station_lavage);
        }

        [HttpPost]
        public ActionResult Edit(station_lavage station_lavage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station_lavage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Zone = new SelectList(db.zones, "ID_zone", "NOM_zone", station_lavage.ID_Zone);
            return View(station_lavage);
        }

        public ActionResult Delete(int id = 0)
        {
            station_lavage station_lavage = db.station_lavage.Find(id);
            if (station_lavage == null)
            {
                return HttpNotFound();
            }
            return View(station_lavage);
        }
        // POST: /StationLavage/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            station_lavage station_lavage = db.station_lavage.Find(id);
            db.station_lavage.Remove(station_lavage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //POUR RAPPORT 
        public ActionResult RapportQuantite()
        {
            return View();
        }
        public ActionResult GetDataQuantite()
        {
            RecolteEntities context = new RecolteEntities();
            var quantite = from d in db.recoltes
                           join f in db.station_lavage
                           on d.ID_station equals f.ID_station
                           select new { f.NOM_station, f.ID_station, d.quantite } into x
                           group x by new { x.NOM_station, x.ID_station } into g
                           select new
                           {
                               name = g.Key.NOM_station,
                               key = g.Key.ID_station,
                               count = g.Select(x => x.quantite).Sum()

                           };

            return Json(quantite, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RapportClient()
        {
            return View();
        }
        public ActionResult GetDataClient()
        {
            RecolteEntities context = new RecolteEntities();

            var NombreClient = from s in db.station_lavage
                               join r in db.recoltes
                               on s.ID_station equals r.ID_station
                               join c in db.clients
                               on r.ID_client equals c.ID_client
                               select new { s.NOM_station, s.ID_station, c.ID_client } into x
                               group x by new { x.NOM_station, x.ID_station } into g
                               select new
                               {
                                   name = g.Key.NOM_station,
                                   key = g.Key.ID_station,
                                   count = g.Select(x => x.ID_client).Count()

                               };
            return Json(NombreClient, JsonRequestBehavior.AllowGet);
        }
    }

}