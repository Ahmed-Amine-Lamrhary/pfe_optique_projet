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
    
    public partial class lentilletorique
    {
        public int idLentilleToriques { get; set; }
        public int idTypeLentille { get; set; }
        public Nullable<float> CYL { get; set; }
        public Nullable<float> AXE { get; set; }
    
        public virtual typelentille typelentille { get; set; }
    }
}