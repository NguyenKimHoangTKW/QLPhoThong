using QLPhoThong.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
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
        public ActionResult ThongTin(string id)
        {
            var thongtin = db.HOCSINHs.Where(pc => pc.MaHS == id);
            return View(thongtin.Single());
        }
        public ActionResult ThongTinGiaoVienPartial(string id)
        {
            var thongtin = db.LOPCHUNHIEMs.Where(pc => pc.MaLop == id);
            return PartialView(thongtin.Single());
        }

        public ActionResult BangdiemHK1Partial(string id)
        {
            var danhSachLop = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24").ToList();
            return PartialView(danhSachLop);
        }

        public ActionResult BangdiemHK2Partial(string id)
        {
            var danhSachLop = db.DIEMs.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24").ToList();
            return PartialView(danhSachLop);
        }
        public ActionResult KetQuaHocKy1Partial(string id)
        {
            var DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "1" && pc.MaNH == "NH23|24");
            return PartialView(DanhSachKetQua.FirstOrDefault());
        }

        public ActionResult KetQuaHocKy2Partial(string id)
        {
            var DanhSachKetQua = db.KETQUAHOCKies.Where(pc => pc.MaHS == id && pc.MaHK == "2" && pc.MaNH == "NH23|24");
            return PartialView(DanhSachKetQua.FirstOrDefault());
        }
        [HttpGet]
        public ActionResult DiemDanhPartial(string id)
        {
            var DiemDanh = db.DIEMDANHs.Where(pc => pc.MaHS == id);
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
                var student = db.HOCSINHs.Find(diemdanh.MaHS);
                string smsMessage = $"Thông báo điểm danh - Học sinh {student.TenHS}  " +
                    $"| Vắng có phép : {student.DIEMDANHs.First().NghiCoPhep} " +
                    $"| Vắng không phép : {student.DIEMDANHs.First().NghiKhongPhep} " +
                    $"| Bỏ tiết : {student.DIEMDANHs.First().BoTiet} " +
                    $"| Ghi chú : {student.DIEMDANHs.FirstOrDefault().GhiChu}" +
                    $"| Thời gian gửi SMS : {DateTime.Now.ToString("dddd, dd MMMM yyyy")}";
                string phoneNumber = student.SDT;
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
    }
}