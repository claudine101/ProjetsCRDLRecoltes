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
    public class ProvinceController : Controller
    {
       
        private RecolteEntities db = new RecolteEntities();
        public ActionResult Index()
        {
            return View(db.provinces.ToList());
        }

       
        public ActionResult Details(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(province province)
        {
            if (ModelState.IsValid)
            {
                db.provinces.Add(province);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(province);
        }


        public ActionResult Edit(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();

            }
            return View(province);
        }

        //
        // POST: /Province/Edit/5

        [HttpPost]
        public ActionResult Edit(province province)
        {
            if (ModelState.IsValid)
            {
                db.Entry(province).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(province);
        }

        //
        // GET: /Province/Delete/5

        public ActionResult Delete(int id = 0)
        {
            province province = db.provinces.Find(id);
            if (province == null)
            {
                return HttpNotFound();
            }
            return View(province);
        }

        //
        // POST: /Province/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            province province = db.provinces.Find(id);
            db.provinces.Remove(province);
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