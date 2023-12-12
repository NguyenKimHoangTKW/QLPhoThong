using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using QLPhoThong.App_Start;
using QLPhoThong.Models;

namespace QLPhoThong.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class UsersController : Controller
    {
        private diemhsEntities db = new diemhsEntities();

        // GET: Admin/Users
        [HttpGet]
        public ActionResult Index(int? size, int? page)
        {
            var users = db.Users.Include(u => u.GIAOVIEN).Include(u => u.TITLEUSER).OrderBy(g => g.MaTitleUser);
            ViewBag.Page = page;
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }
            ViewBag.Size = items;
            ViewBag.CurrentSize = size;
            page = page ?? 1;
            int pageSize = (size ?? 5);
            ViewBag.pageSize = pageSize;
            int pageNumber = (page ?? 1);
            int checkTotal = (int)(users.ToList().Count / pageSize) + 1;
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            // 4. Trả kết quả về Views
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV");
            ViewBag.MaTitleUser = new SelectList(db.TITLEUSERs, "MaTitleUser", "TenTitleUser");
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDUser,UserName,PassWord,MaTitleUser,MaGV,NgayTao")] User user, FormCollection f)
        {
            var password = GetMD5(f["Password"]);
            var checkpassword = GetMD5(f[""]);
            if (ModelState.IsValid)
            {
                user.NgayTao = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", user.MaGV);
            ViewBag.MaTitleUser = new SelectList(db.TITLEUSERs, "MaTitleUser", "TenTitleUser", user.MaTitleUser);
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", user.MaGV);
            ViewBag.MaTitleUser = new SelectList(db.TITLEUSERs, "MaTitleUser", "TenTitleUser", user.MaTitleUser);
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDUser,UserName,PassWord,MaTitleUser,MaGV,NgayTao")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGV = new SelectList(db.GIAOVIENs, "MaGV", "TenGV", user.MaGV);
            ViewBag.MaTitleUser = new SelectList(db.TITLEUSERs, "MaTitleUser", "TenTitleUser", user.MaTitleUser);
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}
