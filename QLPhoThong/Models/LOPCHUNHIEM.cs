//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLPhoThong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LOPCHUNHIEM
    {
        public int MaLopChuNhiem { get; set; }
        public int MaLop { get; set; }
        public int MaGV { get; set; }
        public int MaHKy { get; set; }
    
        public virtual GIAOVIEN GIAOVIEN { get; set; }
        public virtual HOCKY HOCKY { get; set; }
        public virtual LOP LOP { get; set; }
    }
}
