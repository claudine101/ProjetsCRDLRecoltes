//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEMPLATE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class client
    {
        public client()
        {
            this.recoltes = new HashSet<recolte>();
        }
    
        public int ID_client { get; set; }
        public string CNI { get; set; }
        public string NOM_client { get; set; }
        public string PRENOM_client { get; set; }
        public string TEL_client { get; set; }
        public int ID_colline { get; set; }
        public int ID_association { get; set; }
        public Nullable<System.DateTime> DATE_insertion { get; set; }
    
        public virtual association association { get; set; }
        public virtual colline colline { get; set; }
        public virtual ICollection<recolte> recoltes { get; set; }
    }
}
