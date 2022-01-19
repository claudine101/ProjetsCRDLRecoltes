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
    public class RecolteController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var recolt = (from d in db.recoltes
                      join c in db.clients
                      on d.ID_client equals c.ID_client
                      join q in db.qualites
                      on d.ID_qualite equals q.ID_qualite
                      join f in db.station_lavage
                      on d.ID_station equals f.ID_station
                      select new recoltModel
                      {
                          NOM_client=c.NOM_client,
                          PRENOM_client = c.PRENOM_client,
                          quantite =d.quantite,
                          NOM_qualite =q.NOM_qualite,
                          Prix =d.Prix,
                          NOM_station= f.NOM_station,
                          ID_recolte =d.ID_recolte,
                          ID_client =c.ID_client,
                          ID_qualite =q.ID_qualite,
                          ID_station = f.ID_station,
                          date=(d.Date_insertion).Value

                      }).ToList();

            ViewBag.station = new SelectList(db.station_lavage, "ID_station", "NOM_station"); 
            return View(recolt.ToList());
        }
        [HttpPost]
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int? station)
        {
            var province = (from p in db.station_lavage
                            where p.ID_station == station
                            select new
                            {

                                station = p.NOM_station
                            }).ToList();
            var nomprovinc = "";
            foreach (var vp in province)
            {
                nomprovinc = vp.station;
            }
            ViewBag.nomprovince = "qui se trouve dans  la station de lavage  " + nomprovinc;
            ViewBag.mot = " du " + startDate + " au " + endDate;
            
            var recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          where f.ID_station == station || d.Date_insertion >= startDate && d.Date_insertion <= endDate 
                          select new recoltModel
                          {
                              NOM_client = c.NOM_client,
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

            ViewBag.station = new SelectList(db.station_lavage, "ID_station", "NOM_station"); 
            return View(recolt.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            return View(recolte);
        }

        public ActionResult Create()
        {
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI");
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite");
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View();
        }

        [HttpPost]
        public ActionResult Create(recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.recoltes.Add(recolte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(recolte);
        }
        public ActionResult Edit(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "NOM_client", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(recolte);
        }

        [HttpPost]
        public ActionResult Edit(recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recolte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "NOM_client", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(recolte);
        }
        public ActionResult Delete(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            return View(recolte);
        }

        
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            recolte recolte = db.recoltes.Find(id);
            db.recoltes.Remove(recolte);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        //POUR LE RAPPORT 

    }
}