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
    
    public partial class lentilletorique
    {
        public int idLentilleToriques { get; set; }
        public int idTypeLentille { get; set; }
        public Nullable<float> CYL { get; set; }
        public Nullable<float> AXE { get; set; }
    
        public virtual ligne_type_lentille ligne_type_lentille { get; set; }
    }
}
