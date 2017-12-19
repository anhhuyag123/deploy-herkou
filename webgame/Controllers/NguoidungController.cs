using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;

namespace webgame.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection col,KhachHang kh)
        {
            var hoten = col["HotenKH"];
            var taikhoan = col["Taikhoan"];
            var matkhau = col["Matkhau"];
            var diachi = col["diachi"];
            var sdt = col["sodienthoai"];
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Họ tên không được để trống";
            }
            if(String.IsNullOrEmpty(taikhoan))
            {
                ViewData["loi2"] = "Tài khoản không được để trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Mật khẩu không được để trống";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["loi4"] = "Địa chỉ không được để trống";
            }
            if (String.IsNullOrEmpty(sdt))
            {
                ViewData["loi5"] = "Cần phải có số điện thoại";
            }
            else
            {
                ViewBag.Thongbao = "Đăng nhập thành công";
                kh.TenDangNhap = taikhoan;
                kh.MatKhau = matkhau;
                kh.TenKhachHang = hoten;
                kh.SDT = sdt;
                kh.DiaChi = diachi;
                db.KhachHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection col)
        {
            var taikhoan = col["Taikhoan"];
            var matkhau = col["Matkhau"];
            if (String.IsNullOrEmpty(taikhoan))
            {
                ViewData["loi2"] = "Tài khoản không được để trống";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["loi3"] = "Mật khẩu không được để trống";
            }
            else
            {
                KhachHang kh = db.KhachHangs.FirstOrDefault(n => n.TenDangNhap == taikhoan && n.MatKhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                }
                else
                {
                    ViewBag.Thongbao = "Tên tài khoản hoặc mật khẩu không chính xác!";
                }
            }
            return View();
        }
    }
}