//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ORM_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class firma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public firma()
        {
            this.people = new HashSet<person>();
        }
    
        public int firmanr { get; set; }
        public string firmanavn { get; set; }
        public string postnr { get; set; }
    
        public virtual postnummer postnummer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<person> people { get; set; }
    }
}
