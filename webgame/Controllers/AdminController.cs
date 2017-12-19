using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;

namespace webgame.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
         DataClasses1DataContext data = new DataClasses1DataContext();
         public ActionResult Index()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
           {
               return RedirectToAction("Login", "Admin");
           }
            return View();
        }
            [HttpPost]
         public ActionResult tktg(string txtdatestart, string txtdateend,FormCollection collection, string Stukhoa)
         {

             string dateStart = txtdatestart;
             string dateend = txtdateend;

             DateTime datestart = DateTime.Parse(dateStart);
             DateTime dateEnd = DateTime.Parse(dateend);
             List<ChiTietDat> listKQ = data.ChiTietDats.Where(n => n.DatHang.NgayDatHang >= datestart && n.DatHang.NgayDatHang <= dateEnd).ToList();
             if (listKQ.Count == 0)
             {
                 ViewBag.Thongbao = "Không có dữ liệu";
             }
             return View(listKQ.OrderBy(n => n.DatHang.NgayDatHang));
         }
          public ActionResult Login()
        {
            return View();
        }
         [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendangnhap = collection["ID"];
            var matkhau = collection["Password"];
            if (string.IsNullOrEmpty(tendangnhap))
            {
                ViewData["Loi1"] = "Phải nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                LoginAdmin ad = data.LoginAdmins.SingleOrDefault(n => n.TenDangNhap == tendangnhap && n.MatKhau == matkhau);
                if (ad != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TenDangNhap"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
	}	
}