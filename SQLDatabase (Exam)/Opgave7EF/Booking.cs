//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Opgave7EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Bookudstyrs = new HashSet<Bookudstyr>();
        }
    
        public int id { get; set; }
        public System.DateTime dato { get; set; }
        public bool forsikring { get; set; }
        public Nullable<int> lejerId { get; set; }
        public Nullable<int> virksomhedsId { get; set; }
    
        public virtual Lejer Lejer { get; set; }
        public virtual Virksomhed Virksomhed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bookudstyr> Bookudstyrs { get; set; }
    }
}
