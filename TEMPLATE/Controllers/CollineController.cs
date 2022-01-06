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
    public class CollineController : Controller
    {
        private RecolteEntities db = new RecolteEntities();


        public ActionResult Index()
        {
            var collines = db.collines.Include(c => c.zone);
            return View(collines.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            colline colline = db.collines.Find(id);
            if (colline == null)
            {
                return HttpNotFound();
            }
            return View(colline);
        }



        public ActionResult Create()
        {
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            return View();
        }

    
        [HttpPost]
        public ActionResult Create(colline colline)
        {
            if (ModelState.IsValid)
            {
                db.collines.Add(colline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone", colline.ID_zone);
            return View(colline);
        }


        public ActionResult Edit(int id = 0)
        {
            colline colline = db.collines.Find(id);
            if (colline == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone", colline.ID_zone);
            return View(colline);
        }

        [HttpPost]
        public ActionResult Edit(colline colline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone", colline.ID_zone);
            return View(colline);
        }

        public ActionResult Delete(int id = 0)

        {
            colline colline = db.collines.Find(id);
            if (colline == null)
            {
                return HttpNotFound();
            }
            return View(colline);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            colline colline = db.collines.Find(id);
            db.collines.Remove(colline);
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