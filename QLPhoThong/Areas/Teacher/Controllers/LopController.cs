using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{
    public class LopController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/Lop
        public ActionResult Index(string id)
        {
            var danhSachLop = db.PHANCONGs.Where(pc => pc.MaGV == id).OrderBy(pc => pc.LOP.TenLop).ToList();
            return View(danhSachLop);
        }

        public ActionResult LopChuNhiem(string id)
        {
            var danhSachLop = db.LOPCHUNHIEMs.Where(pc => pc.MaGV == id).ToList();
            return View(danhSachLop);
        }

        public ActionResult DanhSachHocSinh(string id)
        {
            var danhSachLop = db.HOCSINHs.Where(pc => pc.MaLop == id).OrderByDescending(pc=>pc.TenHS).ToList();
            ViewBag.TongHocSinh = danhSachLop.Count;
            return View(danhSachLop);
        }
        public ActionResult DanhSachHocSinhPhuTrachDay(string idlop, int idmh, string hocky = "1")
        {
            var danhSachLop = (from hs in db.HOCSINHs
                               from d in db.DIEMs
                               where d.MaHS == hs.MaHS
                               where hs.MaLop == idlop
                               where d.MaHK == hocky
                               where d.MaMH == idmh
                               select d).ToList();
          
            ViewBag.TongHocSinh = danhSachLop.Count;
            ViewBag.hocky = new SelectList(db.HOCKies, "MaHky", "TenHky");
            return View(danhSachLop);
        }
       
        public ActionResult XemBangDiemHocSinh(string id)
        {
            var danhSachLop = db.DIEMs.FirstOrDefault(pc => pc.MaHS == id);
            return View(danhSachLop);
        }
        
        
    }
}
