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
            var employe_associatione = from e in db.employe_association
                                       join u in db.utilisateurs
                                       on e.ID_employe equals u.ID_employ
                                       join a in db.associations
                                       on e.ID_association equals a.ID_association
                                       join h in db.historique_utilisateur
                                       on u.ID_utilisateur equals h.ID_utilisateur
                                       select new recoltModel
                                       {
                                           ID = u.ID_utilisateur,
                                           id = h.ID_histoUtilisateur,
                                           Nom_association = a.NOM_association,
                                           NOM_employe = e.NOM_employe,
                                           PRENOM_employe = e.PRENOM_employe,
                                           Tel_employe = e.TEL_employe,
                                           EMAIL_employe = e.EMAIL_employe,
                                           CNI = e.CNI,
                                           statut = e.Statut,
                                           username = u.username,
                                           password = u.passwords,
                                           date = h.DATE_desactive.Value

                                       };
            var employe_station = from e in db.employe_station_lavage
                                  join u in db.utilisateurs
                                       on e.ID_employ equals u.ID_employe
                                       join a in db.station_lavage
                                       on e.ID_station equals a.ID_station
                                  join h in db.historique_utilisateur
                                  on u.ID_utilisateur equals h.ID_utilisateur
                                       select new recoltModel
                                       {
                                           ID = u.ID_utilisateur,
                                           id = h.ID_histoUtilisateur,
                                           Nom_association = a.NOM_station,
                                           NOM_employe = e.NOM_employe,
                                           PRENOM_employe = e.PRENOM_employe,
                                           Tel_employe = e.TEL_employe,
                                           EMAIL_employe = e.EMAIL_employe,
                                           CNI = e.CNI,
                                           statut = e.Statut,
                                           username=u.username,
                                           password=u.passwords,
                                           date=h.DATE_desactive.Value

                                       };
            var historique = from a in db.historique_utilisateur

                             select new recoltModel
                             {
                                 ID = a.ID_utilisateur,

                             };
            ViewData["emp_association"] = employe_associatione.ToList();
            ViewData["emp_station"] = employe_station.ToList();
            ViewData["historique_emp"] = historique.ToList();
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            return View(employe_associatione.ToList());
        }
        [HttpPost]
        public ActionResult Index(int? association, int? station)
        {

            
            if (association == null)
            {

                var employe_associatione = from e in db.employe_association
                                           join u in db.utilisateurs
                                           on e.ID_employe equals u.ID_employ
                                           join a in db.associations
                                           on e.ID_association equals a.ID_association
                                           join h in db.historique_utilisateur
                                           on u.ID_utilisateur equals h.ID_utilisateur
                                           
                                           select new recoltModel
                                           {
                                               ID = u.ID_utilisateur,
                                               Nom_association = a.NOM_association,
                                               NOM_employe = e.NOM_employe,
                                               PRENOM_employe = e.PRENOM_employe,
                                               Tel_employe = e.TEL_employe,
                                               EMAIL_employe = e.EMAIL_employe,
                                               CNI = e.CNI,
                                               statut = e.Statut,
                                               username = u.username,
                                               password = u.passwords,
                                               date = h.DATE_desactive.Value

                                           };
                var employe_station = from e in db.employe_station_lavage
                                      join u in db.utilisateurs
                                           on e.ID_employ equals u.ID_employe
                                      join a in db.station_lavage
                                      on e.ID_station equals a.ID_station
                                      join h in db.historique_utilisateur
                                      on u.ID_utilisateur equals h.ID_utilisateur
                                      where a.ID_station == station
                                      select new recoltModel
                                      {
                                          ID = u.ID_utilisateur,

                                          Nom_association = a.NOM_station,
                                          NOM_employe = e.NOM_employe,
                                          PRENOM_employe = e.PRENOM_employe,
                                          Tel_employe = e.TEL_employe,
                                          EMAIL_employe = e.EMAIL_employe,
                                          CNI = e.CNI,
                                          statut = e.Statut,
                                          username = u.username,
                                          password = u.passwords,
                                          date = h.DATE_desactive.Value

                                      };
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
                ViewBag.AS = "Les employes inactifs de station de lavage " + nomprovinc;
                ViewData["emp_association"] = employe_associatione.ToList();
                ViewData["emp_station"] = employe_station.ToList();
               
            }
            else
            {
                var employe_associatione = from e in db.employe_association
                                           join u in db.utilisateurs
                                           on e.ID_employe equals u.ID_employ
                                           join a in db.associations
                                           on e.ID_association equals a.ID_association
                                           join h in db.historique_utilisateur
                                           on u.ID_utilisateur equals h.ID_utilisateur
                                           where a.ID_association == association
                                           select new recoltModel
                                           {
                                               ID = u.ID_utilisateur,
                                               Nom_association = a.NOM_association,
                                               NOM_employe = e.NOM_employe,
                                               PRENOM_employe = e.PRENOM_employe,
                                               Tel_employe = e.TEL_employe,
                                               EMAIL_employe = e.EMAIL_employe,
                                               CNI = e.CNI,
                                               statut = e.Statut,
                                               username = u.username,
                                               password = u.passwords,
                                               date = h.DATE_desactive.Value

                                           };
                var employe_station = from e in db.employe_station_lavage
                                      join u in db.utilisateurs
                                           on e.ID_employ equals u.ID_employe
                                      join a in db.station_lavage
                                      on e.ID_station equals a.ID_station
                                      join h in db.historique_utilisateur
                                      on u.ID_utilisateur equals h.ID_utilisateur
                                    
                                      select new recoltModel
                                      {
                                          ID = u.ID_utilisateur,

                                          Nom_association = a.NOM_station,
                                          NOM_employe = e.NOM_employe,
                                          PRENOM_employe = e.PRENOM_employe,
                                          Tel_employe = e.TEL_employe,
                                          EMAIL_employe = e.EMAIL_employe,
                                          CNI = e.CNI,
                                          statut = e.Statut,
                                          username = u.username,
                                          password = u.passwords,
                                          date = h.DATE_desactive.Value

                                      };
                var province = (from p in db.associations
                                where p.ID_association == association
                                select new
                                {

                                    station = p.NOM_association
                                }).ToList();
                var nomprovinc = "";
                foreach (var vp in province)
                {
                    nomprovinc = vp.station;
                }
                ViewBag.AS1 = "Les employes inactifs d'association " + nomprovinc;
                ViewData["emp_association"] = employe_associatione.ToList();
                ViewData["emp_station"] = employe_station.ToList();
               
            }
             
           
            var historique = from a in db.historique_utilisateur

                             select new recoltModel
                             {
                                 ID = a.ID_utilisateur,

                             };
             
           
            ViewData["historique_emp"] = historique.ToList();
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.station = new SelectList(db.station_lavage, "ID_station", "NOM_station");

            return View();
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
            ViewBag.ID_statio = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            ViewBag.ID_associatio = new SelectList(db.associations, "ID_association", "NOM_association");
            return View();
        }
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
        public ActionResult Delete(int id = 0)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            if (historique_utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(historique_utilisateur);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            historique_utilisateur historique_utilisateur = db.historique_utilisateur.Find(id);
            db.historique_utilisateur.Remove(historique_utilisateur);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //POUR CLIENT PAR ASSOCIATION
      
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}