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
        public ActionResult Index()
        {
            var historique_asscociation = db.historique_asscociation.Include(h => h.association);
            return View(historique_asscociation.ToList());
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