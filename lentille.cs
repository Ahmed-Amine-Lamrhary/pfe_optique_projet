//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MenuWithSubMenu
{
    using System;
    using System.Collections.Generic;
    
    public partial class lentille
    {
        public int idLentille { get; set; }
        public string idArticle { get; set; }
        public int idTypeLentille { get; set; }
        public Nullable<int> idOeil { get; set; }
        public string Couleur { get; set; }
    
        public virtual article article { get; set; }
        public virtual oeil oeil { get; set; }
        public virtual typelentille typelentille { get; set; }
    }
}
