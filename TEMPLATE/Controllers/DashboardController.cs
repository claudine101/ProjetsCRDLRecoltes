using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TEMPLATE.Models;

namespace TEMPLATE.Controllers
{
    public class DashboardController : Controller
    {
        private RecolteEntities db = new RecolteEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(utilisateur user)
        {
            var v = from p in db.profiles
                    join u in db.utilisateurs
                        on p.ID_profile equals u.ID_profile
                    where (u.username == user.username && u.passwords == user.passwords)
                    select new
                    {
                        profile_name = p.NOM_profile,
                        username = u.username,
                        employeStation = u.ID_employ,
                        passord = u.passwords,
                        employeAssociation = u.ID_employe,
                        a = u.ID_profile
                    };

            var result = v.FirstOrDefault();
            if (result != null)
            {
                Session["username"] = result.username;
                Session["profile"] = result.profile_name;
                Session["IDEmploye"] = result.employeAssociation;
                Session["IDEmploy"] = result.employeStation;
                Session["association"] = "";
                ;
                if (result.profile_name == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                if (result.profile_name == "employe")
                {
                    return RedirectToAction("Dashboard", "ClientStation");
                }
            }

            return View(user);
        }
//        public ActionResult Logout()
//        {
//            Session.Abandon();

//            return RedirectToAction("Login", "Dashboard");
//        }
//        //public ActionResult Login(string email, string password)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var f_password = GetMD5(password);
//        //        var data = db.utilisateurs.Where(s => s.username.Equals(email) && s.passwords.Equals(password)).ToList();
//        //        if (data.Count() > 0)
//        //        {
//        //            //add session
//        //            Session["FullName"] = data.FirstOrDefault().username ;
//        //            //Session["Email"] = data.FirstOrDefault().Email;
//        //            Session["Idemploye"] = data.FirstOrDefault().ID_employe;
//        //            return RedirectToAction("Index");
//        //        }
//        //        else
//        //        {
//        //            ViewBag.error = "Login failed";
//        //            return RedirectToAction("Login");
//        //        }
//        //    }
//        //    return View();
//        //}

//        //create a string MD5
//        public static string GetMD5(string str)
//        {
//            MD5 md5 = new MD5CryptoServiceProvider();
//            byte[] fromData = Encoding.UTF8.GetBytes(str);
//            byte[] targetData = md5.ComputeHash(fromData);
//            string byte2String = null;

//            for (int i = 0; i < targetData.Length; i++)
//            {
//                byte2String += targetData[i].ToString("x2");

//            }
//            return byte2String;
//        }

//        //
//        // GET: /Dashbord/
//        public ActionResult GetData()
//        {
//            RecolteEntities context = new RecolteEntities();

//            //var query = context.Produits .GroupBy(p => p.produitName)
//            //       .Select(g => new { name = g.Key, count = g.Sum(w => w.PrixUnitaire) }).ToList();

//            //var list = context.recoltes.GroupBy(t => new { t.ID_client, t.ID_recolte })
//            //  .Select(i => new
//            //  {
//            //      name = i.Key.ID_client,
//            //      key = i.Key.ID_recolte,
//            //      count = i.Sum(w => w.quantite)
//            //  }).ToList();

//            var liste =
//    from re in db.recoltes
//    join S in db.station_lavage on re.ID_station equals S.ID_station
    
//    select new
//    {
//        name = S.NOM_station,
//        key = re.ID_recolte,
//        count = re.quantite
// };
////string query = "SELECT v.Name as Name, p.ListPrice as ListPrice, LEFT(v.Description, 20) as Description from SalesLT.vProductAndDescription v, SalesLT.Product p WHERE v.ProductID = p.ProductID";
////var viewModel = db.Database.SqlQuery<recolte>(query);



//            //string query = "SELECT sum(recolte.quantite) as no,recolte.ID_station as station  from  recolte group by(recolte.ID_station )";
//            //var list = db.Database.SqlQuery<recolte>(query);
//            //var result = from a in list.ToList()
//            //             join b in db.station_lavage on a.ID_station equals b.ID_station
//            //             select new
//            //             {
//            //                 name = b.NOM_station,
//            //                 count = a.quantite
//            //             };


//            var robotDogs = from d in db.recoltes
//                            join f in db.station_lavage
//                            on d.ID_station equals f.ID_station
//                            select new { f.NOM_station, f.ID_station, d.quantite } into x
//                            group x by new { x.NOM_station, x.ID_station} into g
//                            select new
//                            {
//                                name = g.Key.NOM_station,
//                                key = g.Key.ID_station,
//                                count = g.Select(x => x.quantite).Sum()
                                
//                            };



//            return Json(robotDogs, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult GetDataClient()
//        {
//            RecolteEntities context = new RecolteEntities();
//            var NombreClient = from a in db.associations
//                               join c in db.clients
//                               on a.ID_association equals c.ID_association
//                               select new { a.NOM_association, a.ID_association, c.ID_client } into x
//                               group x by new { x.NOM_association, x.ID_association } into g
//                               select new
//                               {
//                                   name = g.Key.NOM_association,
//                                   key = g.Key.ID_association,
//                                   count = g.Select(x => x.ID_client).Count()

//                               };
//            return Json(NombreClient, JsonRequestBehavior.AllowGet);
//        }
//        public ActionResult Index()
//        {
//            return View();
//        }
        
//        public ActionResult IndexAssociation()
//        {
//            var IDassocition = 0;
//            var IDstation = 0;
//            var nom = "";
//            var id = 0;
//            nom = "";
//            List<recoltModel> recolt = new List<recoltModel>();
//            if (Session["IDEmploye"] != null)
//            {
//                IDassocition = (int)Session["IDEmploye"];
//                var station = (from e in db.employe_station_lavage
//                                   where e.ID_employ == IDassocition
//                                   select new
//                                   {
//                                       e.ID_station

//                                   }).ToList();

                    
//                    foreach (var vp in station)
//                    {
//                        id = vp.ID_station;
//                    }
//                    var Nomstation = (from s in db.station_lavage
//                                      where s.ID_station == id
//                                       select new
//                                       {
//                                           s.NOM_station

//                                       }).ToList();
//                    foreach (var n in Nomstation)
//                    {
//                        nom = n.NOM_station;
//                    }
                    
//                    recolt = (from d in db.recoltes
//                              join c in db.clients
//                              on d.ID_client equals c.ID_client
//                              join q in db.qualites
//                              on d.ID_qualite equals q.ID_qualite
//                              join f in db.station_lavage
//                              on d.ID_station equals f.ID_station
//                              join e in db.employe_station_lavage
//                              on f.ID_station equals e.ID_station
//                              where f.ID_station == id
//                              select new recoltModel
//                              {
//                                  NOM_client = c.NOM_client,
//                                  PRENOM_client = c.PRENOM_client,
//                                  //quantite=(d.quantite),
//                                  NOM_qualite = q.NOM_qualite,
//                                  //Prix=(d.Prix).ToString(),
//                                  NOM_station = f.NOM_station,
//                                  ID_recolte = d.ID_recolte,
//                                  ID_client = c.ID_client,
//                                  ID_qualite = q.ID_qualite,
//                                  ID_station = f.ID_station,
//                                  Date_insertion = (d.Date_insertion).Value

//                              }).ToList();
                    
//                }
            
//            else
//            { IDstation = (int)Session["IDEmploy"];
//            var station = (from e in db.employe_association
//                           where e.ID_employe == IDstation
//                           select new
//                           {
//                               e.ID_association

//                           }).ToList();

//            foreach (var vp in station)
//            {
//                id = vp.ID_association;
//            }
//            recolt = (from 
//                       a in db.associations
//                       join c in db.clients
//                      on a.ID_association equals c.ID_association
//                      join e in db.employe_association
//                      on a.ID_association equals e.ID_association
//                      join r in db.recoltes 
//                      on c.ID_client equals r.ID_client
//                      join q in db.qualites
//                      on  r.ID_qualite equals q.ID_qualite 
//                      join s in db.station_lavage
//                      on r.ID_station equals s.ID_station
                      
//                      where e.ID_association == id
//                      select new recoltModel
//                      {
//                          NOM_client = c.NOM_client,
//                          PRENOM_client = c.PRENOM_client,
//                          quantite = (r.quantite),
//                          NOM_qualite = q.NOM_qualite,
//                          Prix = (r.Prix),
//                          NOM_station = a.NOM_association,
//                          ID_recolte = r.ID_recolte,
//                          ID_client = c.ID_client,
//                          ID_qualite = q.ID_qualite,
//                          ID_station = a.ID_association,
//                          Date_insertion = (r.Date_insertion).Value

//                      }).ToList();
                    
//            }

//            ViewBag.IDassocition = IDassocition;
//            ViewBag.IDstation = IDstation;
//            ViewBag.nom = nom;
//            ViewBag.id = id;

            
//           var recoltes = db.recoltes.Include(r => r.client).Include(r => r.qualite).Include(r => r.station_lavage);
//           return View(recolt);
//        }

//        //
//        // GET: /Dashbord/Details/5

//        public ActionResult Details(int id = 0)
//        {
//            employe_association employe_association = db.employe_association.Find(id);
//            if (employe_association == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employe_association);
//        }

//        //
//        // GET: /Dashbord/Create

//        public ActionResult Create()
//        {
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association");
//            return View();
//        }

//        //
//        // POST: /Dashbord/Create

//        [HttpPost]
//        public ActionResult Create(employe_association employe_association)
//        {
//            if (ModelState.IsValid)
//            {
//                db.employe_association.Add(employe_association);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
//            return View(employe_association);
//        }

//        //
//        // GET: /Dashbord/Edit/5

//        public ActionResult Edit(int id = 0)
//        {
//            employe_association employe_association = db.employe_association.Find(id);
//            if (employe_association == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
//            return View(employe_association);
//        }

//        //
//        // POST: /Dashbord/Edit/5

//        [HttpPost]
//        public ActionResult Edit(employe_association employe_association)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(employe_association).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.ID_association = new SelectList(db.associations, "ID_association", "NOM_association", employe_association.ID_association);
//            return View(employe_association);
//        }

//        //
//        // GET: /Dashbord/Delete/5

//        public ActionResult Delete(int id = 0)
//        {
//            employe_association employe_association = db.employe_association.Find(id);
//            if (employe_association == null)
//            {
//                return HttpNotFound();
//            }
//            return View(employe_association);
//        }

//        //
//        // POST: /Dashbord/Delete/5

//        [HttpPost, ActionName("Delete")]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            employe_association employe_association = db.employe_association.Find(id);
//            db.employe_association.Remove(employe_association);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            db.Dispose();
//            base.Dispose(disposing);
//        }
    }
}