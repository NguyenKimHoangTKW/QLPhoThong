using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{

    public class QuanLyHanhKiemController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/QuanLyHanhKiem
        public ActionResult DanhGiaHanhKiem(string id)
        {
            var danhgia = db.DANHGIAHANHKIEMs.Where(dghk => dghk.MaHS == id);
            return View(danhgia.Single());
        }
    }
}