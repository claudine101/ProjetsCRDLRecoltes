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
    public class HistoriqueAssoctionController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //POUR LES ASSOCTIATION INACTIFS
        public ActionResult Index()
        {
            var donne = from a in db.associations
                        join c in db.collines
                        on a.ID_colline equals c.ID_colline
                        join z in db.zones
                        on c.ID_zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        join h in db.historique_asscociation 
                        on a.ID_association equals h.ID_association
                        select new recoltModel
                        {
                            ID = h.ID_histoAssoc,
                            assocition = a.NOM_association,
                            tel = a.TEL_association,
                            date = (a.DATE_association).Value,
                            colline = c.NOM_colline,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        };
            ViewData["association"] = donne.ToList();
            ViewBag.ID_provinceInactifs = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_communeInactifs = new SelectList(db.communes, "ID_commune", "NOM_commune");
            return View();
     
        }
        [HttpPost]
        public ActionResult Index(int? ID_provinceInactifs, int? ID_communeInactifs)
        {
            var donne = from a in db.associations
                        join c in db.collines
                        on a.ID_colline equals c.ID_colline
                        join z in db.zones
                        on c.ID_zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        join h in db.historique_asscociation
                        on a.ID_association equals h.ID_association
                        where p.ID_province == ID_provinceInactifs || co.ID_commune == ID_communeInactifs
                        select new recoltModel
                        {
                            ID = h.ID_histoAssoc,
                            assocition = a.NOM_association,
                            tel = a.TEL_association,
                            Date_insertion = (a.DATE_association).Value,
                            colline = c.NOM_colline,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province,
                            date=h.DATE_desactive.Value
                        };
            ViewData["association"] = donne.ToList();
            ViewBag.ID_provinceInactifs = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_communeInactifs = new SelectList(db.communes, "ID_commune", "NOM_commune");
            return View();

        }
        public ActionResult Details(int id = 0)
        {
            historique_asscociation historique_asscociation = db.historique_asscociation.Find(id);
            if (historique_asscociation == null)
            {
                return HttpNotFound();
            }
            return View(historique_asscociation);
        }

     
        public ActionResult Create()
        {
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View();
        }

        //
        // POST: /HistoriqueAssoction/Create

        [HttpPost]
        public ActionResult Create(historique_asscociation historique_asscociation)
        {
            if (ModelState.IsValid)
            {
                db.historique_asscociation.Add(historique_asscociation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", historique_asscociation.ID_association);
            return View(historique_asscociation);
        }

        //
        // GET: /HistoriqueAssoction/Edit/5

        public ActionResult Edit(int id = 0)
        {
            historique_asscociation historique_asscociation = db.historique_asscociation.Find(id);
            if (historique_asscociation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", historique_asscociation.ID_association);
            return View(historique_asscociation);
        }

        //
        // POST: /HistoriqueAssoction/Edit/5

        [HttpPost]
        public ActionResult Edit(historique_asscociation historique_asscociation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique_asscociation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", historique_asscociation.ID_association);
            return View(historique_asscociation);
        }

        //
        // GET: /HistoriqueAssoction/Delete/5

        public ActionResult Delete(int id = 0)
        {
            historique_asscociation historique_asscociation = db.historique_asscociation.Find(id);
            if (historique_asscociation == null)
            {
                return HttpNotFound();
            }
            return View(historique_asscociation);
        }

        //
        // POST: /HistoriqueAssoction/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            historique_asscociation historique_asscociation = db.historique_asscociation.Find(id);
            db.historique_asscociation.Remove(historique_asscociation);
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