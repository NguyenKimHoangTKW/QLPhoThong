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
using OfficeOpenXml;
using System.Globalization;

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
                        dd.MaNH = "NH23|24";
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
                            BANGDIEMCANAM bdcn = new BANGDIEMCANAM();
                            bdcn.MaHS = hOCSINH.MaHS;
                            bdcn.MaMH = item.MaMH;
                            bdcn.MaNH = "NH23|24";
                            db.BANGDIEMCANAMs.Add(bdcn);
                        }
                        db.SaveChanges();
                        foreach (var item2 in lstHocKy)
                        {
                            KETQUAHOCKY kqhk = new KETQUAHOCKY();
                            kqhk.MaHS = hOCSINH.MaHS;
                            kqhk.MaHK = item2.MaHky;
                            kqhk.MaNH = "NH23|24";
                            kqhk.Xeploai = "Chưa xét";
                            kqhk.HocLuc = "Chưa xét";
                            kqhk.HanhKiem = "Chưa xét";
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
        private string MapTenLopToMaLop(string tenLop)
        {
            var lop = db.LOPs.FirstOrDefault(l => l.TenLop == tenLop);

            if (lop != null)
            {
                return lop.MaLop;
            }
            else
            {
                return "";
            }
        }
        private int MapTenDanTocToIdDanToc(string tenDanToc)
        {
            var danToc = db.DanTocs.FirstOrDefault(dt => dt.TenDanToc == tenDanToc);

            if (danToc != null)
            {
                return danToc.idDanToc;
            }
            else
            {
                return 0;
            }
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    List<MONHOC> lstMonHoc = LayMonHoc();
                    List<HOCKY> lstHocKy = LayHocKy();
                    List<HANHKIEM> lstHanhKiem = LayHanhKiem();
                    List<DIEMDANH> lstDiemDanh = LayDiemDanh();

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        List<HOCSINH> danhSachHocSinh = new List<HOCSINH>();
                        for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
                        {
                            string tenLop = worksheet.Cells[row, 8].Value.ToString();
                            string maLop = MapTenLopToMaLop(tenLop);
                            string tenDanToc = worksheet.Cells[row, 10].Value.ToString();
                            int idDanToc = MapTenDanTocToIdDanToc(tenDanToc);
                            HOCSINH hOCSINH = new HOCSINH
                            {
                                HoVaTenDem = worksheet.Cells[row, 2].Value.ToString(),
                                TenHS = worksheet.Cells[row, 3].Value.ToString(),
                                NgaySinh = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString()),
                                DiaChi = worksheet.Cells[row, 5].Value.ToString(),
                                SDT = worksheet.Cells[row, 6].Value.ToString(),
                                GioiTinh = worksheet.Cells[row, 7].Value.ToString(),
                                MaLop = maLop,
                                TrangThai = worksheet.Cells[row, 9].Value.ToString(),
                                idDanToc = idDanToc,
                                Thumb = worksheet.Cells[row, 11].Value.ToString(),
                            };
                            danhSachHocSinh.Add(hOCSINH);
                        }
                        foreach (var hOCSINH in danhSachHocSinh)
                        {
                            string nextId = GetNextId();
                            hOCSINH.MaHS = nextId;
                            db.HOCSINHs.Add(hOCSINH);
                            db.SaveChanges();

                            DIEMDANH dd = new DIEMDANH();
                            dd.MaHS = hOCSINH.MaHS;
                            dd.NghiCoPhep = 0;
                            dd.NghiKhongPhep = 0;
                            dd.BoTiet = 0;
                            dd.MaNH = "NH23|24";
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
                                BANGDIEMCANAM bdcn = new BANGDIEMCANAM();
                                bdcn.MaHS = hOCSINH.MaHS;
                                bdcn.MaMH = item.MaMH;
                                bdcn.MaNH = "NH23|24";
                                db.BANGDIEMCANAMs.Add(bdcn);
                            }
                            db.SaveChanges();
                            foreach (var item2 in lstHocKy)
                            {
                                KETQUAHOCKY kqhk = new KETQUAHOCKY();
                                kqhk.MaHS = hOCSINH.MaHS;
                                kqhk.MaHK = item2.MaHky;
                                kqhk.MaNH = "NH23|24";
                                kqhk.Xeploai = "Chưa xét";
                                kqhk.HocLuc = "Chưa xét";
                                kqhk.HanhKiem = "Chưa xét";
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
                        }
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Import không thành công: " + ex.Message;
                }
            }
            else
            {
                ViewBag.Message = "Vui lòng chọn file Excel!";
            }

            return RedirectToAction("Index");
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
        public ActionResult DownloadFile()
        {
            string filePath = Server.MapPath("~/Areas/Admin/FileExcel/Biểu mẫu Thêm Học Sinh.xlsx");
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                string fileName = "Biểu mẫu Thêm Học Sinh.xlsx";

                // Trả về một FileResult để tải xuống
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                return HttpNotFound("File not found.");
            }
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
                var bdcn = db.BANGDIEMCANAMs.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
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
                foreach (var bangdiemcn in bdcn)
                {
                    db.BANGDIEMCANAMs.Remove(bangdiemcn);
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
