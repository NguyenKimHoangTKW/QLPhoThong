using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class DiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/Diem
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
            var dIEMs = db.DIEMs.Include(d => d.HOCKY).Include(d => d.HOCSINH).Include(d => d.MONHOC).OrderBy(d => d.MaBD);
            // 3 Đoạn code sau dùng để phân trang
            ViewBag.Page = page;

            // 3.1. Tạo danh sách chọn số trang
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 3.2. Thiết lập số trang đang chọn vào danh sách List<SelectListItem> items
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.Size = items;
            ViewBag.CurrentSize = size;
            // 3.3. Nếu page = null thì đặt lại là 1.
            page = page ?? 1; //if (page == null) page = 1;

            // 3.4. Tạo kích thước trang (pageSize), mặc định là 5.
            int pageSize = (size ?? 5);

            ViewBag.pageSize = pageSize;

            // 3.5. Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 3.6 Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(dIEMs.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(dIEMs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Diem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM dIEM = db.DIEMs.Find(id);
            if (dIEM == null)
            {
                return HttpNotFound();
            }
            return View(dIEM);
        }

        // GET: Admin/Diem/Create
        public ActionResult Create()
        {
            ViewBag.MaHK = new SelectList(db.HOCKies, "MaHky", "TenHky");
            ViewBag.MaHS = new SelectList(db.HOCSINHs, "MaHS", "TenHS");
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH");
            return View();
        }

        // POST: Admin/Diem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBD,DiemMieng,Diem15p,Diem1Tiet,DiemThi,MaHS,MaMH,MaHK,DiemTB")] DIEM dIEM)
        {
            if (ModelState.IsValid)
            {
                db.DIEMs.Add(dIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHK = new SelectList(db.HOCKies, "MaHky", "TenHky", dIEM.MaHK);
            ViewBag.MaHS = new SelectList(db.HOCSINHs, "MaHS", "TenHS", dIEM.MaHS);
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", dIEM.MaMH);
            return View(dIEM);
        }

        // GET: Admin/Diem/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM dIEM = db.DIEMs.Find(id);
            if (dIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHK = new SelectList(db.HOCKies, "MaHky", "TenHky", dIEM.MaHK);
            ViewBag.MaHS = new SelectList(db.HOCSINHs, "MaHS", "TenHS", dIEM.MaHS);
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", dIEM.MaMH);
            return View(dIEM);
        }

        // POST: Admin/Diem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBD,DiemMieng,Diem15p,Diem1Tiet,DiemThi,MaHS,MaMH,MaHK,DiemTB")] DIEM dIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHK = new SelectList(db.HOCKies, "MaHky", "TenHky", dIEM.MaHK);
            ViewBag.MaHS = new SelectList(db.HOCSINHs, "MaHS", "TenHS", dIEM.MaHS);
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", dIEM.MaMH);
            return View(dIEM);
        }

        // GET: Admin/Diem/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIEM dIEM = db.DIEMs.Find(id);
            if (dIEM == null)
            {
                return HttpNotFound();
            }
            return View(dIEM);
        }

        // POST: Admin/Diem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DIEM dIEM = db.DIEMs.Find(id);
            db.DIEMs.Remove(dIEM);
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
