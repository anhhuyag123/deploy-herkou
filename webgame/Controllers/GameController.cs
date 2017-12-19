using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;
namespace WebBanGame.Areas.Admin.Controllers
{
    public class GameController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/Game/
        [HttpGet]
        public ActionResult Them()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            ViewBag.Masx = new SelectList(data.NhaSanXuats.ToList().OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.Maloai = new SelectList(data.LoaiGames.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.Mahm = new SelectList(data.HeMays.ToList().OrderBy(n => n.Tendong), "Madong", "Tendong");
            return View();
        }
        [HttpPost]
        public ActionResult Them(FormCollection collection, SanPham sp)
        {
            var CB_Name = collection["txttengame"];
            var CB_HangGame = int.Parse(collection["Masx"]);
            var CB_Theloai = int.Parse(collection["Maloai"]);
            var CB_anh = collection["txthinhanh"];
            var CB_motamh = collection["txtgioithieu"];
            var CB_video = collection["txtvideo"];
            var CB_Giaban = decimal.Parse(collection["txtgia"]);
            var CB_ngaycapnhat = DateTime.Parse(collection["txtngaydang"]);
            var CB_cauhinh = collection["txtcauhinh"];
            var CB_dongmay = int.Parse(collection["Mahm"]);
            var CB_hesokm = float.Parse(collection["txthskm"]);
            if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên  không được để trống ";
            }
            else
            {
                sp.TenSP = CB_Name;
                sp.MaNhaSanXuat = CB_HangGame;
                sp.MaLoai = CB_Theloai;
                sp.HinhAnh = CB_anh;
                sp.MoTa = CB_motamh;
                if (collection["txttieubieu"] != null)
                    sp.TieuBieu = true;
                else
                    sp.TieuBieu = false;
                if (collection["txtkhuyemai"] != null)
                    sp.KhuyenMai = true;
                else
                    sp.KhuyenMai = false;
                sp.HesoKM = CB_hesokm;
                sp.Video = CB_video;
                sp.GiaBan = CB_Giaban;
                sp.NgayCapNhat = CB_ngaycapnhat;
                sp.CauHinh = CB_cauhinh;
                sp.Madong = CB_dongmay;
                data.SanPhams.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("Them");
            }
            return this.Them();
        }



    }
}