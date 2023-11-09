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
            var danhSachLop = db.PHANCONGs.Where(pc => pc.MaGV == id).ToList();
            return View(danhSachLop);
        }

        public ActionResult LopChuNhiem(string id)
        {
            var danhSachLop = db.LOPCHUNHIEMs.Where(pc => pc.MaGV == id).ToList();
            return View(danhSachLop);
        }

        public ActionResult DanhSachHocSinh(string id)
        {
            var danhSachLop = db.HOCSINHs.Where(pc => pc.MaLop == id).ToList();
            return View(danhSachLop);
        }
        public ActionResult DanhSachHocSinhPhuTrachDay(string idlop, int idmh)
        {
            string idhk = "1";
            var danhSachLop = (from hs in db.HOCSINHs
                               from d in db.DIEMs
                               where d.MaHS == hs.MaHS
                               where hs.MaLop == idlop
                               where d.MaHK == idhk
                               where d.MaMH == idmh
                               select d).ToList();
            if (Request.IsAjaxRequest())
            {
                return PartialView("NhapDiem", danhSachLop);
            }
            ViewBag.idhk = new SelectList(db.HOCKies, "MaHky", "TenHky",idhk);
            ViewBag.TongHocSinh = danhSachLop.Count;
            return View(danhSachLop);
        }
       
        public ActionResult XemBangDiemHocSinh(string id)
        {
            var danhSachLop = db.DIEMs.FirstOrDefault(pc => pc.MaHS == id);
            return View(danhSachLop);
        }
        
        
    }
}
