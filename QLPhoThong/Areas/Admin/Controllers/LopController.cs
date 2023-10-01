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
    public class LopController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/Lop
        public ActionResult Index()
        {
            return View(db.LOPs.ToList());
        }

        // GET: Admin/Lop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP lOP = db.LOPs.Find(id);
            if (lOP == null)
            {
                return HttpNotFound();
            }
            return View(lOP);
        }

        // GET: Admin/Lop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Lop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( LOP model)
        {
            if (ModelState.IsValid)
            {
                db.LOPs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Lop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP lOP = db.LOPs.Find(id);
            if (lOP == null)
            {
                return HttpNotFound();
            }
            return View(lOP);
        }

        // POST: Admin/Lop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLop,TenLop,LopChuyen")] LOP lOP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOP);
        }

        // GET: Admin/Lop/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOP lOP = db.LOPs.Find(id);
            if (lOP == null)
            {
                return HttpNotFound();
            }
            return View(lOP);
        }

        // POST: Admin/Lop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOP lOP = db.LOPs.Find(id);
            db.LOPs.Remove(lOP);
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
