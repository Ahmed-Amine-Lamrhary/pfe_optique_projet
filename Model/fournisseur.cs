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
    
    public partial class fournisseur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public fournisseur()
        {
            this.cmdfournisseurs = new HashSet<cmdfournisseur>();
        }
    
        public int idFournisseur { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cmdfournisseur> cmdfournisseurs { get; set; }
    }
}
