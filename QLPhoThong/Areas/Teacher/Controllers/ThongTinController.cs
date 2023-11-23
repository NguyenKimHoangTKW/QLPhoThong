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
        [HttpGet]
        public ActionResult ThongTin(string id)
        {
            var thongtin = db.HOCSINHs.Where(pc => pc.MaHS == id);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            return View(thongtin.Single());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThongTin([Bind(Include = "MaHS,TenHS,NgaySinh,DiaChi,SDT,MaLop,GioiTinh,TrangThai,idDanToc,Thumb, iDHS")] HOCSINH hOCSINH, HttpPostedFileBase Thumb, FormCollection form)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    if (Thumb != null)
                    {
                        string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                        string _Tail = Path.GetExtension(Thumb.FileName);
                        string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                        string _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), fullLink);
                        Thumb.SaveAs(_path);
                        hOCSINH.Thumb = fullLink;
                        _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), form["oldimage"]);

                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                    hOCSINH.Thumb = form["oldimage"];
                    db.Entry(hOCSINH).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop", hOCSINH.MaLop);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", hOCSINH.idDanToc);
            return View(hOCSINH);
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