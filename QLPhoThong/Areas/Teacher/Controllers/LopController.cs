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
        public ActionResult DanhSachHocSinhPhuTrachDay(string id)
        {
            var danhSachLop = db.HOCSINHs.Where(pc => pc.MaLop == id).ToList();
            return View(danhSachLop);
        }
        public ActionResult XemBangDiemHocSinh(string id)
        {
            var danhSachLop = db.DIEMs.FirstOrDefault(pc => pc.MaHS == id);
            return View(danhSachLop);
        }
        
        
    }
}
