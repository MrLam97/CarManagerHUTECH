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
    
    public partial class THONGKETHANG
    {
        public decimal THANG { get; set; }
        public decimal NAM { get; set; }
        public int MAXE { get; set; }
        public Nullable<int> SOCHUYEN { get; set; }
        public Nullable<float> SOKM { get; set; }
    
        public virtual XE XE { get; set; }
    }
}