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
    public class ProfilesController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //
        // GET: /Profiles/

        public ActionResult Index()
        {
            return View(db.profiles.ToList());
        }

        //
        // GET: /Profiles/Details/5

        public ActionResult Details(int id = 0)
        {
            profile profile = db.profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // GET: /Profiles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Profiles/Create

        [HttpPost]
        public ActionResult Create(profile profile)
        {
            if (ModelState.IsValid)
            {
                db.profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        //
        // GET: /Profiles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            profile profile = db.profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profiles/Edit/5

        [HttpPost]
        public ActionResult Edit(profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        //
        // GET: /Profiles/Delete/5

        public ActionResult Delete(int id = 0)
        {
            profile profile = db.profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profiles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            profile profile = db.profiles.Find(id);
            db.profiles.Remove(profile);
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