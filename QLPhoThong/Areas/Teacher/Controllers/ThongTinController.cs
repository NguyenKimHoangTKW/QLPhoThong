using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult ThongTin(string id)
        {
            var thongtin = db.HOCSINHs.Where(pc => pc.MaHS == id);
            return View(thongtin.Single());
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
            var DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24");
            return PartialView(DanhSachKetQua.FirstOrDefault());
        }

        public ActionResult KetQuaHocKy2Partial(string id)
        {
            var DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24");
            return PartialView(DanhSachKetQua.FirstOrDefault());
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