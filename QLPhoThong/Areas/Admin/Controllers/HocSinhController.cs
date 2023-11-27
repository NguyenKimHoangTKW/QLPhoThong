using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class HocSinhController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/HocSinh
        public ActionResult Index()
        {
            var hOCSINHs = db.HOCSINHs.Include(h => h.DanToc).Include(h => h.LOP).OrderBy(h => h.TenHS);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop");
            return View(hOCSINHs.ToList());
        }

        // GET: Admin/HocSinh/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            return View(hOCSINH);
        }

        // GET: Admin/HocSinh/Create
        public ActionResult Create()
        {
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop");
            HOCSINH hOCSINH = new HOCSINH();
            return PartialView("Create",hOCSINH);
        }
        public List<MONHOC> LayMonHoc()
        {
            List<MONHOC> lstMonHoc = db.MONHOCs.ToList();
            return lstMonHoc;
        }
        public List<HANHKIEM> LayHanhKiem()
        {
            List<HANHKIEM> lstHanhKiem = db.HANHKIEMs.ToList();
            return lstHanhKiem;
        }
        public List<HOCKY> LayHocKy()
        {
            List<HOCKY> lstHocKy = db.HOCKies.ToList();
            return lstHocKy;
        }
        public List<DIEMDANH> LayDiemDanh()
        {
            List<DIEMDANH> lstDiemDanh = db.DIEMDANHs.ToList();
            return lstDiemDanh;
        }
        // POST: Admin/HocSinh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HOCSINH hOCSINH, HttpPostedFileBase Thumb, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<MONHOC> lstMonHoc = LayMonHoc();
                    List<HOCKY> lstHocKy = LayHocKy();
                    List<HANHKIEM> lstHanhKiem = LayHanhKiem();
                    List<DIEMDANH> lstDiemDanh = LayDiemDanh();
                    var sdt = f["SoDienThoai"];
                    hOCSINH.SDT ="+84" + sdt;
                    string nextId = GetNextId();
                    hOCSINH.MaHS = nextId;
                    if (Thumb != null && Thumb.ContentLength > 0)
                    {
                        string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                        string _Tail = Path.GetExtension(Thumb.FileName);
                        string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                        string _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), fullLink);
                        Thumb.SaveAs(_path);
                        hOCSINH.Thumb = fullLink;
                        db.HOCSINHs.Add(hOCSINH);
                        db.SaveChanges();

                        DIEMDANH dd = new DIEMDANH();
                        dd.MaHS = hOCSINH.MaHS;
                        dd.NghiCoPhep = 0;
                        dd.NghiKhongPhep = 0;
                        dd.BoTiet = 0;
                        db.DIEMDANHs.Add(dd);
                        db.SaveChanges();

                        foreach (var item in lstMonHoc)
                        {
                            foreach (var item2 in lstHocKy)
                            {
                                DIEM diem = new DIEM();
                                diem.MaHS = hOCSINH.MaHS;
                                diem.MaMH = item.MaMH;
                                diem.MaHK = item2.MaHky;
                                diem.MaNH = "NH23|24";
                                db.DIEMs.Add(diem);

                            }
                        }
                        db.SaveChanges();
                        foreach (var item2 in lstHocKy)
                        {
                            KETQUAHOCKY kqhk = new KETQUAHOCKY();
                            kqhk.MaHS = hOCSINH.MaHS;
                            kqhk.MaHK = item2.MaHky;
                            kqhk.MaNH = "NH23|24";
                            kqhk.Xeploai = "Chưa xét";
                            db.KETQUAHOCKies.Add(kqhk);

                        }
                        db.SaveChanges();
                        foreach (var item in lstHanhKiem)
                        {
                            DANHGIAHANHKIEM dghk = new DANHGIAHANHKIEM();
                            dghk.MaHS = hOCSINH.MaHS;
                            dghk.MaHKiem = item.MaHKiem;
                            dghk.MaNH = "NH23|24";
                            db.DANHGIAHANHKIEMs.Add(dghk);
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            else
            {
                ModelState.AddModelError("", "Vui lòng chọn một tệp ảnh.");
            }


            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop", hOCSINH.MaLop);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", hOCSINH.idDanToc);
            return View(hOCSINH);
        }
        private string GetNextId()
        {
            var allIds = db.HOCSINHs.Select(h => h.MaHS).ToList();

            int nextId = 1;

            if (allIds.Count > 0)
            {
                var maxId = allIds.Max();

                if (maxId.StartsWith("HS"))
                {
                    int.TryParse(maxId.Substring(2), out int numericPart);
                    nextId = numericPart + 1;
                }
            }

            string formattedId = "HS" + nextId.ToString();

            return formattedId;
        }

        // GET: Admin/HocSinh/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", hOCSINH.idDanToc);
            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop", hOCSINH.MaLop);
            return PartialView("Edit", hOCSINH);
        }

        // POST: Admin/HocSinh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaHS,HoVaTenDem,TenHS,NgaySinh,DiaChi,SDT,GioiTinh,MaLop,TrangThai,idDanToc,Thumb")] HOCSINH hOCSINH, HttpPostedFileBase Thumb, FormCollection form)
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

        // GET: Admin/HocSinh/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);
            if (hOCSINH == null)
            {
                return HttpNotFound();
            }
            return View(hOCSINH);
        }

        // POST: Admin/HocSinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOCSINH hOCSINH = db.HOCSINHs.Find(id);

            if (hOCSINH != null)
            {
                var diemList = db.DIEMs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                var dghkList = db.DANHGIAHANHKIEMs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                var kqhkList = db.KETQUAHOCKies.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                var diemdanh = db.DIEMDANHs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                foreach (var kqhk in kqhkList)
                {
                    db.KETQUAHOCKies.Remove(kqhk);
                }
                foreach (var diem in diemList)
                {
                    db.DIEMs.Remove(diem);
                }
                foreach (var dghk in dghkList)
                {
                    db.DANHGIAHANHKIEMs.Remove(dghk);
                }
                foreach (var dd in diemdanh)
                {
                    db.DIEMDANHs.Remove(dd);
                }
                if (!string.IsNullOrEmpty(hOCSINH.Thumb))
                {
                    string imagePath = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), hOCSINH.Thumb);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                db.HOCSINHs.Remove(hOCSINH);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult ChuyenLopHocSinh(string malop = "1")
        {
            var hOCSINHs = db.HOCSINHs.Include(h => h.DanToc).Include(h => h.LOP).Where(hs => hs.MaLop == malop).OrderBy(h => h.TenHS).ToList();
            ViewBag.idLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            ViewBag.malop = new SelectList(db.LOPs, "MaLop", "TenLop");
            ViewBag.SiSo = hOCSINHs.Count;
            return View(hOCSINHs);
        }
        public ActionResult CapNhatChuyenLopPartial(string id)
        {
            var hOCSINHs = db.HOCSINHs.Where(hs => hs.MaHS == id).FirstOrDefault();
            ViewBag.idLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return PartialView(hOCSINHs);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatChuyenLopPartial(HOCSINH hOCSINH)
        {
            if (ModelState.IsValid)
            {
                List<DIEM> lstdiem = db.DIEMs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                List<DIEMDANH> lstdiemdanh = db.DIEMDANHs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
                foreach(var item in lstdiem)
                {
                    if (item != null)
                    {
                        item.DiemMieng = null;
                        item.Diem15p = null;
                        item.Diem1Tiet =null;
                        item.DiemThi = null;
                        item.DiemTB = null;
                    }
                }
                foreach (var item in lstdiemdanh)
                {
                    if (item != null)
                    {
                        item.NghiCoPhep = 0;
                        item.NghiKhongPhep = 0;
                        item.BoTiet = 0;
                        item.GhiChu = null;
                    }
                }
                db.Entry(hOCSINH).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SweetAlertMessage"] = "Chuyển lớp thành công cho học sinh " + hOCSINH.TenHS;
                TempData["SweetAlertType"] = "success";
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
