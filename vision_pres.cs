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
    
    public partial class vision_pres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vision_pres()
        {
            this.examen = new HashSet<examan>();
            this.ordonnances = new HashSet<ordonnance>();
        }
    
        public int id { get; set; }
        public int od_sphere { get; set; }
        public int od_cylindre { get; set; }
        public int od_axe { get; set; }
        public int od_add { get; set; }
        public int og_sphère { get; set; }
        public int og_cylindre { get; set; }
        public int og_axe { get; set; }
        public int og_add { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<examan> examen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ordonnance> ordonnances { get; set; }
    }
}
