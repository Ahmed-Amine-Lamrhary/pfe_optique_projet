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
    
    public partial class cmdfournisseur
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cmdfournisseur()
        {
            this.lignecommandes = new HashSet<lignecommande>();
        }
    
        public int idCmdFournisseur { get; set; }
        public int idFournisseur { get; set; }
        public System.DateTime DateEntree { get; set; }
    
        public virtual fournisseur fournisseur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lignecommande> lignecommandes { get; set; }

        // state (not saved in database)
        public string etatCmd { get; set; }
    }
}
