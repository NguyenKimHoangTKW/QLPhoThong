using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{
    public class QuanLyDiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/QuanLyDiem
        public ActionResult XemDiemHocSinhPhuTrachDay(string id, string idgv, string idlop)
        {
            var xemdiem = from d in db.DIEMs
                          from p in db.PHANCONGs
                          from m in db.MONHOCs
                          from l in db.LOPs
                          where p.MaMH == m.MaMH
                          where m.MaMH == d.MaMH
                          where l.MaLop == idlop
                          where l.MaLop == p.MaLop
                          where d.MaHK == "1"
                          where d.MaNH == "NH23|24"
                          where d.MaHS == id
                          where p.MaGV == idgv
                          select d;
            return View(xemdiem.First());
        }
        [HttpGet]
        public ActionResult XemDiemHocSinhPhuTrachDayHK1Partial(int id)
        {
            var xemdiem = db.DIEMs.Where(d => d.MaBD == id);
                          
            return PartialView(xemdiem.First());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XemDiemHocSinhPhuTrachDayHK1Partial(DIEM diem)
        {
       
            if (ModelState.IsValid)
            {
                if (diem.DiemMieng != null && diem.Diem15p != null && diem.Diem1Tiet != null && diem.DiemThi != null)
                {
                    float diemtbm = (float)((diem.DiemMieng + diem.Diem15p) + (diem.Diem1Tiet * 2) + (diem.DiemThi * 3)) / 7;
                    string diemtbmFormatted = diemtbm.ToString("N1"); 
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    diem.DiemTB = diemtbmRounded;
                    db.Entry(diem).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Entry(diem).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
            }
            return PartialView(diem);
        }
    }
}