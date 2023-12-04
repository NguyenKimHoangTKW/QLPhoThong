using QLPhoThong.App_Start;
using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace QLPhoThong.Areas.Teacher.Controllers
{

    public class ThongTinController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/ThongTin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongTinGiaoVienPartial(string id)
        {
            var thongtin = db.LOPCHUNHIEMs.Where(pc => pc.MaLop == id);
            return PartialView(thongtin.Single());
        }
        public ActionResult BangdiemHK1Partial(string id, string manh)
        {
            var bangdiem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == manh).ToList();
            return PartialView(bangdiem);
        }
        public ActionResult BangdiemHK2Partial(string id, string manh)
        {
            var bangdiem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == manh).ToList();
            return PartialView(bangdiem);
        }
        public ActionResult BangDiemCaNamPartial(string id, string manh)
        {
            List<BANGDIEMCANAM> bangdiem = db.BANGDIEMCANAMs.Where(pc => pc.MaHS == id  && pc.MaNH == manh).ToList();
            foreach(var item in bangdiem)
            {
                List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaNH == manh && pc.MaMH == item.MaMH).ToList();
                if(diem.Any(item1 => item1.DiemTB == null))
                {
                    item.DiemTBCN = null;
                }
                else
                {
                    float diemtbm = (float)(diem.Sum(d => d.DiemTB) / 2);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    item.DiemTBCN = diemtbmRounded;
                }
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return PartialView(bangdiem);
        }
        public ActionResult KetQuaHocKy1Partial(string id, string manh)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == manh).FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == manh).ToList();
            if (ModelState.IsValid)
            {
                if (diem.Any(item => item.DiemTB == null))
                {
                    DanhSachKetQua.TBMHK = null;
                }
                else
                {
                    float diemtbm = (float)(diem.Sum(d => d.DiemTB) / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                }
                db.Entry(DanhSachKetQua).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView(DanhSachKetQua);
        }

        public ActionResult KetQuaHocKy2Partial(string id, string manh)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == manh).FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == manh).ToList();
            if (ModelState.IsValid)
            {
                if (diem.Any(item => item.DiemTB == null))
                {
                    DanhSachKetQua.TBMHK = null;
                }
                else
                {
                    float diemtbm = (float)(diem.Sum(d => d.DiemTB) / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                }
                db.Entry(DanhSachKetQua).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView(DanhSachKetQua);
        }

        public ActionResult KetQuaCaNam(string id, string manh)
        {
            var KetQuaCaNam = db.KETQUACANAMs.Where(pc => pc.MaHS == id && pc.MaNH == manh);
            return PartialView(KetQuaCaNam.Single());
        }
        [HttpGet]
        public ActionResult DiemDanhPartial(string id, string manh)
        {
            var DiemDanh = db.DIEMDANHs.Where(pc => pc.MaHS == id && pc.MaNH == manh);
            return PartialView(DiemDanh.Single());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult DiemDanhPartial(DIEMDANH diemdanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemdanh).State = EntityState.Modified;
                db.SaveChanges();
            }

            if (Request.UrlReferrer != null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return View();
            }
        }

    }
}