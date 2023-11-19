using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Controllers
{
    public class LoginController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        // GET: Post//Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            var Gv = db.Users.SingleOrDefault(d=> d.UserName == user.UserName && d.PassWord == user.PassWord && d.MaTitleUser == 2);
            var AD = db.Users.SingleOrDefault(d => d.UserName == user.UserName && d.PassWord == user.PassWord && d.MaTitleUser == 1);
          
            if (Gv != null)
            {
                Session["User"] = Gv;
                TempData["SweetAlertMessage"] = "Chào mừng " + Gv.GIAOVIEN.TenGV + ", chào mừng bạn đến với Sổ liên lạc điện tử";
                TempData["SweetAlertType"] = "success";
                return RedirectToAction("Index", "Home", new { area = "Teacher" });
            }
            else if (AD != null)
            {
                Session["UserAdmin"] = AD;
                TempData["SweetAlertMessage"] = "Chào mừng " + AD.GIAOVIEN.TenGV + ", chào mừng bạn đến với Sổ liên lạc điện tử";
                TempData["SweetAlertType"] = "success";
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else
            {
                TempData["SweetAlertMessage"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
                TempData["SweetAlertType"] = "error";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}