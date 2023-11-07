using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class DanhMucHanhKiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/DanhMucHanhKiem
        public ActionResult Index()
        {
            return View(db.DANHMUCHANHKIEMs.ToList());
        }

        // GET: Admin/DanhMucHanhKiem/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCHANHKIEM dANHMUCHANHKIEM = db.DANHMUCHANHKIEMs.Find(id);
            if (dANHMUCHANHKIEM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCHANHKIEM);
        }

        // GET: Admin/DanhMucHanhKiem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DanhMucHanhKiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDMHK,TenDMHK,DiemToiDa")] DANHMUCHANHKIEM dANHMUCHANHKIEM)
        {
            if (ModelState.IsValid)
            {
                int nextId = GetNextId();
                dANHMUCHANHKIEM.MaDMHK = nextId;
                db.DANHMUCHANHKIEMs.Add(dANHMUCHANHKIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dANHMUCHANHKIEM);
        }
        private int GetNextId()
        {
                int? maxId = db.DANHMUCHANHKIEMs.Max(t => (int?)t.MaDMHK);

            if (maxId.HasValue)
            {
                return maxId.Value + 1;
            }
            {
                return 1;
            }
        }
        // GET: Admin/DanhMucHanhKiem/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCHANHKIEM dANHMUCHANHKIEM = db.DANHMUCHANHKIEMs.Find(id);
            if (dANHMUCHANHKIEM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCHANHKIEM);
        }

        // POST: Admin/DanhMucHanhKiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDMHK,TenDMHK,DiemToiDa")] DANHMUCHANHKIEM dANHMUCHANHKIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHMUCHANHKIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dANHMUCHANHKIEM);
        }

        // GET: Admin/DanhMucHanhKiem/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCHANHKIEM dANHMUCHANHKIEM = db.DANHMUCHANHKIEMs.Find(id);
            if (dANHMUCHANHKIEM == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCHANHKIEM);
        }

        // POST: Admin/DanhMucHanhKiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DANHMUCHANHKIEM dANHMUCHANHKIEM = db.DANHMUCHANHKIEMs.Find(id);
            db.DANHMUCHANHKIEMs.Remove(dANHMUCHANHKIEM);
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
