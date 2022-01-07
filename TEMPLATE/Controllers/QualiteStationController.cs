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
    public class QualiteStationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        public ActionResult Index()
        {
            return View(db.qualites.ToList());
        }
        public ActionResult Details(int id = 0)
        {
            qualite qualite = db.qualites.Find(id);
            if (qualite == null)
            {
                return HttpNotFound();
            }
            return View(qualite);
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /QualiteStation/Create

        [HttpPost]
        public ActionResult Create(qualite qualite)
        {
            if (ModelState.IsValid)
            {
                db.qualites.Add(qualite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualite);
        }

        //
        // GET: /QualiteStation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            qualite qualite = db.qualites.Find(id);
            if (qualite == null)
            {
                return HttpNotFound();
            }
            return View(qualite);
        }

        //
        // POST: /QualiteStation/Edit/5

        [HttpPost]
        public ActionResult Edit(qualite qualite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qualite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qualite);
        }

        //
        // GET: /QualiteStation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            qualite qualite = db.qualites.Find(id);
            if (qualite == null)
            {
                return HttpNotFound();
            }
            return View(qualite);
        }

        //
        // POST: /QualiteStation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            qualite qualite = db.qualites.Find(id);
            db.qualites.Remove(qualite);
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