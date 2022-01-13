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
    public class HistoriquePrixController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            var historique_prix = db.historique_prix.Include(h => h.qualite);
            return View(historique_prix.ToList());
        }
        public ActionResult Details(int id = 0)
        {
            historique_prix historique_prix = db.historique_prix.Find(id);
            if (historique_prix == null)
            {
                return HttpNotFound();
            }
            return View(historique_prix);
        }
        public ActionResult Create()
        {
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite");
            return View();
        }

        [HttpPost]
        public ActionResult Create(historique_prix historique_prix)
        {
            if (ModelState.IsValid)
            {
                db.historique_prix.Add(historique_prix);
                db.SaveChanges();
               ViewBag.sms= "Data Berhasil disimpan...";
                return RedirectToAction("Index");
            }

            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", historique_prix.ID_qualite);
            TempData["pesan"] = "Data Berhasil disimpan...";
                        
            return View(historique_prix);
        }


        public ActionResult Edit(int id = 0)
        {
            historique_prix historique_prix = db.historique_prix.Find(id);
            if (historique_prix == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", historique_prix.ID_qualite);
            return View(historique_prix);
        }


        [HttpPost]
        public ActionResult Edit(historique_prix historique_prix)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique_prix).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", historique_prix.ID_qualite);
            return View(historique_prix);
        }

        public ActionResult Delete(int id = 0)
        {
            historique_prix historique_prix = db.historique_prix.Find(id);
            if (historique_prix == null)
            {
                return HttpNotFound();
            }
            return View(historique_prix);
        }

     
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            historique_prix historique_prix = db.historique_prix.Find(id);
            db.historique_prix.Remove(historique_prix);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetDataPrix()
        {
            RecolteEntities context = new RecolteEntities();
            var prix = from h in db.historique_prix
                       
                           
                           select new {h.DATE_insertion, h.PRIX ,h.ID_qualite} into x
                           group x by new {x.DATE_insertion, x.PRIX,x.ID_qualite } into g
                           select new
                           {
                               name = g.Key.DATE_insertion,
                               id=g.Key.ID_qualite,
                               count = g.Key.PRIX


                           };

            return Json(prix,JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDataPrixSeriseB()
        {
            RecolteEntities context = new RecolteEntities();
            var prix = from h in db.historique_prix
                       where h.ID_qualite == 2

                       select new { h.DATE_insertion, h.PRIX } into x
                       group x by new { x.DATE_insertion, x.PRIX } into g
                       select new
                       {
                           name = g.Key.DATE_insertion,
                           count = g.Key.PRIX

                       };

            return Json(prix, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}