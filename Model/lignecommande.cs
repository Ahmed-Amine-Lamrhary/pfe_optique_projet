//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MenuWithSubMenu.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class lignecommande
    {
        public int idLigne { get; set; }
        public string idArticle { get; set; }
        public int idCmdFournisseur { get; set; }
        public System.DateTime Date_Commande { get; set; }
        public string Adresse_Commande { get; set; }
        public int Qte_Commande { get; set; }
        public float Prix_Total { get; set; }
        public string EtatCmd { get; set; }
        public Nullable<int> idVisite { get; set; }
    
        public virtual article article { get; set; }
        public virtual cmdfournisseur cmdfournisseur { get; set; }
        public virtual visite visite { get; set; }
    }
}
