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
    
    public partial class vision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vision()
        {
            this.vision_detaille = new HashSet<vision_detaille>();
        }
    
        public int idOeil { get; set; }
        public Nullable<float> cyl { get; set; }
        public Nullable<float> sph { get; set; }
        public Nullable<float> axe { get; set; }
        public Nullable<float> add { get; set; }
        public Nullable<float> ecart { get; set; }
        public Nullable<float> hauteur { get; set; }
        public System.DateTime date_modification { get; set; }
        public Nullable<int> lentille_idLentille { get; set; }
        public Nullable<int> verre_idVerre { get; set; }
        public bool gauche { get; set; }
        public bool loin { get; set; }
        public Nullable<int> visite_id { get; set; }
    
        public virtual lentille lentille { get; set; }
        public virtual verre verre { get; set; }
        public virtual visite visite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vision_detaille> vision_detaille { get; set; }
    }
}
