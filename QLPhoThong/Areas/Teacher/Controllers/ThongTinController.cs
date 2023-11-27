using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QLPhoThong.Areas.Teacher.Controllers
{
    public class ThongTinController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/ThongTin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongTinGiaoVienPartial(string id)
        {
            var thongtin = db.LOPCHUNHIEMs.Where(pc => pc.MaLop == id);
            return PartialView(thongtin.Single());
        }
        public ActionResult BangdiemHK1Partial(string id)
        {
            var danhSachLop = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24").ToList();
            return PartialView(danhSachLop);
        }
        public ActionResult BangdiemHK2Partial(string id)
        {
            var danhSachLop = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24").ToList();
            return PartialView(danhSachLop);
        }
        public ActionResult KetQuaHocKy1Partial(string id)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24").FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24").ToList();
            foreach(var item in diem)
            {
                if(item.DiemTB != null)
                {
                    var diemtb = diem.Sum(d => d.DiemTB);
                    float diemtbm = (float)(diemtb / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                    db.Entry(DanhSachKetQua).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return PartialView(DanhSachKetQua);
        }

        public ActionResult KetQuaHocKy2Partial(string id)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24").FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24").ToList();
            foreach (var item in diem)
            {
                if (item.DiemTB != null)
                {
                    var diemtb = diem.Sum(d => d.DiemTB);
                    float diemtbm = (float)(diemtb / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                    db.Entry(DanhSachKetQua).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return PartialView(DanhSachKetQua);
        }
        [HttpGet]
        public ActionResult DiemDanhPartial(string id)
        {
            var DiemDanh = db.DIEMDANHs.Where(pc => pc.MaHS == id);
            return PartialView(DiemDanh.Single());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DiemDanhPartial(DIEMDANH diemdanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemdanh).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return View();
            }
        }

    }
}