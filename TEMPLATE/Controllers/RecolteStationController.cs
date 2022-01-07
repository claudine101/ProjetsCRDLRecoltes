
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
    public class RecolteStationController : Controller
    {
        private RecolteEntities db = new RecolteEntities();
        public ActionResult Index()
        {



            RecolteEntities context = new RecolteEntities();
            if (Session["IDEmploye"] != null)
            {
                var IDemploye = (int)Session["IDEmploye"];
                var station = (from e in db.employe_station_lavage
                               where e.ID_employ == IDemploye
                               select new
                               {
                                   e.ID_station

                               }).ToList();

                var id = 0;
                foreach (var vp in station)
                {
                    id = vp.ID_station;
                }
                var NombreClient = from s in db.station_lavage
                                   join r in db.recoltes
                                   on s.ID_station equals r.ID_station
                                   join q in db.qualites
                                   on r.ID_qualite equals q.ID_qualite
                                   join c in db.clients
                                   on r.ID_client equals c.ID_client
                                   where s.ID_station == id
                                   select new
                                   {
                                       c.NOM_client,
                                       c.PRENOM_client,
                                       q.NOM_qualite,
                                       r.quantite
                                   } into x
                                   group x by new { x.NOM_qualite } into g
                                   select new recoltModel
                                   {
                                       name = g.Key.NOM_qualite,
                                       count = g.Select(x => x.quantite).Sum()

                                   };
                return View(NombreClient);

            }
            else
            {
                var IDemploye = (int)Session["IDEmploy"];
                var association = (from e in db.employe_association
                                   where e.ID_employe == IDemploye
                                   select new
                                   {
                                       e.ID_association

                                   }).ToList();

                var id = 0;
                foreach (var vp in association)
                {
                    id = vp.ID_association;
                }
                var NombreClient = from s in db.station_lavage
                                   join r in db.recoltes
                                   on s.ID_station equals r.ID_station
                                   join q in db.qualites
                                   on r.ID_qualite equals q.ID_qualite
                                   join c in db.clients
                                   on r.ID_client equals c.ID_client
                                   join a in db.associations on
                                   c.ID_association equals a.ID_association
                                   where a.ID_association == id
                                   select new { c.NOM_client, c.PRENOM_client, q.NOM_qualite, r.quantite } into x
                                   group x by new { x.NOM_qualite } into g
                                   select new recoltModel
                                   {
                                       name = g.Key.NOM_qualite,
                                       count = g.Select(x => x.quantite).Sum()

                                   };
                return View(NombreClient);
            }
        }
        

        public ActionResult Indexe()
        {
            var IDemploye = 0;
            var IDstation = 0;
            var nomStation = "";
            var Idstation = 0;
            List<recoltModel> recolt = new List<recoltModel>();
            if (Session["IDEmploye"] != null)
            {
                IDemploye = (int)Session["IDEmploye"];
                var station = (from e in db.employe_station_lavage
                               where e.ID_employ == IDemploye
                               select new
                               {
                                   e.ID_station

                               }).ToList();


                foreach (var vp in station)
                {
                    Idstation = vp.ID_station;
                }
                var IDSTATION = 0;
                var Nomstation = (from s in db.station_lavage
                                  where s.ID_station == Idstation
                                  select new
                                  {
                                      s.NOM_station,
                                      s.ID_station

                                  }).ToList();
                foreach (var n in Nomstation)
                {
                    nomStation = n.NOM_station;
                    IDSTATION = n.ID_station;
                }
                Session["association"] = "UNE STATION DE LAVAGE " + nomStation;
                Session["associat"] = nomStation;
                Session["associatID"] = IDSTATION;
                Session["station"] = "une station de lavage " + nomStation;
                recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          join e in db.employe_station_lavage
                          on f.ID_station equals e.ID_station

                          where f.ID_station == Idstation
                          select new
                          {
                              c.NOM_client,
                              c.PRENOM_client,
                              d.quantite,
                              q.NOM_qualite,
                              d.Prix,
                              f.NOM_station,
                              d.ID_recolte,
                              c.ID_client,
                              q.ID_qualite,
                              f.ID_station,
                              (d.Date_insertion).Value

                          } into x
                          group x by new
                          {
                              x.NOM_client,
                              x.PRENOM_client,
                              x.NOM_qualite,
                              x.ID_client,
                              x.ID_qualite,
                              x.Prix,
                              x.ID_station,
                          } into g
                          orderby g.Key.NOM_client
                          select new recoltModel
                          {
                              NOM_client = g.Key.NOM_client,
                              PRENOM_client = g.Key.PRENOM_client,
                              quantite = g.Select(x => x.quantite).Sum(),
                              NOM_qualite = g.Key.NOM_qualite,
                              ID_client = g.Key.ID_client,
                              ID_qualite = g.Key.ID_qualite,
                              Prix = g.Key.Prix,
                              ID_station = g.Key.ID_station,

                          }).ToList();

            }
            else
            {
                IDemploye = (int)Session["IDEmploy"];
                var association = (from e in db.employe_association
                                   where e.ID_employe == IDemploye
                                   select new
                                   {
                                       e.ID_association

                                   }).ToList();


                foreach (var vp in association)
                {
                    Idstation = vp.ID_association;
                }
                var Nomstation = (from s in db.associations
                                  where s.ID_association == Idstation
                                  select new
                                  {
                                      s.NOM_association

                                  }).ToList();
                foreach (var n in Nomstation)
                {
                    nomStation = n.NOM_association;
                }
                Session["association"] = "UNE ASSOCIATION " + nomStation;
                Session["associat"] = nomStation;
                Session["station"] = "une association " + nomStation;
                recolt = (from d in db.recoltes
                          join c in db.clients
                          on d.ID_client equals c.ID_client
                          join q in db.qualites
                          on d.ID_qualite equals q.ID_qualite
                          join f in db.station_lavage
                          on d.ID_station equals f.ID_station
                          join a in db.associations
                          on c.ID_association equals a.ID_association
                          join e in db.employe_association
                          on a.ID_association equals e.ID_association

                          where a.ID_association == Idstation
                          select new
                          {
                              c.NOM_client,
                              c.PRENOM_client,
                              d.quantite,
                              q.NOM_qualite,
                              d.Prix,
                              f.NOM_station,
                              d.ID_recolte,
                              c.ID_client,
                              q.ID_qualite,
                              f.ID_station,
                              d.Date_insertion

                          } into x
                          group x by new
                          {
                              x.NOM_client,
                              x.PRENOM_client,
                              x.NOM_qualite,
                              x.ID_client,
                              x.ID_qualite,
                              x.ID_station,
                              x.Prix,
                              x.NOM_station
                          } into g
                          orderby g.Key.NOM_client
                          select new recoltModel
                          {
                              NOM_client = g.Key.NOM_client,
                              PRENOM_client = g.Key.PRENOM_client,
                              quantite = g.Select(x => x.quantite).Sum(),
                              NOM_qualite = g.Key.NOM_qualite,
                              ID_client = g.Key.ID_client,
                              ID_qualite = g.Key.ID_qualite,
                              Prix = g.Key.Prix,
                              NOM_station = g.Key.NOM_station,
                              ID_station = g.Key.ID_station,

                          }).ToList();

            }


            ViewBag.IDassocition = IDemploye;
            ViewBag.IDstation = IDstation;

            ViewBag.id = IDemploye;


            var recoltes = db.recoltes.Include(r => r.client).Include(r => r.qualite).Include(r => r.station_lavage);
            return View(recolt);
        }
      
        //public ActionResult Detaill(int id = 0)
        //{
        //    var IDassocition = 0;
        //    var IDstation = 0;
        //    var nom = "";
        //    var ide = 0;
        //    nom = "";
        //    List<recoltModel> recolt = new List<recoltModel>();
        //    if (Session["IDEmploye"] != null)
        //    {
        //        IDassocition = (int)Session["IDEmploye"];
        //        var station = (from e in db.employe_station_lavage
        //                       where e.ID_employ == IDassocition
        //                       select new
        //                       {
        //                           e.ID_station

        //                       }).ToList();


        //        foreach (var vp in station)
        //        {
        //            ide = vp.ID_station;
        //        }
        //        var Nomstation = (from s in db.station_lavage
        //                          where s.ID_station == ide
        //                          select new
        //                          {
        //                              s.NOM_station

        //                          }).ToList();
        //        foreach (var n in Nomstation)
        //        {
        //            nom = n.NOM_station;
        //        }

        //        recolt = (from d in db.recoltes
        //                  join c in db.clients
        //                  on d.ID_client equals c.ID_client
        //                  join q in db.qualites
        //                  on d.ID_qualite equals q.ID_qualite
        //                  join f in db.station_lavage
        //                  on d.ID_station equals f.ID_station
        //                  join e in db.employe_station_lavage
        //                  on f.ID_station equals e.ID_station
        //                  //where f.ID_station == ide $$ c.ID_client==id
        //                  where f.ID_station == ide 
        //                  select new
        //                  {
        //                      c.NOM_client,
        //                      c.PRENOM_client,
        //                      d.quantite,
        //                      q.NOM_qualite,
        //                      d.Prix,
        //                      f.NOM_station,
        //                      d.ID_recolte,
        //                      c.ID_client,
        //                      q.ID_qualite,
        //                      f.ID_station,
        //                      (d.Date_insertion).Value

        //                  } into x
        //                  group x by new
        //                  {
        //                      x.NOM_client,
        //                      x.PRENOM_client,
        //                      x.NOM_qualite,
        //                      x.ID_client,
        //                      x.ID_qualite,
        //                      x.NOM_station,
        //                      x.Prix,
        //                      x.ID_recolte
        //                  } into g
        //                  orderby g.Key.NOM_client
        //                  select new recoltModel
        //                  {
        //                      NOM_client = g.Key.NOM_client,
        //                      PRENOM_client = g.Key.PRENOM_client,
        //                      quantite = g.Select(x => x.quantite).Sum(),
        //                      NOM_qualite = g.Key.NOM_qualite,
        //                      ID_client = g.Key.ID_client,
        //                      ID_qualite = g.Key.ID_qualite,
        //                      NOM_station = g.Key.NOM_station,
        //                      ID_recolte = g.Key.ID_recolte,
        //                      Prix = g.Key.Prix

        //                  }).ToList();
        //    recolte recolte = db.recoltes.Find(id);
        //    if (recolte == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(recolte);
        //}
        //}

        //
        // GET: /Recolte/Create

        public ActionResult Create()
        {
            Double PRIXSERIEA = 0;
            var SERIEA = (from s in db.historique_prix
                          where s.ID_qualite == 1
                          select new
                          {
                              s.PRIX,


                          }).ToList();
            foreach (var n in SERIEA)
            {
                PRIXSERIEA = n.PRIX;
            }
            Double PRIXSERIEB = 0;
            var SERIEB = (from s in db.historique_prix
                          where s.ID_qualite == 2
                          select new
                          {
                              s.PRIX,


                          }).ToList();
            foreach (var n in SERIEB)
            {
                PRIXSERIEB = n.PRIX;
            }
            ViewBag.prixSerieA = PRIXSERIEA;
            ViewBag.prixSerieB = PRIXSERIEB;
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI");
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite");
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station");
            return View();
        }

        [HttpPost]
        public ActionResult Create(recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.recoltes.Add(recolte);
                db.SaveChanges();
                return RedirectToAction("Indexe");
            }
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            return View(recolte);
        }

        public ActionResult Edit(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            Double PRIXSERIEA = 0;
            var SERIEA = (from s in db.historique_prix
                          where s.ID_qualite == 1
                          select new
                          {
                              s.PRIX,


                          }).ToList();
            foreach (var n in SERIEA)
            {
                PRIXSERIEA = n.PRIX;
            }
            Double PRIXSERIEB = 0;
            var SERIEB = (from s in db.historique_prix
                          where s.ID_qualite == 2
                          select new
                          {
                              s.PRIX,


                          }).ToList();
            foreach (var n in SERIEB)
            {
                PRIXSERIEB = n.PRIX;
            }
            ViewBag.prixSerieA = PRIXSERIEA;
            ViewBag.prixSerieB = PRIXSERIEB;
            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            return View(recolte);
        }

        [HttpPost]
        public ActionResult Edit(recolte recolte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recolte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Indexe");
            }
            ViewBag.ID_client = new SelectList(db.clients, "ID_client", "CNI", recolte.ID_client);
            ViewBag.ID_qualite = new SelectList(db.qualites, "ID_qualite", "NOM_qualite", recolte.ID_qualite);
            ViewBag.ID_station = new SelectList(db.station_lavage, "ID_station", "NOM_station", recolte.ID_station);
            return View(recolte);
        }

        
        public ActionResult Delete(int id = 0)
        {
            recolte recolte = db.recoltes.Find(id);
            if (recolte == null)
            {
                return HttpNotFound();
            }
            return View(recolte);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            recolte recolte = db.recoltes.Find(id);
            db.recoltes.Remove(recolte);
            db.SaveChanges();
            return RedirectToAction("Indexe");
        }


        public ActionResult Details(int idclient = 0, int idqualite = 0, int idstation = 0)
        {
            if (Session["IDEmploye"] != null)
            {
                var recolt = (from d in db.recoltes
                              join c in db.clients
                              on d.ID_client equals c.ID_client
                              join q in db.qualites
                              on d.ID_qualite equals q.ID_qualite
                              join f in db.station_lavage
                              on d.ID_station equals f.ID_station
                              where f.ID_station == idstation && c.ID_client == idclient && q.ID_qualite == idqualite
                              select new
                              {
                                  c.NOM_client,
                                  c.PRENOM_client,
                                  d.quantite,
                                  q.NOM_qualite,
                                  d.Prix,
                                  f.NOM_station,
                                  d.ID_recolte,
                                  c.ID_client,
                                  q.ID_qualite,
                                  f.ID_station,
                                  d.Date_insertion

                              } into x
                              select new recoltModel
                              {
                                  NOM_client = x.NOM_client,
                                  PRENOM_client = x.PRENOM_client,
                                  quantite = x.quantite,
                                  NOM_qualite = x.NOM_qualite,
                                  ID_client = x.ID_client,
                                  ID_qualite = x.ID_qualite,
                                  Prix = x.Prix,
                                  ID_station = x.ID_station,
                                  date = x.Date_insertion.Value,
                                  ID_recolte = x.ID_recolte,

                              }).ToList();

                return View(recolt);
            }
            else
            {
                var recolt = (from d in db.recoltes
                              join c in db.clients
                              on d.ID_client equals c.ID_client
                              join q in db.qualites
                              on d.ID_qualite equals q.ID_qualite
                              join f in db.station_lavage
                              on d.ID_station equals f.ID_station
                              where f.ID_station == idstation && c.ID_client == idclient && q.ID_qualite == idqualite
                              select new
                              {
                                  c.NOM_client,
                                  c.PRENOM_client,
                                  d.quantite,
                                  q.NOM_qualite,
                                  d.Prix,
                                  f.NOM_station,
                                  d.ID_recolte,
                                  c.ID_client,
                                  q.ID_qualite,
                                  f.ID_station,
                                  d.Date_insertion

                              } into x
                              select new recoltModel
                              {
                                  NOM_client = x.NOM_client,
                                  PRENOM_client = x.PRENOM_client,
                                  quantite = x.quantite,
                                  NOM_qualite = x.NOM_qualite,
                                  ID_client = x.ID_client,
                                  ID_qualite = x.ID_qualite,
                                  Prix = x.Prix,
                                  ID_station = x.ID_station,
                                  date = x.Date_insertion.Value,
                                  ID_recolte = x.ID_recolte,
                                  NOM_station = x.NOM_station

                              }).ToList();

                return View(recolt);
            }
        }
       
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        

        
        //POUR LE RAPPORT 
        public ActionResult RapportClient()
        {
            return View();
        }
        public ActionResult GetDataClient()
        {
            RecolteEntities context = new RecolteEntities();
            if (Session["IDEmploye"] != null)
            {
                var IDemploye = (int)Session["IDEmploye"];
                var station = (from e in db.employe_station_lavage
                               where e.ID_employ == IDemploye
                               select new
                               {
                                   e.ID_station

                               }).ToList();

                var id = 0;
                foreach (var vp in station)
                {
                    id = vp.ID_station;
                }
                var NombreClient1 = from s in db.station_lavage
                                    join r in db.recoltes
                                    on s.ID_station equals r.ID_station
                                    join q in db.qualites
                                    on r.ID_qualite equals q.ID_qualite
                                    join c in db.clients
                                    on r.ID_client equals c.ID_client
                                    where s.ID_station == id
                                    select new { c.NOM_client, c.PRENOM_client, r.quantite } into x
                                    group x by new { x.NOM_client, x.PRENOM_client } into g
                                    select new
                                    {
                                        name = g.Key.NOM_client + " " + g.Key.PRENOM_client,
                                        count = g.Select(x => x.quantite).Sum()

                                    };
                return Json(NombreClient1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var IDemploye = (int)Session["IDEmploy"];
                var association = (from e in db.employe_association
                                   where e.ID_employe == IDemploye
                                   select new
                                   {
                                       e.ID_association

                                   }).ToList();

                var id = 0;
                foreach (var vp in association)
                {
                    id = vp.ID_association;
                }
                var NombreClient1 = from s in db.station_lavage
                                    join r in db.recoltes
                                    on s.ID_station equals r.ID_station

                                    join c in db.clients
                                    on r.ID_client equals c.ID_client
                                    join a in db.associations on
                                    c.ID_association equals a.ID_association
                                    where a.ID_association == id
                                    select new { c.NOM_client, c.PRENOM_client, r.quantite } into x
                                    group x by new { x.NOM_client, x.PRENOM_client } into g
                                    select new
                                    {
                                        name = g.Key.NOM_client + " " + g.Key.PRENOM_client,
                                        count = g.Select(x => x.quantite).Sum()

                                    };
                return Json(NombreClient1, JsonRequestBehavior.AllowGet);
            }

        }
        

        public ActionResult RapportQualite()
        {
            return View();
        }
       

        public ActionResult GetDataQualite()
        {
            RecolteEntities context = new RecolteEntities();
            if (Session["IDEmploye"] != null)
            {
                var IDemploye = (int)Session["IDEmploye"];
                var station = (from e in db.employe_station_lavage
                               where e.ID_employ == IDemploye
                               select new
                               {
                                   e.ID_station

                               }).ToList();

                var id = 0;
                foreach (var vp in station)
                {
                    id = vp.ID_station;
                }
                var NombreClient = from s in db.station_lavage
                                   join r in db.recoltes
                                   on s.ID_station equals r.ID_station
                                   join q in db.qualites
                                   on r.ID_qualite equals q.ID_qualite
                                   join c in db.clients
                                   on r.ID_client equals c.ID_client
                                   where s.ID_station == id
                                   select new { c.NOM_client, c.PRENOM_client, q.NOM_qualite, r.quantite } into x
                                   group x by new { x.NOM_qualite } into g
                                   select new
                                   {
                                       name = g.Key.NOM_qualite,
                                       count = g.Select(x => x.quantite).Sum()

                                   };
                return Json(NombreClient, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var IDemploye = (int)Session["IDEmploy"];
                var association = (from e in db.employe_association
                                   where e.ID_employe == IDemploye
                                   select new
                                   {
                                       e.ID_association

                                   }).ToList();

                var id = 0;
                foreach (var vp in association)
                {
                    id = vp.ID_association;
                }
                var NombreClient = from s in db.station_lavage
                                   join r in db.recoltes
                                   on s.ID_station equals r.ID_station
                                   join q in db.qualites
                                   on r.ID_qualite equals q.ID_qualite
                                   join c in db.clients
                                   on r.ID_client equals c.ID_client
                                   join a in db.associations on
                                   c.ID_association equals a.ID_association
                                   where a.ID_association == id
                                   select new { c.NOM_client, c.PRENOM_client, q.NOM_qualite, r.quantite } into x
                                   group x by new { x.NOM_qualite } into g
                                   select new
                                   {
                                       name = g.Key.NOM_qualite,
                                       count = g.Select(x => x.quantite).Sum()

                                   };
                return Json(NombreClient, JsonRequestBehavior.AllowGet);
            }
        }
        //POUR CLIENT PAR ASSOCIATION
        public JsonResult getClient(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<client> client = db.clients.Where(x => x.ID_association == id).ToList();
            return Json(client, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getPrix(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<historique_prix> historique_prix = db.historique_prix.Where(x => x.ID_qualite == id).ToList();
            return Json(historique_prix, JsonRequestBehavior.AllowGet);
        }
        //POUR LA PRIX DE LA RECOLTE
        //public JsonResult getPrix(int ID_qualite)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    var product = db.historique_prix.Where(p => p.ID_qualite == ID_qualite).ToList().FirstOrDefault();
        //    return Json(product, JsonRequestBehavior.AllowGet);
        //}

    }
}