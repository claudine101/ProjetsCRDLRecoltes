using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEMPLATE.Models
{
    public class associations 
    {
        public int ID_client { get; set; }
        public DateTime Date_insertion { get; set; }
        public string CNI { get; set; }
        public string NOM_client { get; set; }
        public DateTime DATE_insertion { get; set; }
        public string TEL_client { get; set; }
        public string NOM_colline { get; set; }
        public string NOM_association { get; set; }
        public string PRENOM_client { get; set; }

    }
}
