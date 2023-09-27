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
    public class LopChuNhiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/LopChuNhiem
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
            var lOPCHUNHIEMs = db.LOPCHUNHIEMs.Include(l => l.GIAOVIEN).Include(l => l.HOCKY).Include(l => l.LOP).OrderBy(g => g.MaLopChuNhiem);
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
            int checkTotal = (int)(lOPCHUNHIEMs.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(lOPCHUNHIEMs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/LopChuNhiem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPCHUNHIEM lOPCHUNHIEM = db.LOPCHUNHIEMs.Find(id);
            if (lOPCHUNHIEM == null)
            {
                return HttpNotFound();
            }
            return View(lOPCHUNHIEM);
        }

        // GET: Admin/LopChuNhiem/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV");
            ViewBag.MaHKy = new SelectList(db.HOCKies, "MaHky", "TenHky");
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return View();
        }

        // POST: Admin/LopChuNhiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLop,MaGV,MaHKy,MaLopChuNhiem")] LOPCHUNHIEM lOPCHUNHIEM)
        {
            if (ModelState.IsValid)
            {
                db.LOPCHUNHIEMs.Add(lOPCHUNHIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", lOPCHUNHIEM.MaGV);
            ViewBag.MaHKy = new SelectList(db.HOCKies, "MaHky", "TenHky", lOPCHUNHIEM.MaHKy);
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", lOPCHUNHIEM.MaLop);
            return View(lOPCHUNHIEM);
        }

        // GET: Admin/LopChuNhiem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPCHUNHIEM lOPCHUNHIEM = db.LOPCHUNHIEMs.Find(id);
            if (lOPCHUNHIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", lOPCHUNHIEM.MaGV);
            ViewBag.MaHKy = new SelectList(db.HOCKies, "MaHky", "TenHky", lOPCHUNHIEM.MaHKy);
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", lOPCHUNHIEM.MaLop);
            return View(lOPCHUNHIEM);
        }

        // POST: Admin/LopChuNhiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLop,MaGV,MaHKy,MaLopChuNhiem")] LOPCHUNHIEM lOPCHUNHIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOPCHUNHIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", lOPCHUNHIEM.MaGV);
            ViewBag.MaHKy = new SelectList(db.HOCKies, "MaHky", "TenHky", lOPCHUNHIEM.MaHKy);
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", lOPCHUNHIEM.MaLop);
            return View(lOPCHUNHIEM);
        }

        // GET: Admin/LopChuNhiem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOPCHUNHIEM lOPCHUNHIEM = db.LOPCHUNHIEMs.Find(id);
            if (lOPCHUNHIEM == null)
            {
                return HttpNotFound();
            }
            return View(lOPCHUNHIEM);
        }

        // POST: Admin/LopChuNhiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOPCHUNHIEM lOPCHUNHIEM = db.LOPCHUNHIEMs.Find(id);
            db.LOPCHUNHIEMs.Remove(lOPCHUNHIEM);
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
