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
    
    public partial class cmdclient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cmdclient()
        {
            this.ligneentrees = new HashSet<ligneentree>();
        }
    
        public string idCmdClient { get; set; }
        public System.DateTime DateCmd { get; set; }
        public string cin { get; set; }
    
        public virtual client client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ligneentree> ligneentrees { get; set; }
    }
}
