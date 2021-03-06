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

      
        public ActionResult Index()
        {

            var donne = from c in db.clients
                        join a in db.associations
                        on c.ID_association equals a.ID_association
                        join co in db.collines
                        on c.ID_client equals co.ID_colline
                        select new recoltModel
                        {
                            ID = c.ID_client,
                            CNI=c.CNI,
                            NOM_client=c.NOM_client,
                            PRENOM_client=c.PRENOM_client,
                            assocition = a.NOM_association,
                            tel = c.TEL_client,
                            date = (c.DATE_insertion).Value,
                            colline = co.NOM_colline,
                        };
            ViewBag.ID_associationClients = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(donne.ToList());

        }
        [HttpPost]
        public ActionResult Index(int?ID_associationClients)
        {
            var assoc = (from a in db.associations
                            where a.ID_association == ID_associationClients
                            select new
                            {

                                association = a.NOM_association
                            }).ToList();
            var associationse = "";
            foreach (var vp in assoc)
            {
                associationse = vp.association;
            }
            ViewBag.associ = "qui se trouve dans l'association " + associationse;
            var donne = from c in db.clients
                        join a in db.associations
                        on c.ID_association equals a.ID_association
                        join co in db.collines
                        on c.ID_client equals co.ID_colline
                        where a.ID_association == ID_associationClients
                        select new recoltModel
                        {
                            ID = c.ID_client,
                            CNI = c.CNI,
                            NOM_client = c.NOM_client,
                            PRENOM_client = c.PRENOM_client,
                            assocition = a.NOM_association,
                            tel = c.TEL_client,
                            date = (c.DATE_insertion).Value,
                            colline = co.NOM_colline,
                        };
            ViewBag.ID_associationClients = new SelectList(db.associations, "ID_association", "NOM_association");
            return View(donne.ToList());

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

        //
        // POST: /Client/Delete/5

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