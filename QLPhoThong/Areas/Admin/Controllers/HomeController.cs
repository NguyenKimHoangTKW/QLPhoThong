﻿using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        
        // GET: Admin/Home
        public ActionResult Index()
        {
            IList<HOCSINH> liststudent = db.HOCSINHs.ToList();
            IList<MONHOC> listgiaovien = db.MONHOCs.ToList();
            IList<LOP> listlop = db.LOPs.ToList();
            ViewBag.TotalStudent = liststudent.Count;
            ViewBag.TotalTeacher = listgiaovien.Count;
            ViewBag.TotalClass = listlop.Count;
            return View();
        }
    }
}