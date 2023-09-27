using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Areas.Teacher.Controllers
{
    public class LopController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/Lop
        public ActionResult Index()
        {
            var LogInResult = (User)Session["User"];
            if (LogInResult != null)
            {
                int maGV = LogInResult.MaGV;

                // Truy vấn lấy danh sách lớp của giáo viên có MaGV tương ứng
                var danhSachLop = (from l in db.LOPs
                                   from lcn in db.LOPCHUNHIEMs
                                   from gv in db.GIAOVIENs
                                   from hk in db.HOCKies
                                   where lcn.MaGV == gv.MaGV
                                   where lcn.MaLop == l.MaLop
                                   where hk.MaHky == lcn.MaHKy
                                   where gv.MaGV == maGV
                                   select lcn
                                   ).ToList();
                ViewBag.TotalTeacher = danhSachLop.Count;
                //select * 
                //from Lop l, GIAOVIEN gv, LOPCHUNHIEM lcn , HOCKY hk
                //where lcn.MaGV = gv.MaGV
                //and lcn.MaLop = l.MaLop
                //and hk.MaHky = lcn.MaHKy
                // Chuyển danh sách lớp đến view để hiển thị
                return View(danhSachLop);
            }
            else
                return RedirectToAction("Login", "Account"); 
            }
        }
    }
