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
    
    public partial class ordonnance
    {
        public int id { get; set; }
        public System.DateTime dateCreation { get; set; }
        public System.DateTime dateExpiration { get; set; }
        public string typeVerres { get; set; }
        public string notes { get; set; }
        public string photo { get; set; }
        public int ophtalmologue_ophtalmologueId { get; set; }
        public string client_cin { get; set; }
        public int vision_pres_id { get; set; }
        public int vision_loins_id { get; set; }
    
        public virtual client client { get; set; }
        public virtual ophtalmologue ophtalmologue { get; set; }
        public virtual vision_loins vision_loins { get; set; }
        public virtual vision_pres vision_pres { get; set; }
    }
}
