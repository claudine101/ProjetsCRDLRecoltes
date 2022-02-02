using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEMPLATE.Models
{
    public class recoltModel
    {
        
        public string NOM_client{get;set;}
        public string PRENOM_client{get;set;}
        public double quantite{get;set;}
        public string NOM_qualite{get;set;}
        public double Prix { get; set; }
        public string NOM_station{get;set;}
        public int ID_recolte{get;set;}
        public int ID_client{get;set;}
        public int ID_qualite{get;set;}
        public int ID_station { get; set; }
        public int ID { get; set; }
        public DateTime Date_insertion { get; set; }
        public string assocition{ get; set; }
        public string  tel { get; set; }
        public DateTime date { get; set; }
        public string colline { get; set; }
        public string zone { get; set; }
        public string commune { get; set; }
        public string province { get; set; }
        public string CNI { get; set; }
        public string Nom_association{ get; set; }
        public int  ID_association{ get; set; }
        public string Tel_client { get; set; }
        public string  name { get; set; }
        public double count { get; set; }
        public int id { get; set; }
         //public string  Nom_association { get; set; }
          public string NOM_employe { get; set; }
           public string PRENOM_employe { get; set; }
            public string Tel_employe { get; set; }
             public string EMAIL_employe { get; set; }
              //public string CNI { get; set; }
               public string statut { get; set; }
                public string  username { get; set; }
                  public string password { get; set; }
    }
}