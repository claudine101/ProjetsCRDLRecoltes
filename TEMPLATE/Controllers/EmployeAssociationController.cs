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

        public ActionResult Index()
        {
            var employe_association = db.employe_association.Include(e => e.association);
            return View(employe_association.ToList());
        }


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