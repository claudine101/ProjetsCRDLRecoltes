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
            var recoltes = db.recoltes.Include(r => r.client).Include(r => r.qualite).Include(r => r.station_lavage);
            return View(recoltes.ToList());
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
            return View(recolte);
        }
        public ActionResult Edit(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
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
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
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