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
    
    public partial class vision_detaille
    {
        public int idvision_detaille { get; set; }
        public string remarques { get; set; }
        public int vision_id { get; set; }
        public int visite_id { get; set; }
    
        public virtual vision vision { get; set; }
        public virtual visite visite { get; set; }
    }
}
