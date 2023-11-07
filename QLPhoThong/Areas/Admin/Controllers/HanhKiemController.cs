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
    public class HanhKiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/HanhKiem
        public ActionResult Index()
        {
            var hANHKIEMs = db.HANHKIEMs.Include(h => h.DANHMUCHANHKIEM);
            return View(hANHKIEMs.ToList());
        }

        // GET: Admin/HanhKiem/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANHKIEM hANHKIEM = db.HANHKIEMs.Find(id);
            if (hANHKIEM == null)
            {
                return HttpNotFound();
            }
            return View(hANHKIEM);
        }

        // GET: Admin/HanhKiem/Create
        public ActionResult Create()
        {
            ViewBag.MaDMHK = new SelectList(db.DANHMUCHANHKIEMs, "MaDMHK", "TenDMHK");
            return View();
        }

        // POST: Admin/HanhKiem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHKiem,MaDMHK,TenHKiem,DiemToiDa")] HANHKIEM hANHKIEM)
        {
            if (ModelState.IsValid)
            {
                int MaDMHK = hANHKIEM.MaDMHK;
                string MaHKiemPrefix = MaDMHK + ".";
                string nextId = GetNextId(MaDMHK);
                hANHKIEM.MaHKiem = MaHKiemPrefix + nextId;
                db.HANHKIEMs.Add(hANHKIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDMHK = new SelectList(db.DANHMUCHANHKIEMs, "MaDMHK", "TenDMHK", hANHKIEM.MaDMHK);
            return View(hANHKIEM);
        }
        private string GetNextId(int MaDanhMucHanhKiem)
        {
            var allIds = db.HANHKIEMs.Where(h => h.MaDMHK == MaDanhMucHanhKiem).Select(h => h.MaHKiem).ToList();

            int nextId = 1;

            if (allIds.Count > 0)
            {
                var maxId = allIds.Max();

                if (maxId.StartsWith(MaDanhMucHanhKiem + "."))
                {
                    int.TryParse(maxId.Substring((MaDanhMucHanhKiem + 1).ToString().Length + 1), out int numericPart);
                    nextId = numericPart + 1;
                }
            }

            return nextId.ToString();
        }

        // GET: Admin/HanhKiem/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANHKIEM hANHKIEM = db.HANHKIEMs.Find(id);
            if (hANHKIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDMHK = new SelectList(db.DANHMUCHANHKIEMs, "MaDMHK", "TenDMHK", hANHKIEM.MaDMHK);
            return View(hANHKIEM);
        }

        // POST: Admin/HanhKiem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHKiem,MaDMHK,TenHKiem,DiemToiDa")] HANHKIEM hANHKIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hANHKIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDMHK = new SelectList(db.DANHMUCHANHKIEMs, "MaDMHK", "TenDMHK", hANHKIEM.MaDMHK);
            return View(hANHKIEM);
        }

        // GET: Admin/HanhKiem/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANHKIEM hANHKIEM = db.HANHKIEMs.Find(id);
            if (hANHKIEM == null)
            {
                return HttpNotFound();
            }
            return View(hANHKIEM);
        }

        // POST: Admin/HanhKiem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HANHKIEM hANHKIEM = db.HANHKIEMs.Find(id);
            db.HANHKIEMs.Remove(hANHKIEM);
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
