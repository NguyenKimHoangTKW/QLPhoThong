using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class HocSinhController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/HocSinh
        public ActionResult Index()
        {
            var hOCSINHs = db.HOCSINHs.Include(h => h.LOP).Include(h => h.DanToc);
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
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            return View();
        }
        public List<MONHOC> LayMonHoc()
        {
            List<MONHOC> lstMonHoc =db.MONHOCs.ToList();
            return lstMonHoc;
        }
        public List<HANHKIEM> LayHanhKiem()
        {
            List<HANHKIEM> lstHanhKiem = db.HANHKIEMs.ToList();
            return lstHanhKiem;
        }
        public List<HOCKY> LayHocKy()
        {
            List<HOCKY> lstHocKy =db.HOCKies.ToList();
            return lstHocKy;
        }
        // POST: Admin/HocSinh/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(HOCSINH hOCSINH, HttpPostedFileBase Thumb)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    List<MONHOC> lstMonHoc = LayMonHoc();
                    List<HOCKY> lstHocKy = LayHocKy();
                    List<HANHKIEM> lstHanhKiem = LayHanhKiem();
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
                        foreach (var item in lstMonHoc)
                        {
                            foreach(var item2 in lstHocKy)
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
                        db.SaveChanges() ;
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


            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", hOCSINH.MaLop);
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
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", hOCSINH.MaLop);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", hOCSINH.idDanToc);
            return View(hOCSINH);
        }

        // POST: Admin/HocSinh/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "MaHS,TenHS,NgaySinh,DiaChi,SDT,MaLop,GioiTinh,TrangThai,idDanToc,Thumb, iDHS")] HOCSINH hOCSINH, HttpPostedFileBase Thumb, FormCollection form)
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
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", hOCSINH.MaLop);
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
            HOCSINH hOCSINH = db.HOCSINHs.SingleOrDefault(hs => hs.MaHS == id);
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
                var dghkList = db.DANHGIAHANHKIEMs.Where(d=> d.MaHS == hOCSINH.MaHS).ToList() ;
                var kqhkList = db.KETQUAHOCKies.Where(d => d.MaHS == hOCSINH.MaHS).ToList();
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
