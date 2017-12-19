using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;

namespace webgame.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /GioHang/
        public List<GioHang> laygiohang()
        {
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang == null)
            {
                listgiohang = new List<GioHang>();
                Session["GioHang"] = listgiohang;
            }

            return listgiohang;
        }
        public ActionResult ThemGioHang(int imagame, string strUrl)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.Find(n => n.iMaGame == imagame);
            if (sanpham == null)
            {
                sanpham = new GioHang(imagame);
                listgiohang.Add(sanpham);
                return Redirect(strUrl);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strUrl);
            }
        }
        //tong so luong
        private int tongsoluong()
        {
            int iTongsoluong = 0;
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang != null)
            {
                iTongsoluong = listgiohang.Sum(n => n.isoluong);
            }
            return iTongsoluong;
        }
        private double tongtien()
        {
            double itongtien = 0;
            List<GioHang> listgiohang = Session["GioHang"] as List<GioHang>;
            if (listgiohang != null)
            {
                itongtien = listgiohang.Sum(n => n.thanhtien);
            }
            return itongtien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> listgiohang = laygiohang();
            if (listgiohang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return View(listgiohang);
        }
        public ActionResult GioHangPartinal()
        {
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int iMaSP)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaGame == iMaSP);
            if (sanpham != null)
            {
                listgiohang.RemoveAll(n => n.iMaGame == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (listgiohang.Count == 0)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int iMaSP, FormCollection f)
        {
            List<GioHang> listgiohang = laygiohang();
            GioHang sanpham = listgiohang.SingleOrDefault(n => n.iMaGame == iMaSP);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaHang()
        {
            List<GioHang> listgiohang = laygiohang();
            listgiohang.Clear();
            return RedirectToAction("GioHang", "GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "Nguoidung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioHang", "GioHang");
            }
            List<GioHang> listgiohang = laygiohang();
            ViewBag.Tongsoluong = tongsoluong();
            ViewBag.Tongtien = tongtien();
            return View(listgiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DatHang dh = new DatHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<GioHang> gh = laygiohang();
            dh.NgayDatHang = DateTime.Now;
            dh.DaGiao = false;
            var ngaygiao = String.Format("{0:dd/MM/YYYY}", collection["NgayGiaoHang"]);
            dh.NgayGiaoHang = DateTime.Parse(ngaygiao);
            dh.TenNguoiNhan = kh.TenKhachHang;
            dh.DiaChiNhan = kh.DiaChi;
            dh.DienThoaiNhan = kh.SDT;
            dh.HTThanhToan = false;
            dh.HTGiaoHang = false;
            dh.MaKhachHang = kh.MaKhachHang;
            data.DatHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                ChiTietDat ctdh = new ChiTietDat();
                ctdh.SoDH = dh.SoDH;
                ctdh.MaSP = item.iMaGame;
                ctdh.SoLuong = item.isoluong;
                ctdh.DonGia = (decimal)item.idongia;
                decimal ttien = item.isoluong * (decimal)item.idongia;
                ctdh.ThanhTien = (int)ttien;
                data.ChiTietDats.InsertOnSubmit(ctdh);

            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}