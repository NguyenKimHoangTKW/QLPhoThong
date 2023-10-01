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
    public class GiaoVienController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/GiaoVien
        public ActionResult Index()
        {
            var gIAOVIENs = db.GIAOVIENs.Include(g => g.MONHOC).Include(g => g.DanToc);
            return View(gIAOVIENs.ToList());
        }

        // GET: Admin/GiaoVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAOVIEN);
        }

        // GET: Admin/GiaoVien/Create
        public ActionResult Create()
        {
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH");
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            return View();
        }

        // POST: Admin/GiaoVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,TenGV,NgaySinh,SDT,Diachi,MaMH,GioiTinh,Gmail,TrangThai,idDanToc,Thumb,iDGV")] GIAOVIEN gIAOVIEN, HttpPostedFileBase Thumb)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    int nextId = GetNextId();

                    // Tạo mã id mới
                    gIAOVIEN.MaGV = nextId;
                    gIAOVIEN.iDGV = "GV" + nextId.ToString("2023PT"); ;
                    // Kiểm tra xem có tệp ảnh được chọn không
                    if (Thumb != null && Thumb.ContentLength > 0)
                    {
                        // Tạo một tên tệp mới dựa trên timestamp
                        string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                        string _Tail = Path.GetExtension(Thumb.FileName);
                        string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                        string _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), fullLink);

                        Thumb.SaveAs(_path);
                        gIAOVIEN.Thumb = fullLink;
                        db.GIAOVIENs.Add(gIAOVIEN);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
            }

            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", gIAOVIEN.MaMH);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gIAOVIEN);
        }
        private int GetNextId()
        {
            // Tìm giá trị id tiếp theo trong bảng
            int? maxId = db.GIAOVIENs.Max(t => (int?)t.MaGV);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/GiaoVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", gIAOVIEN.MaMH);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gIAOVIEN);
        }

        // POST: Admin/GiaoVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,TenGV,NgaySinh,SDT,Diachi,MaMH,GioiTinh,Gmail,TrangThai,idDanToc,Thumb,iDGV")] GIAOVIEN gIAOVIEN, HttpPostedFileBase Thumb, FormCollection form)
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
                        string _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesTeacher"), fullLink);
                        Thumb.SaveAs(_path);
                        gIAOVIEN.Thumb = fullLink;
                        _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesTeacher"), form["oldimage"]);

                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                    gIAOVIEN.Thumb = form["oldimage"];
                    db.Entry(gIAOVIEN).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", gIAOVIEN.MaMH);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gIAOVIEN);
        }

        // GET: Admin/GiaoVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            if (gIAOVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIAOVIEN);
        }

        // POST: Admin/GiaoVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GIAOVIEN gIAOVIEN = db.GIAOVIENs.Find(id);
            db.GIAOVIENs.Remove(gIAOVIEN);
            db.SaveChanges();
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
