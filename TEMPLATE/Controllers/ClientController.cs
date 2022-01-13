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
    public class ClientController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //
        // GET: /Client/

        public ActionResult Index()
        {
            var clients = db.clients.Include(a => a.association).Include(c => c.colline);
            return View(clients.ToList());

            
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(int id = 0)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        //
        // GET: /Client/Create

        public ActionResult Create()
        {


            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }

        //
        // POST: /Client/Create

        [HttpPost]
        public ActionResult Create(client client)
        {
            if (ModelState.IsValid)
            {
                db.clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", client.ID_association);
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", client.ID_colline);
            return View(client);
        }

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(int id = 0)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", client.ID_association);
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", client.ID_colline);
            return View(client);
        }

        //
        // GET: /Client/Delete/5

        public ActionResult Delete(int id = 0)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

   


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            client client = db.clients.Find(id);
            db.clients.Remove(client);
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