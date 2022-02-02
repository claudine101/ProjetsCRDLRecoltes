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
        {var donne = from a in db.station_lavage
                    
                        join z in db.zones
                        on a.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        join h in db.historique_station 
                        on a.ID_station equals h.ID_station
                        select new recoltModel
                        {
                            ID = h.ID_histoStation,
                            NOM_station = a.NOM_station,
                            tel = a.TEL_station,
                            Date_insertion=(a.DATE_insertion).Value,
                            date = (h.DATE_desactive).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };
            ViewData["station"] = donne.ToList();
            ViewBag.ID_provinceInactifse = new SelectList(db.provinces, "ID_province", "NOM_province");
            return View();
        }
        [HttpPost]
        public ActionResult Index(int? ID_provinceState)
        {
            var province = (from p in db.provinces
                            where p.ID_province == ID_provinceState
                            select new
                            {

                                province = p.NOM_province
                            }).ToList();
            var nomprovinc = "";
            foreach (var vp in province)
            {
                nomprovinc = vp.province;
            }
            ViewBag.nomprovince = "qui se trouve dans le province " + nomprovinc;
            var donne = from a in db.station_lavage
                    
                        join z in db.zones
                        on a.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        join h in db.historique_station 
                        on a.ID_station equals h.ID_station
                        where p.ID_province == ID_provinceState
                        select new recoltModel
                        {
                            ID = h.ID_histoStation,
                            NOM_station = a.NOM_station,
                            tel = a.TEL_station,
                            Date_insertion=(a.DATE_insertion).Value,
                            date = (h.DATE_desactive).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };
            ViewData["station"] = donne.ToList();
            ViewBag.ID_provinceInactifse = new SelectList(db.provinces, "ID_province", "NOM_province");
            return View();
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

        public ActionResult Create()
        {
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            return View();
        }


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

        public ActionResult Delete(int id = 0)
        {
            historique_station historique_station = db.historique_station.Find(id);
            if (historique_station == null)
            {
                return HttpNotFound();
            }
            return View(historique_station);
        }


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