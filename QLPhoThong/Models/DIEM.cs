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
    
    public partial class DIEM
    {
        public int MaBD { get; set; }
        public Nullable<double> DiemMieng { get; set; }
        public Nullable<double> Diem15p { get; set; }
        public Nullable<double> Diem1Tiet { get; set; }
        public Nullable<double> DiemThi { get; set; }
        public string DanhGiaCuaGV { get; set; }
        public string MaHS { get; set; }
        public int MaMH { get; set; }
        public string MaHK { get; set; }
        public Nullable<double> DiemTB { get; set; }
        public string MaNH { get; set; }
    
        public virtual HOCKY HOCKY { get; set; }
        public virtual HOCSINH HOCSINH { get; set; }
        public virtual MONHOC MONHOC { get; set; }
        public virtual NAMHOC NAMHOC { get; set; }
    }
}
