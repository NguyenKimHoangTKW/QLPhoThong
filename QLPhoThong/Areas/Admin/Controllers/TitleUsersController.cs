﻿using System;
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
using QLPhoThong.App_Start;

namespace QLPhoThong.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class TitleUsersController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/TitleUsers
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
            var titleUser = db.TITLEUSERs.OrderBy(g => g.MaTitleUser);
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
            int checkTotal = (int)(titleUser.ToList().Count / pageSize) + 1;
            // Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(titleUser.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/TitleUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TITLEUSER tITLEUSER = db.TITLEUSERs.Find(id);
            if (tITLEUSER == null)
            {
                return HttpNotFound();
            }
            return View(tITLEUSER);
        }

        // GET: Admin/TitleUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TitleUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTitleUser,TenTitleUser")] TITLEUSER tITLEUSER)
        {
            if (ModelState.IsValid)
            {
                db.TITLEUSERs.Add(tITLEUSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tITLEUSER);
        }

        // GET: Admin/TitleUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TITLEUSER tITLEUSER = db.TITLEUSERs.Find(id);
            if (tITLEUSER == null)
            {
                return HttpNotFound();
            }
            return View(tITLEUSER);
        }

        // POST: Admin/TitleUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTitleUser,TenTitleUser")] TITLEUSER tITLEUSER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tITLEUSER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tITLEUSER);
        }

        // GET: Admin/TitleUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TITLEUSER tITLEUSER = db.TITLEUSERs.Find(id);
            if (tITLEUSER == null)
            {
                return HttpNotFound();
            }
            return View(tITLEUSER);
        }

        // POST: Admin/TitleUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TITLEUSER tITLEUSER = db.TITLEUSERs.Find(id);
            db.TITLEUSERs.Remove(tITLEUSER);
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
