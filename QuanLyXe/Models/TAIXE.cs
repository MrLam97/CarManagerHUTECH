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
    
    public partial class TAIXE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIXE()
        {
            this.CHITIETPHANCONGs = new HashSet<CHITIETPHANCONG>();
        }
    
        public string TENDNTX { get; set; }
        public int MACHUCVU { get; set; }
        public string MATKHAUTX { get; set; }
        public string HOTENTX { get; set; }
        public Nullable<System.DateTime> NAMSINHTX { get; set; }
        public string DIACHITX { get; set; }
        public Nullable<int> SDTTX { get; set; }
        public Nullable<int> CMNDTX { get; set; }
        public Nullable<int> SOBANGLAI { get; set; }
        public string HANBANGLAI { get; set; }
        public string NOICAPBL { get; set; }
        public Nullable<System.DateTime> NGAYCAPBL { get; set; }
        public Nullable<System.DateTime> NGAYHETHANBL { get; set; }
        public string CHANDUNG { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHANCONG> CHITIETPHANCONGs { get; set; }
        public virtual CHUCVU CHUCVU { get; set; }
    }
}