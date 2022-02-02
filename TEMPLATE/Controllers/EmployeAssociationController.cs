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
    public class EmployeAssociationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //
        // GET: /EmployeAssociation/

        public ActionResult Index()
        {
            var employe_associatione = from e in db.employe_association
                        join a in db.associations
                        on e.ID_association equals a.ID_association
                        
                        select new recoltModel
                        {
                            ID = e.ID_employe,
                            Nom_association=a.NOM_association,
                            NOM_employe=e.NOM_employe,
                            PRENOM_employe=e.PRENOM_employe,
                            Tel_employe= e.TEL_employe,
                            EMAIL_employe=e.EMAIL_employe,
                            CNI=e.CNI,
                            statut=e.Statut
                            
                        };
            var historique = from a in db.historique_utilisateur
                             join u in db.utilisateurs
                             on a.ID_utilisateur equals  u.ID_utilisateur 
                             select new recoltModel
                             {
                                 ID=a.ID_histoUtilisateur,
                                 id = u.ID_employ.Value,

                             };
            ViewData["emp_association"] = employe_associatione.ToList();
            
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");
            
            return View(employe_associatione.ToList());
        }
        [HttpPost]
        public ActionResult Index(int?association)
        {
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
            ViewBag.nomprovince = "qui travaille dans l'association  " + nomprovinc;
            ViewBag.nomprovinc = nomprovinc;
            var employe_associatione = from e in db.employe_association
                                       join a in db.associations
                                       on e.ID_association equals a.ID_association
                                       where e.ID_association == association
                                       select new recoltModel
                                       {
                                           ID = e.ID_employe,
                                           Nom_association = a.NOM_association,
                                           NOM_employe = e.NOM_employe,
                                           PRENOM_employe = e.PRENOM_employe,
                                           Tel_employe = e.TEL_employe,
                                           EMAIL_employe = e.EMAIL_employe,
                                           CNI = e.CNI,
                                           statut = e.Statut

                                       };
            var historique = from a in db.historique_utilisateur

                             select new recoltModel
                             {
                                 ID = a.ID_utilisateur,

                             };
            ViewData["emp_association"] = employe_associatione.ToList();
            ViewData["historique_emp"] = historique.ToList();
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(employe_associatione.ToList());
        }

        //LES EMPLOYES INACTIFS
        public ActionResult Indexe()
        {
            var employe_associatione = from e in db.employe_association
                                       join a in db.associations
                                       on e.ID_association equals a.ID_association

                                       select new recoltModel
                                       {
                                           ID = e.ID_employe,
                                           Nom_association = a.NOM_association,
                                           NOM_employe = e.NOM_employe,
                                           PRENOM_employe = e.PRENOM_employe,
                                           Tel_employe = e.TEL_employe,
                                           EMAIL_employe = e.EMAIL_employe,
                                           CNI = e.CNI,
                                           statut = e.Statut

                                       };
            var historique = from a in db.historique_utilisateur

                             select new recoltModel
                             {
                                 ID = a.ID_utilisateur,

                             };
            ViewData["emp_association"] = employe_associatione.ToList();
            ViewData["historique_emp"] = historique.ToList();
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");

            return View(employe_associatione.ToList());
        }
        [HttpPost]
        public ActionResult Indexe(int? association)
        {
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
            ViewBag.nomprovince = "qui travaille dans l'association  " + nomprovinc;
            ViewBag.nomprovinc = nomprovinc;
            var employe_associatione = from e in db.employe_association
                                       join a in db.associations
                                       on e.ID_association equals a.ID_association
                                       where e.ID_association == association
                                       select new recoltModel
                                       {
                                           ID = e.ID_employe,
                                           Nom_association = a.NOM_association,
                                           NOM_employe = e.NOM_employe,
                                           PRENOM_employe = e.PRENOM_employe,
                                           Tel_employe = e.TEL_employe,
                                           EMAIL_employe = e.EMAIL_employe,
                                           CNI = e.CNI,
                                           statut = e.Statut

                                       };
            var historique = from a in db.historique_utilisateur

                             select new recoltModel
                             {
                                 ID = a.ID_utilisateur,

                             };
            ViewData["emp_association"] = employe_associatione.ToList();
            ViewData["historique_emp"] = historique.ToList();
            ViewBag.association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(employe_associatione.ToList());
        }
        // GET: /EmployeAssociation/Details/5

        public ActionResult Details(int id = 0)
        {
            employe_association employe_association = db.employe_association.Find(id);
            if (employe_association == null)
            {
                return HttpNotFound();
            }
            return View(employe_association);
        }

        //
        // GET: /EmployeAssociation/Create

        public ActionResult Create()
        {
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            return View();
        }

        //
        // POST: /EmployeAssociation/Create

        [HttpPost]
        public ActionResult Create(employe_association employe_association)
        {
            if (ModelState.IsValid)
            {
                db.employe_association.Add(employe_association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
            return View(employe_association);
        }

        //
        // GET: /EmployeAssociation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            employe_association employe_association = db.employe_association.Find(id);
            if (employe_association == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
            return View(employe_association);
        }

        //
        // POST: /EmployeAssociation/Edit/5

        [HttpPost]
        public ActionResult Edit(employe_association employe_association)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe_association).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
            return View(employe_association);
        }

        //
        // GET: /EmployeAssociation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            employe_association employe_association = db.employe_association.Find(id);
            if (employe_association == null)
            {
                return HttpNotFound();
            }
            return View(employe_association);
        }

        //
        // POST: /EmployeAssociation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            employe_association employe_association = db.employe_association.Find(id);
            db.employe_association.Remove(employe_association);
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