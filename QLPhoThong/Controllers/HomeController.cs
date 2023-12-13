using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLPhoThong.Controllers
{
    public class HomeController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult SearchByPhoneNumber()
        {
                return PartialView();
        }

        [HttpPost]
        public ActionResult SearchByPhoneNumber(string phoneNumber, string namhoc)
        {
            
            var hocSinh = from hs in db.HOCSINHs
                          from d in db.DIEMs
                          where d.MaHS == hs.MaHS
                          where hs.SDT == "0" + phoneNumber
                          where d.MaNH == namhoc
                          select d;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                TempData["SweetAlertMessage"] = "Vui lòng nhập số điện thoại";
                TempData["SweetAlertType"] = "error";
            }
            else if (hocSinh.Any())
            {
                return RedirectToAction("BangDiem","Home", new {id = hocSinh.FirstOrDefault().MaHS, manh = hocSinh.FirstOrDefault().MaNH});
            }
            else
            {
                TempData["SweetAlertMessage"] = "Không tìm thấy học sinh phù hợp";
                TempData["SweetAlertType"] = "error";
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BangDiem(string id, string manh)
        {
            var bangDiem = db.DIEMs.FirstOrDefault(bd => bd.MaHS == id && bd.MaNH == manh);
            if (bangDiem != null)
            {
                return View(bangDiem);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
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
        public ActionResult KetQuaHK1Partial(string id, string manh)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == manh).FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == manh).ToList();
            if (ModelState.IsValid)
            {
                if (diem.Any(item => item.DiemTB == null))
                {
                    DanhSachKetQua.TBMHK = null;
                    DanhSachKetQua.HocLuc = null;
                }
                else
                {
                    float diemtbm = (float)(diem.Sum(d => d.DiemTB) / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                    double diemToan = diem.FirstOrDefault(d => d.MaMH == 1)?.DiemTB ?? 0;
                    double diemVan = diem.FirstOrDefault(d => d.MaMH == 2)?.DiemTB ?? 0;
                    if (diemToan >= 8 && diem.All(item => item.DiemTB >= 6.5) && (diem.Sum(d => d.DiemTB) / 10) >= 8 || diemVan >= 8 && diem.All(item => item.DiemTB >= 6.5) && (diem.Sum(d => d.DiemTB) / 10) >= 8)
                    {
                        DanhSachKetQua.HocLuc = "Giỏi";
                    }
                    else if (diemToan >= 6.5 && diem.All(item => item.DiemTB >= 5) && (diem.Sum(d => d.DiemTB) / 10) >= 6.5 || diemVan >= 6.5 && diem.All(item => item.DiemTB >= 5) && (diem.Sum(d => d.DiemTB) / 10) >= 6.5)
                    {
                        DanhSachKetQua.HocLuc = "Khá";
                    }
                    else if (diemToan >= 5 && diem.All(item => item.DiemTB >= 3.5) && (diem.Sum(d => d.DiemTB) / 10) >= 5 || diemVan >= 5 && diem.All(item => item.DiemTB >= 3.5) && (diem.Sum(d => d.DiemTB) / 10) >= 5)
                    {
                        DanhSachKetQua.HocLuc = "Trung Bình";
                    }
                    else if (diemToan >= 3.5 && diem.All(item => item.DiemTB >= 2) && (diem.Sum(d => d.DiemTB) / 10) >= 3.5 || diemVan >= 3.5 && diem.All(item => item.DiemTB >= 2) && (diem.Sum(d => d.DiemTB) / 10) >= 3.5)
                    {
                        DanhSachKetQua.HocLuc = "Yếu";
                    }
                    else if (diemToan >= 0.5 && diem.All(item => item.DiemTB >= 0) && (diem.Sum(d => d.DiemTB) / 10) >= 0 || diemVan >= 0.5 && diem.All(item => item.DiemTB >= 0) && (diem.Sum(d => d.DiemTB) / 10) >= 0)
                    {
                        DanhSachKetQua.HocLuc = "Kém";
                    }
                    else
                    {
                        DanhSachKetQua.HocLuc = null;
                    }
                    db.Entry(DanhSachKetQua).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return PartialView(DanhSachKetQua);
        }
        public ActionResult KetQuaHK2Partial(string id, string manh)
        {
            KETQUAHOCKY DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == manh).FirstOrDefault();
            List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == manh).ToList();
            if (ModelState.IsValid)
            {
                if (diem.Any(item => item.DiemTB == null))
                {
                    DanhSachKetQua.TBMHK = null;
                    DanhSachKetQua.HocLuc = null;
                }
                else
                {
                    float diemtbm = (float)(diem.Sum(d => d.DiemTB) / 10);
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMHK = diemtbmRounded;
                    double diemToan = diem.FirstOrDefault(d => d.MaMH == 1)?.DiemTB ?? 0;
                    double diemVan = diem.FirstOrDefault(d => d.MaMH == 2)?.DiemTB ?? 0;
                    if (diemToan >= 8 && diem.All(item => item.DiemTB >= 6.5) && (diem.Sum(d => d.DiemTB) / 10) >= 8 || diemVan >= 8 && diem.All(item => item.DiemTB >= 6.5) && (diem.Sum(d => d.DiemTB) / 10) >= 8)
                    {
                        DanhSachKetQua.HocLuc = "Giỏi";
                    }
                    else if (diemToan >= 6.5 && diem.All(item => item.DiemTB >= 5) && (diem.Sum(d => d.DiemTB) / 10) >= 6.5 || diemVan >= 6.5 && diem.All(item => item.DiemTB >= 5) && (diem.Sum(d => d.DiemTB) / 10) >= 6.5)
                    {
                        DanhSachKetQua.HocLuc = "Khá";
                    }
                    else if (diemToan >= 5 && diem.All(item => item.DiemTB >= 3.5) && (diem.Sum(d => d.DiemTB) / 10) >= 5 || diemVan >= 5 && diem.All(item => item.DiemTB >= 3.5) && (diem.Sum(d => d.DiemTB) / 10) >= 5)
                    {
                        DanhSachKetQua.HocLuc = "Trung Bình";
                    }
                    else if (diemToan >= 3.5 && diem.All(item => item.DiemTB >= 2) && (diem.Sum(d => d.DiemTB) / 10) >= 3.5 || diemVan >= 3.5 && diem.All(item => item.DiemTB >= 2) && (diem.Sum(d => d.DiemTB) / 10) >= 3.5)
                    {
                        DanhSachKetQua.HocLuc = "Yếu";
                    }
                    else if (diemToan >= 0.5 && diem.All(item => item.DiemTB >= 0) && (diem.Sum(d => d.DiemTB) / 10) >= 0 || diemVan >= 0.5 && diem.All(item => item.DiemTB >= 0) && (diem.Sum(d => d.DiemTB) / 10) >= 0)
                    {
                        DanhSachKetQua.HocLuc = "Kém";
                    }
                    else
                    {
                        DanhSachKetQua.HocLuc = null;
                    }
                    db.Entry(DanhSachKetQua).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return PartialView(DanhSachKetQua);
        }
        public ActionResult BangdiemCaNam(string id, string manh)
        {
            List<BANGDIEMCANAM> bangdiem = db.BANGDIEMCANAMs.Where(pc => pc.MaHS == id && pc.MaNH == manh).ToList();
            foreach (var item in bangdiem)
            {
                List<DIEM> diem = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaNH == manh && pc.MaMH == item.MaMH).ToList();
                if (diem.Any(item1 => item1.DiemTB == null))
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

        public ActionResult KetQuaCaNam(string id, string manh)
        {
            KETQUACANAM DanhSachKetQua = db.KETQUACANAMs.Where(pc => pc.MaHS == id && pc.MaNH == manh).FirstOrDefault();
            List<BANGDIEMCANAM> diem = db.BANGDIEMCANAMs.Where(pc => pc.MaHS == id && pc.MaNH == manh).ToList();
            List<KETQUAHOCKY> ketquahocky = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaNH == manh).ToList();
            if (ModelState.IsValid)
            {
                if (diem.Any(item => item.DiemTBCN == null))
                {
                    DanhSachKetQua.TBMCN = null;
                    DanhSachKetQua.HocLuc = null;
                }
                else
                {
                    double diemtbhk1 = ketquahocky.FirstOrDefault(d => d.MaHK == "1")?.TBMHK ?? 0;
                    double diemtbhk2 = ketquahocky.FirstOrDefault(d => d.MaHK == "2")?.TBMHK ?? 0;
                    double diemtbm = (diemtbhk1 + (2 * diemtbhk2)) / 3;
                    string diemtbmFormatted = diemtbm.ToString("N1");
                    float diemtbmRounded = float.Parse(diemtbmFormatted);
                    DanhSachKetQua.TBMCN = diemtbmRounded;

                    double diemToan = diem.FirstOrDefault(d => d.MaMH == 1)?.DiemTBCN ?? 0;
                    double diemVan = diem.FirstOrDefault(d => d.MaMH == 2)?.DiemTBCN ?? 0;
                    if (diemToan >= 8 && diem.All(item => item.DiemTBCN >= 6.5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 8 || diemVan >= 8 && diem.All(item => item.DiemTBCN >= 6.5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 8)
                    {
                        DanhSachKetQua.HocLuc = "Giỏi";
                    }
                    else if (diemToan >= 6.5 && diem.All(item => item.DiemTBCN >= 5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 6.5 || diemVan >= 6.5 && diem.All(item => item.DiemTBCN >= 5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 6.5)
                    {
                        DanhSachKetQua.HocLuc = "Khá";
                    }
                    else if (diemToan >= 5 && diem.All(item => item.DiemTBCN >= 3.5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 5 || diemVan >= 5 && diem.All(item => item.DiemTBCN >= 3.5) && (diem.Sum(d => d.DiemTBCN) / 10) >= 5)
                    {
                        DanhSachKetQua.HocLuc = "Trung Bình";
                    }
                    else if (diemToan >= 3.5 && diem.All(item => item.DiemTBCN >= 2) && (diem.Sum(d => d.DiemTBCN) / 10) >= 3.5 || diemVan >= 3.5 && diem.All(item => item.DiemTBCN >= 2) && (diem.Sum(d => d.DiemTBCN) / 10) >= 3.5)
                    {
                        DanhSachKetQua.HocLuc = "Yếu";
                    }
                    else if (diemToan >= 0.5 && diem.All(item => item.DiemTBCN >= 0) && (diem.Sum(d => d.DiemTBCN) / 10) >= 0 || diemVan >= 0.5 && diem.All(item => item.DiemTBCN >= 0) && (diem.Sum(d => d.DiemTBCN) / 10) >= 0)
                    {
                        DanhSachKetQua.HocLuc = "Kém";
                    }
                    else
                    {
                        DanhSachKetQua.HocLuc = null;
                    }
                    db.Entry(DanhSachKetQua).State = EntityState.Modified;
                }
                db.SaveChanges();
                
                if(DanhSachKetQua.HocLuc == "Giỏi" && DanhSachKetQua.HanhKiem == "Tốt")
                {
                    DanhSachKetQua.XepLoai = "Học sinh giỏi";
                    
                }
                else if(DanhSachKetQua.HocLuc == "Giỏi" && DanhSachKetQua.HanhKiem == "Khá" || DanhSachKetQua.HocLuc == "Khá" && DanhSachKetQua.HanhKiem == "Khá" || DanhSachKetQua.HocLuc == "Khá" && DanhSachKetQua.HanhKiem == "Tốt")
                {
                    DanhSachKetQua.XepLoai = "Học sinh tiên tiến";
                }
                else if(DanhSachKetQua.HocLuc == "Giỏi" && DanhSachKetQua.HanhKiem == "Trung Bình" || DanhSachKetQua.HocLuc == "Khá" && DanhSachKetQua.HanhKiem == "Trung bình" || DanhSachKetQua.HocLuc == "Trung Bình" && DanhSachKetQua.HanhKiem == "Trung Bình" || DanhSachKetQua.HocLuc == "Trung Bình" && DanhSachKetQua.HanhKiem == "Tốt" || DanhSachKetQua.HocLuc == "Trung Bình" && DanhSachKetQua.HanhKiem == "Khá")
                {
                    DanhSachKetQua.XepLoai = "Học sinh trung bình";
                }
                else if(DanhSachKetQua.HocLuc == "Giỏi" && DanhSachKetQua.HanhKiem == "Yếu" || DanhSachKetQua.HocLuc == "Khá" && DanhSachKetQua.HanhKiem == "Yếu" || DanhSachKetQua.HocLuc == "Trung Bình" && DanhSachKetQua.HanhKiem == "Yếu" || DanhSachKetQua.HocLuc == "Yếu" && DanhSachKetQua.HanhKiem == "Yếu" || DanhSachKetQua.HocLuc == "Kém" && DanhSachKetQua.HanhKiem == "Yếu" || DanhSachKetQua.HocLuc == "Yếu" && DanhSachKetQua.HanhKiem == "Tốt" || DanhSachKetQua.HocLuc == "Kém" && DanhSachKetQua.HanhKiem == "Tốt")
                {
                    DanhSachKetQua.XepLoai = "Học sinh Yếu";
                }
                else
                {
                    DanhSachKetQua.XepLoai = null;
                }
                db.Entry(DanhSachKetQua).State = EntityState.Modified;
                db.SaveChanges();

                if(DanhSachKetQua.XepLoai == "Học sinh giỏi" || DanhSachKetQua.XepLoai == "Học sinh tiên tiến" || DanhSachKetQua.XepLoai == "Học sinh trung bình")
                {
                    DanhSachKetQua.TrangThai = "Lên lớp thẳng";
                }
                else if (DanhSachKetQua.XepLoai == "Học sinh Yếu")
                {
                    DanhSachKetQua.TrangThai = "Ở lại lớp";
                }
                else
                {
                    DanhSachKetQua.TrangThai = null;
                }
                db.Entry(DanhSachKetQua).State = EntityState.Modified;
                db.SaveChanges();
            }
            return PartialView(DanhSachKetQua);
        }
        public ActionResult DiemDanhPartial(string id, string manh)
        {
            var DiemDanh = db.DIEMDANHs.Where(pc => pc.MaHS == id && pc.MaNH == manh).FirstOrDefault();
            return PartialView(DiemDanh);
        }


        
    }
}