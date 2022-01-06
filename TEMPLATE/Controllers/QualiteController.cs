//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using TEMPLATE.Models;

//namespace TEMPLATE.Controllers
//{
//    public class QualiteController : Controller
//    {
//        private RecolteEntities db = new RecolteEntities();

//        //
//        // GET: /Qualite/

//        public ActionResult Index()
//        {
//            return View(db.qualites.ToList());
//        }

//        //
//        // GET: /Qualite/Details/5

//        public ActionResult Details(int id = 0)
//        {
//            qualite qualite = db.qualites.Find(id);
//            if (qualite == null)
//            {
//                return HttpNotFound();
//            }
//            return View(qualite);
//        }

//        //
//        // GET: /Qualite/Create

//        public ActionResult Create()
//        {
//            return View();
//        }

//        //
//        // POST: /Qualite/Create

//        [HttpPost]
//        public ActionResult Create(qualite qualite)
//        {
//            if (ModelState.IsValid)
//            {
//                db.qualites.Add(qualite);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(qualite);
//        }

//        //
//        // GET: /Qualite/Edit/5

//        public ActionResult Edit(int id = 0)
//        {
//            qualite qualite = db.qualites.Find(id);
//            if (qualite == null)
//            {
//                return HttpNotFound();
//            }
//            return View(qualite);
//        }

//        //
//        // POST: /Qualite/Edit/5

//        [HttpPost]
//        public ActionResult Edit(qualite qualite)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(qualite).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(qualite);
//        }

//        //
//        // GET: /Qualite/Delete/5

//        public ActionResult Delete(int id = 0)
//        {
//            qualite qualite = db.qualites.Find(id);
//            if (qualite == null)
//            {
//                return HttpNotFound();
//            }
//            return View(qualite);
//        }

//        //
//        // POST: /Qualite/Delete/5

//        [HttpPost, ActionName("Delete")]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            qualite qualite = db.qualites.Find(id);
//            db.qualites.Remove(qualite);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
//    }
//}