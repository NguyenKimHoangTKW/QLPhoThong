using QLPhoThong.App_Start;
using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{
    [TeacherAuthorize]
    public class HomeController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/Home
        public ActionResult Index()
        {
             var user = Session["User"] as QLPhoThong.Models.User; 
            List<PHANCONG> danhSachLop = db.PHANCONGs.Where(pc => pc.MaGV == user.MaGV).OrderBy(pc => pc.LOP.TenLop).ToList();
            ViewBag.TongLopPhanCong = danhSachLop.Count;
            return View();
        }
    }
}