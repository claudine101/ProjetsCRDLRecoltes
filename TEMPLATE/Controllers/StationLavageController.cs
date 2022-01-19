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
        public ActionResult Index()
        {
            var donne = from s in db.station_lavage
                        join z in db.zones
                        on s.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        select new recoltModel
                        {
                            ID = s.ID_station,
                            NOM_station= s.NOM_station,
                            tel = s.TEL_station,
                            date = (s.DATE_insertion).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };

            var historique = db.historique_station ;
            ViewData["station"] = donne.ToList();
            ViewData["historique"] = historique.ToList();
            ViewBag.ID_provinceStation = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_communeStation = new SelectList(db.communes, "ID_commune", "NOM_commune");
            return View();
           
        }
        [HttpPost]
        public ActionResult Index(int? ID_provinceStation)
        {
            var province = (from p in db.provinces
                            where p.ID_province == ID_provinceStation
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
            var donne = from s in db.station_lavage
                        join z in db.zones
                        on s.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        where p.ID_province == ID_provinceStation
                        select new recoltModel
                        {
                            ID = s.ID_station,
                            NOM_station = s.NOM_station,
                            tel = s.TEL_station,
                            date = (s.DATE_insertion).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };

            var historique = db.historique_station;
            ViewData["station"] = donne.ToList();
            ViewData["historique"] = historique.ToList();
            ViewBag.ID_provinceStation = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_communeStation = new SelectList(db.communes, "ID_commune", "NOM_commune");
            return View();

        }
       
        //POUR STATTION ACTIFS
        public ActionResult Indexe()
        {
            var donne = from s in db.station_lavage
                        join z in db.zones
                        on s.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        select new recoltModel
                        {
                            ID = s.ID_station,
                            NOM_station = s.NOM_station,
                            tel = s.TEL_station,
                            date = (s.DATE_insertion).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };

            var historique = db.historique_station;
            ViewData["station"] = donne.ToList();
            ViewData["historique"] = historique.ToList();
            ViewBag.ID_provinceStat = new SelectList(db.provinces, "ID_province", "NOM_province");
            return View();
           
        }
        [HttpPost]
        public ActionResult Indexe(int? ID_provinceStat)
        {
            var province = (from p in db.provinces
                            where p.ID_province == ID_provinceStat
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
            var donne = from s in db.station_lavage
                        join z in db.zones
                        on s.ID_Zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        where p.ID_province == ID_provinceStat
                        select new recoltModel
                        {
                            ID = s.ID_station,
                            NOM_station = s.NOM_station,
                            tel = s.TEL_station,
                            date = (s.DATE_insertion).Value,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };

            var historique = db.historique_station;
            ViewData["station"] = donne.ToList();
            ViewData["historique"] = historique.ToList();
            ViewBag.ID_provinceStat = new SelectList(db.provinces, "ID_province", "NOM_province");
            return View();

        }

        public ActionResult Details(int id = 0)
        {
            var nomASS = "";
            var station = (from e in db.station_lavage
                           where e.ID_station == id
                           select new
                           {
                               e.NOM_station

                           }).ToList();


            foreach (var vp in station)
            {
                nomASS = vp.NOM_station;
            }

            ViewBag.ass = nomASS;
            var recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join a in db.station_lavage
                          on d.ID_station equals a.ID_station
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          where a.ID_station == id
                          select new recoltModel
                          {
                              NOM_client = c.NOM_client,
                              assocition = a.NOM_station,
                              PRENOM_client = c.PRENOM_client,
                              quantite = d.quantite,
                              NOM_qualite = q.NOM_qualite,
                              Prix = d.Prix,
                              NOM_station = f.NOM_station,
                              ID_recolte = d.ID_recolte,
                              ID_client = c.ID_client,
                              ID_qualite = q.ID_qualite,
                              ID_station = f.ID_station,
                              date = (d.Date_insertion).Value

                          }).ToList();
            return View(recolt.ToList());
        }
        [HttpPost]
        public ActionResult Details(DateTime? startDate, DateTime? endDate, int id = 0)
        {
            ViewBag.mot = " du " + startDate + " au " + endDate;
            var nomASS = "";
            var station = (from e in db.station_lavage
                           where e.ID_station == id
                           select new
                           {
                               e.NOM_station

                           }).ToList();


            foreach (var vp in station)
            {
                nomASS = vp.NOM_station;
            }

            ViewBag.ass = nomASS;
            var recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join a in db.station_lavage
                          on d.ID_station equals a.ID_station
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          where a.ID_station == id && a.DATE_insertion >= startDate && a.DATE_insertion <= endDate
                          select new recoltModel
                          {
                              NOM_client = c.NOM_client,
                              assocition = a.NOM_station,
                              PRENOM_client = c.PRENOM_client,
                              quantite = d.quantite,
                              NOM_qualite = q.NOM_qualite,
                              Prix = d.Prix,
                              NOM_station = f.NOM_station,
                              ID_recolte = d.ID_recolte,
                              ID_client = c.ID_client,
                              ID_qualite = q.ID_qualite,
                              ID_station = f.ID_station,
                              date = (d.Date_insertion).Value

                          }).ToList();
            return View(recolt.ToList());
        }
        
        public ActionResult Details1(int id = 0)
        {
            var nomASS = "";
            var station = (from e in db.station_lavage
                           where e.ID_station == id
                           select new
                           {
                               e.NOM_station

                           }).ToList();


            foreach (var vp in station)
            {
                nomASS = vp.NOM_station;
            }

            ViewBag.ass = nomASS;
            var recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join a in db.associations
                          on c.ID_association equals a.ID_association
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          where f.ID_station == id
                          select new recoltModel
                          {
                              NOM_client = c.NOM_client,
                              PRENOM_client = c.PRENOM_client,
                              date = (d.Date_insertion).Value,
                              assocition = a.NOM_association

                          }).Distinct().ToList(); ;
            return View(recolt);
        }
         [HttpPost]
        public ActionResult Details1(DateTime? startDate, DateTime? endDate, int id = 0)
        {
             ViewBag.mot = " du " + startDate + " au " + endDate;

            var nomASS = "";
            var station = (from e in db.station_lavage
                           where e.ID_station == id
                           select new
                           {
                               e.NOM_station

                           }).ToList();


            foreach (var vp in station)
            {
                nomASS = vp.NOM_station;
            }

            ViewBag.ass = nomASS;
            var recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join a in db.associations
                          on c.ID_association equals a.ID_association
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          where f.ID_station == id && d.Date_insertion >= startDate && d.Date_insertion <= endDate
                          select new recoltModel
                          {
                              NOM_client = c.NOM_client,
                              PRENOM_client = c.PRENOM_client,
                              date = (d.Date_insertion).Value,
                              assocition=a.NOM_association

                          }).ToList(); ;
            return View(recolt);
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
                return RedirectToAction("Indexe");
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
            RecolteEntities context = new RecolteEntities();
            var quantite = from d in db.recoltes
                           join f in db.station_lavage
                           on d.ID_station equals f.ID_station
                           select new { f.NOM_station, d.ID_station, d.quantite } into x
                           group x by new { x.NOM_station, x.ID_station } into g
                           select new recoltModel
                           {
                               name = g.Key.NOM_station,
                               ID=g.Key.ID_station,
                               count = g.Select(x => x.quantite).Sum()

                           };
            return View(quantite);
        }
        [HttpPost]
        public ActionResult RapportQuantite(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.mot = " Rapport du recoltes enregistre du  " + startDate + " au " + endDate;
            RecolteEntities context = new RecolteEntities();
            var quantite = from d in db.recoltes
                           join f in db.station_lavage
                           on d.ID_station equals f.ID_station
                           where f.DATE_insertion >= startDate && f.DATE_insertion <= endDate
                           select new { f.NOM_station, f.ID_station, d.quantite } into x
                           group x by new { x.NOM_station, x.ID_station } into g
                           select new recoltModel
                           {
                               name = g.Key.NOM_station,
                               count = g.Select(x => x.quantite).Sum()

                           };
            return View(quantite);
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
            var NombreClient = from s in db.station_lavage
                               join r in db.recoltes
                               on s.ID_station equals r.ID_station
                               join c in db.clients
                               on r.ID_client equals c.ID_client
                               select new { s.NOM_station, s.ID_station, c.ID_client } into x
                               group x by new { x.NOM_station, x.ID_station } into g
                               select new recoltModel
                               {
                                   name = g.Key.NOM_station,
                                   ID=g.Key.ID_station,
                                   count = g.Select(x => x.ID_client).Count()

                               };
            return View(NombreClient);
        }
        [HttpPost]
        public ActionResult RapportClient(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.mot = " Rapport des clients enregistre du  " + startDate + " au " + endDate;
            var NombreClient = from s in db.station_lavage
                               join r in db.recoltes
                               on s.ID_station equals r.ID_station
                               join c in db.clients
                               on r.ID_client equals c.ID_client
                               where c.DATE_insertion >= startDate && c.DATE_insertion <= endDate
                               select new { s.NOM_station, s.ID_station, c.ID_client } into x
                               group x by new { x.NOM_station, x.ID_station } into g
                               select new recoltModel
                               {
                                   name = g.Key.NOM_station,
                                   count = g.Select(x => x.ID_client).Count()

                               };
            return View(NombreClient);
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
                                   Id= g.Key.ID_station,
                                   count = g.Select(x => x.ID_client).Count()

                               };
            return Json(NombreClient, JsonRequestBehavior.AllowGet);
        }
    }

}