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
        public ActionResult Index()
        {
            return View();
        }
        // GET: Post//Login
        [HttpPost]
        public ActionResult Index(User user)
        {
            var username = user.UserName;
            var password = user.PassWord;
            var isCheckLogin = user.MaTitleUser;
            var isLogin = db.Users.SingleOrDefault(x => x.UserName.Equals(username) && x.PassWord.Equals(password));
            var isLoginAdmin = db.Users.SingleOrDefault(x => x.UserName.Equals(username) && x.PassWord.Equals(password) && x.MaTitleUser == 1);
            var isLoginTeacher = db.Users.SingleOrDefault(x => x.UserName.Equals(username) && x.PassWord.Equals(password) && x.MaTitleUser == 2);
            if (isLogin != null)
            {
                Session["User"] = isLogin;
                if (isLoginAdmin != null)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else if (isLoginTeacher != null)
                {
                    return RedirectToAction("Index", "Home", new { area = "Teacher" });

                }
                else
                {
                    return View("Index");
                }
                
            }
            else
            {
                return View("Index");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}