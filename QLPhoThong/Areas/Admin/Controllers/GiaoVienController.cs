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
            var gIAOVIENs = db.GIAOVIENs.Include(g => g.DanToc);
            return View(gIAOVIENs.ToList());
        }

        // GET: Admin/GiaoVien/Details/5
        public ActionResult Details(string id)
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
                    
                    string nextId = GetNextId();
                    gIAOVIEN.MaGV = nextId;
                    if (Thumb != null && Thumb.ContentLength > 0)
                    {
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

            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gIAOVIEN);
        }
        private string GetNextId()
        {
            var allIds = db.GIAOVIENs.Select(h => h.MaGV).ToList();

            int nextId = 1;

            if (allIds.Count > 0)
            {
                var maxId = allIds.Max();

                if (maxId.StartsWith("GVPT0"))
                {
                    int.TryParse(maxId.Substring(2), out int numericPart);
                    nextId = numericPart + 1;
                }
            }

            string formattedId = "GVPT0" + nextId.ToString();

            return formattedId;
        }
        // GET: Admin/GiaoVien/Edit/5
        public ActionResult Edit(string id, GIAOVIEN gIAOVIEN)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var gv = db.GIAOVIENs.SingleOrDefault(g => g.MaGV == id);
            if (gv == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gv);
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
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", gIAOVIEN.idDanToc);
            return View(gIAOVIEN);
        }

        // GET: Admin/GiaoVien/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
