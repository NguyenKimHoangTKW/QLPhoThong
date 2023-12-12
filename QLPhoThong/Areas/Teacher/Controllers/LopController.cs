using QLPhoThong.App_Start;
using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace QLPhoThong.Areas.Teacher.Controllers
{
    [TeacherAuthorize]
    public class LopController : Controller
    {
        private diemhsEntities db = new diemhsEntities();
        // GET: Teacher/Lop
        public ActionResult Index(string id)
        {
            var danhSachLop = db.PHANCONGs.Where(pc => pc.MaGV == id).OrderBy(pc => pc.LOP.TenLop).ToList();
            return View(danhSachLop);
        }

        public ActionResult LopChuNhiem(string id)
        {
            var danhSachLop = db.LOPCHUNHIEMs.Where(pc => pc.MaGV == id).ToList();
            return View(danhSachLop);
        }

        public ActionResult DanhSachHocSinh(string id, string manh)
        {
            var user = Session["User"] as QLPhoThong.Models.User;
            var danhSachLop = (from hs in db.HOCSINHs
                              from lcn in db.LOPCHUNHIEMs
                              where hs.MaLop == lcn.MaLop
                              where hs.MaLop == id
                              where lcn.MaNH == manh
                               select hs).OrderBy(h=>h.TenHS).ToList();
            List<KETQUACANAM> kqcnHSG = (from hs in db.HOCSINHs
                                      from ketquacn in db.KETQUACANAMs
                                      from lcn in db.LOPCHUNHIEMs
                                      where lcn.MaLop == hs.MaLop
                                      where hs.MaHS == ketquacn.MaHS
                                      where lcn.MaNH == manh
                                      where hs.MaLop == id
                                      where lcn.MaGV == user.MaGV
                                      where ketquacn.XepLoai == "Học sinh giỏi"
                                      select ketquacn
                                      ).ToList();

            List<KETQUACANAM> kqcnHSTT = (from hs in db.HOCSINHs
                                         from ketquacn in db.KETQUACANAMs
                                         from lcn in db.LOPCHUNHIEMs
                                         where lcn.MaLop == hs.MaLop
                                         where hs.MaHS == ketquacn.MaHS
                                         where lcn.MaNH == manh
                                         where hs.MaLop == id
                                         where lcn.MaGV == user.MaGV
                                         where ketquacn.XepLoai == "Học sinh tiên tiến"
                                          select ketquacn
                                      ).ToList();

            List<KETQUACANAM> kqcnHSTB = (from hs in db.HOCSINHs
                                          from ketquacn in db.KETQUACANAMs
                                          from lcn in db.LOPCHUNHIEMs
                                          where lcn.MaLop == hs.MaLop
                                          where hs.MaHS == ketquacn.MaHS
                                          where lcn.MaNH == manh
                                          where hs.MaLop == id
                                          where lcn.MaGV == user.MaGV
                                          where ketquacn.XepLoai == "Học sinh trung bình"
                                          select ketquacn
                                      ).ToList();
            List<KETQUACANAM> kqcnHSYeu = (from hs in db.HOCSINHs
                                          from ketquacn in db.KETQUACANAMs
                                          from lcn in db.LOPCHUNHIEMs
                                          where lcn.MaLop == hs.MaLop
                                          where hs.MaHS == ketquacn.MaHS
                                          where lcn.MaNH == manh
                                          where hs.MaLop == id
                                          where lcn.MaGV == user.MaGV
                                          where ketquacn.XepLoai == "Học sinh Yếu"
                                           select ketquacn
                                      ).ToList();

            List<KETQUACANAM> kqcnHSLL = (from hs in db.HOCSINHs
                                           from ketquacn in db.KETQUACANAMs
                                           from lcn in db.LOPCHUNHIEMs
                                           where lcn.MaLop == hs.MaLop
                                           where hs.MaHS == ketquacn.MaHS
                                           where lcn.MaNH == manh
                                           where hs.MaLop == id
                                           where lcn.MaGV == user.MaGV
                                           where ketquacn.TrangThai == "Lên lớp thẳng"
                                          select ketquacn
                                      ).ToList();

            List<KETQUACANAM> kqcnHSOLL = (from hs in db.HOCSINHs
                                          from ketquacn in db.KETQUACANAMs
                                          from lcn in db.LOPCHUNHIEMs
                                          where lcn.MaLop == hs.MaLop
                                          where hs.MaHS == ketquacn.MaHS
                                          where lcn.MaNH == manh
                                          where hs.MaLop == id
                                          where lcn.MaGV == user.MaGV
                                          where ketquacn.TrangThai == "Ở lại lớp"
                                           select ketquacn
                                      ).ToList();
            ViewBag.TongHocSinh = danhSachLop.Count;
            ViewBag.TongHocSinhHSGioi = kqcnHSG.Count;
            ViewBag.TongHocSinhHSTienTien = kqcnHSTT.Count;
            ViewBag.TongHocSinhHSTrungBinh = kqcnHSTB.Count;
            ViewBag.TongHocSinhHSYeu = kqcnHSYeu.Count;

            ViewBag.TongHocSinhLenLop = kqcnHSLL.Count;
            ViewBag.TongHocSinhOLaiLop = kqcnHSOLL.Count;
            return View(danhSachLop);
        }
        public ActionResult DanhSachHocSinhPhuTrachDay(string idlop, int idmh,string manh, string hocky = "1")
        {
            var danhSachLop = (from hs in db.HOCSINHs
                               from d in db.DIEMs
                               where d.MaHS == hs.MaHS
                               where hs.MaLop == idlop
                               where d.MaHK == hocky
                               where d.MaMH == idmh
                               where d.MaNH == manh
                               select d).OrderBy(l => l.HOCSINH.TenHS).ToList();
          
            List<DIEM> diemduoiTB = (from hs in db.HOCSINHs
                                     from d in db.DIEMs
                                     where d.MaHS == hs.MaHS
                                     where hs.MaLop == idlop
                                     where d.MaHK == hocky
                                     where d.MaMH == idmh
                                     where d.DiemTB < 5
                                     where d.MaNH == manh
                                     select d).ToList();
            ViewBag.TongHocSinh = danhSachLop.Count;

            ViewBag.TongHocSinhDuoiTB = diemduoiTB.Count;
            ViewBag.hocky = new SelectList(db.HOCKies, "MaHky", "TenHky");
            return View(danhSachLop);
        }
        public ActionResult DanhGiaHanhKiem( string idlop,string manh, string hocky = "1")
        {
            var danhSachLop = (from hs in db.HOCSINHs
                               from kqhk in db.KETQUAHOCKies
                               where hs.MaHS == kqhk.MaHS
                               where hs.MaLop == idlop
                               where kqhk.MaHK == hocky
                               where kqhk.MaNH ==manh
                               select kqhk
                               ).OrderBy(l => l.HOCSINH.TenHS).ToList();

            ViewBag.TongHocSinh = danhSachLop.Count;
            ViewBag.hocky = new SelectList(db.HOCKies, "MaHky", "TenHky");
            return View(danhSachLop);
        }
        public ActionResult DanhGiaHanhKiemCaNam(string idlop, string manh)
        {
            var danhSachLop = (from hs in db.HOCSINHs
                               from kqhk in db.KETQUACANAMs
                               where hs.MaHS == kqhk.MaHS
                               where hs.MaLop == idlop
                               where kqhk.MaNH == manh
                               select kqhk
                               ).OrderBy(l => l.HOCSINH.TenHS).ToList();

            ViewBag.TongHocSinh = danhSachLop.Count;
            return View(danhSachLop);
        }
        public ActionResult DiemDanhbyDanhGiaHanhKiem(string mahs, string manh)
        {
            var danhSachLop = db.DIEMDANHs.Where(dd => dd.MaHS == mahs && dd.MaNH == manh).FirstOrDefault();
            return PartialView(danhSachLop);
        }
        public ActionResult XemNhanXetGVBMByDanhGiaHanhKiem(string mahs, string manh, string hocky)
        {
            var danhSachLop = db.DIEMs.Where(dd => dd.MaHS == mahs && dd.MaNH == manh && dd.MaHK == hocky).ToList();
            return PartialView("XemNhanXetGVBMByDanhGiaHanhKiem", danhSachLop);
        }
        [HttpGet]
        public ActionResult NhanXetHanhKiem(string mahs, string manh, string hocky)
        {
            var danhSachLop = db.KETQUAHOCKies.Where(dd => dd.MaHS == mahs && dd.MaNH == manh && dd.MaHK == hocky).FirstOrDefault();
            return PartialView("NhanXetHanhKiem", danhSachLop);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NhanXetHanhKiem(KETQUAHOCKY kqhk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kqhk).State = EntityState.Modified;
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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult NhanXetHanhKiemCaNam(KETQUACANAM kqcn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kqcn).State = EntityState.Modified;
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
        public ActionResult XemBangDiemHocSinh(string id, string manh)
        {
            var danhSachLop = db.DIEMs.FirstOrDefault(pc => pc.MaHS == id && pc.MaNH == manh);
            return View(danhSachLop);
        }

        [HttpGet]
        public ActionResult SendSMSPartial(string id, string manh)
        {
            var DiemDanh = db.DIEMDANHs.Where(pc => pc.MaHS == id && pc.MaNH == manh) ;
            return PartialView("SendSMSPartial", DiemDanh.Single());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SendSMSPartial(DIEMDANH diemdanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemdanh).State = EntityState.Modified;
                db.SaveChanges();
                var student = db.HOCSINHs.Find(diemdanh.MaHS);
                string smsMessage = $"Thông báo điểm danh - Học sinh {student.HoVaTenDem + student.TenHS} " +
                    $"| Vắng có phép : {student.DIEMDANHs.First().NghiCoPhep} " +
                    $"| Vắng không phép : {student.DIEMDANHs.First().NghiKhongPhep} " +
                    $"| Bỏ tiết : {student.DIEMDANHs.First().BoTiet} " +
                    $"| Ghi chú : {student.DIEMDANHs.FirstOrDefault().GhiChu}" +
                    $"| Thời gian gửi SMS : {DateTime.Now.ToString("dddd, dd MMMM yyyy")}";
                string phoneNumber = "+84" + student.SDT;
                SendSms(phoneNumber, smsMessage);
                ViewBag.SuccessMessage = "Cập nhật điểm danh thành công!";
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

        private void SendSms(string toPhoneNumber, string message)
        {
            var accountSid = System.Configuration.ConfigurationManager.AppSettings["TwilioAccountSID"];
            var authToken = System.Configuration.ConfigurationManager.AppSettings["TwilioAuthToken"];
            var twilioPhoneNumber = System.Configuration.ConfigurationManager.AppSettings["TwilioPhoneNumber"];
            TwilioClient.Init(accountSid, authToken);
            var messageOptions = new CreateMessageOptions(
                new Twilio.Types.PhoneNumber(toPhoneNumber))
            {
                From = new Twilio.Types.PhoneNumber(twilioPhoneNumber),
                Body = message
            };
            var messageResponse = MessageResource.Create(messageOptions);
            Console.WriteLine(messageResponse.Sid);
        }

        [HttpGet]
        public ActionResult ThongTin(string id)
        {
            var thongtin = db.HOCSINHs.Where(pc => pc.MaHS == id);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc");
            return PartialView("ThongTin", thongtin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ThongTin([Bind(Include = "MaHS,TenHS,NgaySinh,DiaChi,SDT,MaLop,GioiTinh,TrangThai,idDanToc,Thumb, iDHS")] HOCSINH hOCSINH, HttpPostedFileBase Thumb, FormCollection form)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    if (Thumb != null)
                    {
                        string _Head = Path.GetFileNameWithoutExtension(Thumb.FileName);
                        string _Tail = Path.GetExtension(Thumb.FileName);
                        string fullLink = _Head + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + _Tail;
                        string _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), fullLink);
                        Thumb.SaveAs(_path);
                        hOCSINH.Thumb = fullLink;
                        _path = Path.Combine(Server.MapPath("~/Areas/Admin/Images/ImagesStudent"), form["oldimage"]);

                        if (System.IO.File.Exists(_path))
                            System.IO.File.Delete(_path);

                    }
                    else
                        hOCSINH.Thumb = form["oldimage"];
                    db.Entry(hOCSINH).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    ViewBag.Message = "không thành công!!";
                }
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs.OrderBy(l => l.MaLop), "MaLop", "TenLop", hOCSINH.MaLop);
            ViewBag.idDanToc = new SelectList(db.DanTocs, "idDanToc", "TenDanToc", hOCSINH.idDanToc);
            return View(hOCSINH);
        }
    }
}
