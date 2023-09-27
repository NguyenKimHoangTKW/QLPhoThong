using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class GiaoVienController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/GiaoVien
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
             var gIAOVIENs = db.GIAOVIENs.Include(g => g.MONHOC).OrderBy(g => g.MaGV);
            //1.2.Tạo câu truy vấn kết 3 bảng Book, Author, Category

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
            int checkTotal = (int)(gIAOVIENs.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(gIAOVIENs.ToPagedList(pageNumber, pageSize));
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
            return View();
        }

        // POST: Admin/GiaoVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGV,TenGV,NgaySinh,SDT,Diachi,MaMH")] GIAOVIEN gIAOVIEN)
        {
            if (ModelState.IsValid)
            {
                db.GIAOVIENs.Add(gIAOVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", gIAOVIEN.MaMH);
            return View(gIAOVIEN);
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
            return View(gIAOVIEN);
        }

        // POST: Admin/GiaoVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGV,TenGV,NgaySinh,SDT,Diachi,MaMH")] GIAOVIEN gIAOVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIAOVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMH = new SelectList(db.MONHOCs, "MaMH", "TenMH", gIAOVIEN.MaMH);
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
