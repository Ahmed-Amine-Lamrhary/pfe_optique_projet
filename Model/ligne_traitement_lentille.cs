//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MenuWithSubMenu.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ligne_traitement_lentille
    {
        public int idligne_traitement_lentille { get; set; }
        public int traitement_idTraitement { get; set; }
        public int lentille_idLentille { get; set; }
    
        public virtual lentille lentille { get; set; }
        public virtual traitement traitement { get; set; }
    }
}