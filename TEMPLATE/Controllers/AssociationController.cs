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
    public class AssociationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();

        //
        // GET: /Association/
        public ActionResult RapportQuantite()
        {
            return View();
        }
        public ActionResult RapportClient()
        {
            return View();
        }
        public ActionResult GetDataClient()
        {
            RecolteEntities context = new RecolteEntities();
            var NombreClient = from a in db.associations
                               join c in db.clients
                               on a.ID_association equals c.ID_association
                               select new { a.NOM_association, a.ID_association, c.ID_client } into x
                               group x by new { x.NOM_association, x.ID_association } into g
                               select new
                               {
                                   name = g.Key.NOM_association,
                                   key = g.Key.ID_association,
                                   count = g.Select(x => x.ID_client).Count()

                               };
            return Json(NombreClient, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {

            //var associations = db.associations.Include(a => a.colline);
            //return View(associations.ToList());
            //var donne = from a in db.associations
            //            join c in db.collines
            //            on a.ID_colline equals c.ID_colline
            //            join z in db.zones
            //            on c.ID_zone equals z.ID_zone
            //            join co in db.communes
            //            on z.ID_commune equals co.ID_commune
            //            join p in db.provinces
            //            on co.ID_province equals p.ID_province
            //            select new recoltModel
            //            {
            //                ID=a.ID_association,
            //                assocition = a.NOM_association,
            //                tel = a.TEL_association,
            //                date = (a.DATE_association).Value,
            //                colline = c.NOM_colline,
            //                zone = z.NOM_zone,
            //                commune = co.NOM_commune,
            //                province = p.NOM_province
            //            };
            string query = "select * from  association a join collines c"
                           + "on a.ID_colline=c.ID_colline"
                            + "join zones	z ON z.ID_zone=c.ID_zone"
                            + "JOIN communes co on co.ID_commune=z.ID_commune"
                            + " join provinces p on p.ID_province=co.ID_province ";
            var queryData = db.Database.SqlQuery<associations>(query);
            var association = db.associations.Include(a => a.colline);
            var historique = db.historique_asscociation;
            ViewData["association"] = association.ToList();
            ViewData["historique"] = historique.ToList();
            ViewBag.ID_provinc = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commun = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }
        //POUR ASSOCITION INACTIF
        public ActionResult Indexe()
        {
            var association = db.associations.Include(a => a.colline);
            var historique = db.historique_asscociation;
            ViewData["association"] = association.ToList();
            ViewData["historique"] = historique.ToList();
            return View();
        }
        //POUR RAPPORT 
        public ActionResult GetData()
        {
            RecolteEntities context = new RecolteEntities();

            //var query = context.Produits .GroupBy(p => p.produitName)
            //       .Select(g => new { name = g.Key, count = g.Sum(w => w.PrixUnitaire) }).ToList();

            //var list = context.clients.GroupBy(t => new { t.NOM_client, t.ID_client })
            //  .Select(i => new
            //  {
            //      name = i.Key.NOM_client,
            //      count = i.Sum(w => w.ID_client)
            //  }).ToList();

            var quantite = from a in db.associations
                           join c in db.clients
                           on a.ID_association equals c.ID_association
                           join r in db.recoltes
                           on c.ID_client equals r.ID_client
                           select new { a.NOM_association, a.ID_association, r.quantite } into x
                           group x by new { x.NOM_association, x.ID_association } into g
                           select new
                           {
                               name = g.Key.NOM_association,
                               key = g.Key.ID_association,
                               count = g.Select(x => x.quantite).Sum()

                           };

            return Json(quantite, JsonRequestBehavior.AllowGet);


        }
        //POUR RAPORT DES ASSOCIATION PARS LEURS CLIENTS


        //pour affichage par province
        public ActionResult GetDataAffiche()
        {
            RecolteEntities context = new RecolteEntities();

            var donne = from a in db.associations
                        join c in db.collines
                        on a.ID_colline equals c.ID_colline
                        join z in db.zones
                        on c.ID_zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province


                        select new
                        {
                            assocition = a.NOM_association,
                            colline = c.NOM_colline,
                            zone = z.NOM_zone,
                            commune = co.NOM_commune,
                            province = p.NOM_province
                        }

                            ;
            return Json(donne, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Details(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }
        public ActionResult Create()
        {
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }
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
        public ActionResult Edit(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View(association);
        }
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
        public ActionResult Delete(int id = 0)
        {
            association association = db.associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }
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
        public JsonResult getCommune(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<commune> commune = db.communes.Where(x => x.ID_province == id).ToList();
            return Json(commune, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getZone(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<zone> zone = db.zones.Where(x => x.ID_commune == id).ToList();
            return Json(zone, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getColline(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<colline> colline = db.collines.Where(x => x.ID_zone == id).ToList();
            return Json(colline, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDonne(int id)
        {

            db.Configuration.ProxyCreationEnabled = false;

            var donne = from a in db.associations
                        join c in db.collines
                        on a.ID_colline equals c.ID_colline
                        join z in db.zones
                        on c.ID_zone equals z.ID_zone
                        join co in db.communes
                        on z.ID_commune equals co.ID_commune
                        join p in db.provinces
                        on co.ID_province equals p.ID_province
                        where co.ID_province == id
                        select new
                        {
                            assocition = a.NOM_association,
                            tel = a.TEL_association,
                            Date = a.DATE_association.Value,
                            colline = c.NOM_colline,
                        };
            return Json(donne.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}