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
    public class HistoriqueUtilisateurController : Controller
    {
        private RecolteEntities db = new RecolteEntities();
        public ActionResult Index()
        {
            var historique_utilisateur = db.historique_utilisateur.Include(h => h.utilisateur);
            return View(historique_utilisateur.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            if (historique_utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(historique_utilisateur);
        }

        public ActionResult Create()
        {
            ViewBag.ID_utilisateur = new SelectList(db.utilisateurs, "ID_utilisateur", "username");
            return View();
        }

        //
        // POST: /HistoriqueUtilisateur/Create

        [HttpPost]
        public ActionResult Create(historique_utilisateur historique_utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.historique_utilisateur.Add(historique_utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_utilisateur = new SelectList(db.utilisateurs, "ID_utilisateur", "username", historique_utilisateur.ID_utilisateur);
            return View(historique_utilisateur);
        }

        //
        // GET: /HistoriqueUtilisateur/Edit/5

        public ActionResult Edit(int id = 0)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            if (historique_utilisateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_utilisateur = new SelectList(db.utilisateurs, "ID_utilisateur", "username", historique_utilisateur.ID_utilisateur);
            return View(historique_utilisateur);
        }

        //
        // POST: /HistoriqueUtilisateur/Edit/5

        [HttpPost]
        public ActionResult Edit(historique_utilisateur historique_utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historique_utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_utilisateur = new SelectList(db.utilisateurs, "ID_utilisateur", "username", historique_utilisateur.ID_utilisateur);
            return View(historique_utilisateur);
        }

        //
        // GET: /HistoriqueUtilisateur/Delete/5

        public ActionResult Delete(int id = 0)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            if (historique_utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(historique_utilisateur);
        }

        //
        // POST: /HistoriqueUtilisateur/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            db.historique_utilisateur.Remove(historique_utilisateur);
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