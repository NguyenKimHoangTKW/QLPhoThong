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
    
    public partial class HOCSINH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOCSINH()
        {
            this.DIEMDANHs = new HashSet<DIEMDANH>();
            this.BANGDIEMCANAMs = new HashSet<BANGDIEMCANAM>();
            this.KETQUACANAMs = new HashSet<KETQUACANAM>();
            this.DIEMs = new HashSet<DIEM>();
            this.KETQUAHOCKies = new HashSet<KETQUAHOCKY>();
        }
    
        public string MaHS { get; set; }
        public string HoVaTenDem { get; set; }
        public string TenHS { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string GioiTinh { get; set; }
        public string MaLop { get; set; }
        public string TrangThai { get; set; }
        public int idDanToc { get; set; }
        public string Thumb { get; set; }
    
        public virtual DanToc DanToc { get; set; }
        public virtual LOP LOP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEMDANH> DIEMDANHs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BANGDIEMCANAM> BANGDIEMCANAMs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KETQUACANAM> KETQUACANAMs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIEM> DIEMs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KETQUAHOCKY> KETQUAHOCKies { get; set; }
    }
}
