//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyXe.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUACHUA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUACHUA()
        {
            this.CHITIETSUACHUAs = new HashSet<CHITIETSUACHUA>();
        }
    
        public int MASUACHUA { get; set; }
        public int MAXE { get; set; }
        public string NOISUA { get; set; }
        public Nullable<System.DateTime> NGAYBD { get; set; }
        public Nullable<System.DateTime> NGAYKT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETSUACHUA> CHITIETSUACHUAs { get; set; }
        public virtual XE XE { get; set; }
    }
}
