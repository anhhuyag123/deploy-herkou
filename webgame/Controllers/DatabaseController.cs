using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;
namespace webgame.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/
       DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/Database/
        public ActionResult Databse(int ?page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagenumber=(page ??1);
            int pagesize=5;
            return View(data.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pagenumber,pagesize));
        }
      [HttpGet]
        public ActionResult Editgane(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_game = data.SanPhams.First(m => m.MaSP == id);
            ViewBag.Masx = new SelectList(data.NhaSanXuats.ToList().OrderBy(n => n.TenNhaSanXuat), "MaNhaSanXuat", "TenNhaSanXuat", E_game.MaNhaSanXuat);
            ViewBag.Maloai = new SelectList(data.LoaiGames.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", E_game.MaLoai);
            ViewBag.Mahm = new SelectList(data.HeMays.ToList().OrderBy(n => n.Tendong), "Madong", "Tendong", E_game.Madong);
            return View(E_game);
        }
        [HttpPost]
        public ActionResult Editgane(int id, FormCollection collection)
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
            var Egame = data.SanPhams.First(m => m.MaSP == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên không được để trống";
            }

            else
            {
            
                Egame.TenSP = CB_Name;
                Egame.MaNhaSanXuat = CB_HangGame;
                Egame.MaLoai = CB_Theloai;
                Egame.HinhAnh = CB_anh;
                Egame.MoTa = CB_motamh;
                if (collection["txttieubieu"] != null)
                    Egame.TieuBieu = true;
                else
                    Egame.TieuBieu = false;
                if (collection["txtkhuyemai"] != null)
                    Egame.KhuyenMai = true;
                else
                    Egame.KhuyenMai = false;
                Egame.HesoKM = CB_hesokm;
                Egame.Video = CB_video;
                Egame.GiaBan = CB_Giaban;
                Egame.NgayCapNhat = CB_ngaycapnhat;
                Egame.CauHinh = CB_cauhinh;
                Egame.Madong = CB_dongmay;
                UpdateModel (Egame);
                data.SubmitChanges();
                return RedirectToAction("Databse");
            }
            return this.Editgane(id);
        }
        public ActionResult Deletegame(int id)
        {
            var D_game = data.SanPhams.First(m => m.MaSP == id);
            return View(D_game);
        }
        [HttpPost]
        public ActionResult Deletegame(int id, FormCollection collection)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var SanPhams = data.SanPhams.First(m => m.MaSP == id);
            data.SanPhams.DeleteOnSubmit(SanPhams);
            data.SubmitChanges();
            return RedirectToAction("Databse");
        }
        public ActionResult Detailsgame(int id)
        {
            var Details_game= data.SanPhams.Where(m => m.MaSP == id).First();
            return View(Details_game);
        }
	}

}

