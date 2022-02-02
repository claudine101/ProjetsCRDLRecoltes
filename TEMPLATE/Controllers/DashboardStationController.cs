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
    public class DashboardAssociationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //
        // GET: /DashboardAssociation/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /DashboardAssociation/Details/5

        public ActionResult Details(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        //
        // GET: /DashboardAssociation/Create

        public ActionResult Create()
        {
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }

        //
        // POST: /DashboardAssociation/Create

        [HttpPost]
        public ActionResult Create(association association)
        {
            if (ModelState.IsValid)
            {
                db.associations.Add(association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", association.ID_colline);
            return View(association);
        }

        //
        // GET: /DashboardAssociation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", association.ID_colline);
            return View(association);
        }

        //
        // POST: /DashboardAssociation/Edit/5

        [HttpPost]
        public ActionResult Edit(association association)
        {
            if (ModelState.IsValid)
            {
                db.Entry(association).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", association.ID_colline);
            return View(association);
        }

        //
        // GET: /DashboardAssociation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        //
        // POST: /DashboardAssociation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            association association = db.associations.Find(id);
            db.associations.Remove(association);
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