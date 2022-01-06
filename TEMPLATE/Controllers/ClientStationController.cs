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
//    public class ClientStationController : Controller
//    {
//        private RecolteEntities db = new RecolteEntities();
//        public ActionResult Dashboard()
//        {
//            return View();
//        }
//         public ActionResult Index()
//        {
//            var IDemploye = 0;
//            var IDstation = 0;
//            var nom = "";
//            var id = 0;
//            nom = "";
//            List<recoltModel> recolt = new List<recoltModel>();
//            if (Session["IDEmploye"] != null)
//            {
//                IDemploye = (int)Session["IDEmploye"];
//                var station = (from e in db.employe_station_lavage
//                               where e.ID_employ == IDemploye
//                               select new
//                               {
//                                   e.ID_station

//                               }).ToList();


//                foreach (var vp in station)
//                {
//                    id = vp.ID_station;
//                }
//                var Nomstation = (from s in db.station_lavage
//                                  where s.ID_station == id
//                                  select new
//                                  {
//                                      s.NOM_station

//                                  }).ToList();
//                foreach (var n in Nomstation)
//                {
//                    nom = n.NOM_station;
//                }
             
//                recolt = (from a in db.associations
//                          join c in db.clients
//                          on a.ID_association equals c.ID_association
//                          join r in db.recoltes 
//                          on  c.ID_client equals r.ID_client 
//                          join  s in db.station_lavage  on r.ID_station    equals  s.ID_station
//                          join  co in db.collines  on c.ID_colline equals co.ID_colline
//                          where s.ID_station == id
//                          select new recoltModel
//                          {   
//                              CNI=c.CNI,
//                              NOM_client = c.NOM_client,
//                              PRENOM_client = c.PRENOM_client,
//                              Tel_client=c.TEL_client,
//                              colline = co.NOM_colline,
//                              Nom_association=a.NOM_association,
//                              NOM_station = s.NOM_station , 
//                              ID_association = a.ID_association,
//                              ID_recolte = r.ID_recolte,
//                              ID_client = c.ID_client,
//                              ID_station = s.ID_station,
//                              Date_insertion =c.DATE_insertion.Value

//                          }).Distinct().OrderByDescending(s=>s.CNI).ToList();

//            }

//            else
//            {
//                IDemploye = (int)Session["IDEmploy"];
//                var station = (from e in db.employe_association
//                               where e.ID_employe == IDemploye
//                               select new
//                               {
//                                   e.ID_association

//                               }).ToList();

             
<<<<<<< HEAD
//                foreach (var vp in station)
//                {
//                    id = vp.ID_association;
//                }
//                recolt = (from a in db.associations
//                          join c in db.clients
//                          on a.ID_association equals c.ID_association
//                          join co in db.collines on c.ID_colline equals co.ID_colline
//                          where a.ID_association == id
//                          select new recoltModel
//                          {
//                              CNI = c.CNI,
//                              NOM_client = c.NOM_client,
//                              PRENOM_client = c.PRENOM_client,
//                              Tel_client = c.TEL_client,
//                              colline = co.NOM_colline,
//                              Nom_association = a.NOM_association,
//                              ID_association = a.ID_association,
//                              ID_client = c.ID_client,
//                              Date_insertion = c.DATE_insertion.Value

//                          }).Distinct().OrderBy(s => s.CNI).ToList();
//            }

//            ViewBag.IDassocition = IDemploye;
//            ViewBag.IDstation = IDstation;
//            ViewBag.nom = nom;
//            ViewBag.id = id;


//            var recoltes = db.recoltes.Include(r => r.client).Include(r => r.qualite).Include(r => r.station_lavage);
//            return View(recolt);
//        }
//        //
//        // GET: /Client/Details/5

//        public ActionResult Details(int id = 0)
//        {
//            client client = db.clients.Find(id);
//            if (client == null)
//            {
//                return HttpNotFound();
//            }
//            return View(client);
//        }

//        //
//        // GET: /Client/Create

//        public ActionResult Create()
//        {


//            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
//            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
//            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
//            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
//            return View();
//        }

//        //
//        // POST: /Client/Create

//        [HttpPost]
//        public ActionResult Create(client client)
//        {
//            if (ModelState.IsValid)
//            {
//                db.clients.Add(client);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", client.ID_association);
//            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", client.ID_colline);
//            return View(client);
//        }

//        //
//        // GET: /Client/Edit/5

//        public ActionResult Edit(int id = 0)
//        {
//            client client = db.clients.Find(id);
//            if (client == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
//            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
//            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
//            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
//            return View();
//        }

//        //
//        // POST: /Client/Edit/5

//        [HttpPost]
//        public ActionResult Edit(client client)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(client).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", client.ID_association);
//            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline", client.ID_colline);
//            return View(client);
//        }

//        //
//        // GET: /Client/Delete/5

//        public ActionResult Delete(int id = 0)
//        {
//            client client = db.clients.Find(id);
//            if (client == null)
//            {
//                return HttpNotFound();
//            }
//            return View(client);
//        }

//        //
//        // POST: /Client/Delete/5

//        [HttpPost, ActionName("Delete")]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            client client = db.clients.Find(id);
//            db.clients.Remove(client);
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
=======
                foreach (var vp in station)
                {
                    id = vp.ID_association;
                }
                recolt = (from a in db.associations
                          join c in db.clients
                          on a.ID_association equals c.ID_association
                          join co in db.collines on c.ID_colline equals co.ID_colline
                          where a.ID_association == id
                          select new recoltModel
                          {
                              CNI = c.CNI,
                              NOM_client = c.NOM_client,
                              PRENOM_client = c.PRENOM_client,
                              Tel_client = c.TEL_client,
                              colline = co.NOM_colline,
                              Nom_association = a.NOM_association,
                              ID_association = a.ID_association,
                              ID_client = c.ID_client,
                              Date_insertion = c.DATE_insertion.Value

                          }).Distinct().OrderBy(s => s.CNI).ToList();
            }

            ViewBag.IDassocition = IDemploye;
            ViewBag.IDstation = IDstation;
            ViewBag.nom = nom;
            ViewBag.id = id;


            var recoltes = db.recoltes.Include(r => r.client).Include(r => r.qualite).Include(r => r.station_lavage);
            return View(recolt);
        }
        public ActionResult Details(int id = 0)
        {
            client client = db.clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        public ActionResult Create()
        {
            ViewBag.ID_province = new SelectList(db.provinces, "ID_province", "NOM_province");
            ViewBag.ID_commune = new SelectList(db.communes, "ID_commune", "NOM_commune");
            ViewBag.ID_zone = new SelectList(db.zones, "ID_zone", "NOM_zone");
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.ID_colline = new SelectList(db.collines, "ID_colline", "NOM_colline");
            return View();
        }
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
>>>>>>> clientStation
