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
    public class UtilisateurController : Controller
    {
        private RecolteEntities db = new RecolteEntities();
        public ActionResult Index()
        {
            var utilisateurs = db.utilisateurs.Include(u => u.employe_association).Include(u => u.employe_station_lavage).Include(u => u.profile);
            return View(utilisateurs.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            utilisateur utilisateur = db.utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }


        public ActionResult Create()
        {
            ViewBag.ID_employ = new SelectList(db.employe_association, "ID_employe", "PRENOM_employe ");
            ViewBag.ID_employe = new SelectList(db.employe_station_lavage, "ID_employ", "PRENOM_employe ");
            ViewBag.ID_profile = new SelectList(db.profiles, "ID_profile", "NOM_profile");
            return View();
        }
        [HttpPost]
        public ActionResult Create(utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.utilisateurs.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_employ = new SelectList(db.employe_association, "ID_employe", "CNI", utilisateur.ID_employ);
            ViewBag.ID_employe = new SelectList(db.employe_station_lavage, "ID_employ", "CNI", utilisateur.ID_employe);
            ViewBag.ID_profile = new SelectList(db.profiles, "ID_profile", "NOM_profile", utilisateur.ID_profile);
            return View(utilisateur);
        }

        public ActionResult Edit(int id = 0)
        {
            utilisateur utilisateur = db.utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_employ = new SelectList(db.employe_association, "ID_employe", "PRENOM_employe", utilisateur.ID_employ);
            ViewBag.ID_employe = new SelectList(db.employe_station_lavage, "ID_employ", "PRENOM_employe", utilisateur.ID_employe);
            ViewBag.ID_profile = new SelectList(db.profiles, "ID_profile", "NOM_profile", utilisateur.ID_profile);
            return View(utilisateur);
        }
        [HttpPost]
        public ActionResult Edit(utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_employ = new SelectList(db.employe_association, "ID_employe", "CNI", utilisateur.ID_employ);
            ViewBag.ID_employe = new SelectList(db.employe_station_lavage, "ID_employ", "CNI", utilisateur.ID_employe);
            ViewBag.ID_profile = new SelectList(db.profiles, "ID_profile", "NOM_profile", utilisateur.ID_profile);
            return View(utilisateur);
        }


        public ActionResult Delete(int id = 0)
        {
            utilisateur utilisateur = db.utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            utilisateur utilisateur = db.utilisateurs.Find(id);
            db.utilisateurs.Remove(utilisateur);
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