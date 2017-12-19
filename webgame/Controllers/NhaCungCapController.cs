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
    public class NhaCungCapController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/NhaCungCap/
        public ActionResult Index(int? page)
        {
                if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
                {
                    return RedirectToAction("Login", "Admin");
                }
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.NhaSanXuats.ToList().OrderBy(n => n.MaNhaSanXuat).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Them()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Them(FormCollection collection, NhaSanXuat sx)
        {
            var CB_Name = collection["txttennhasanxuat"];
            if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên nhà sản xuất  không được để trống ";
            }
            else
            {
                sx.TenNhaSanXuat = CB_Name;
                data.NhaSanXuats.InsertOnSubmit(sx);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Them();
        }
        public ActionResult Editnhasx(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }   
            var E_nsx= data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            return View(E_nsx);
        }
        [HttpPost]
        public ActionResult Editnhasx(int id, FormCollection collection)
        {
            var CB_Name = collection["txttennhasanxuat"];
            var Enhasx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên không được để trống";
            }

            else
            {
                Enhasx.TenNhaSanXuat = CB_Name;
                UpdateModel(Enhasx);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editnhasx(id);
        }
        public ActionResult Deletenhasx(int id)
        {
                if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
                {
                    return RedirectToAction("Login", "Admin");
                }
            var D_nsx = data.NhaSanXuats.First(m => m.MaNhaSanXuat == id);
            return View(D_nsx);
        }
        [HttpPost]
        public ActionResult Deletenhasx(int id, FormCollection collection)
        {

            var nhasx = data.NhaSanXuats.First(m => m.MaNhaSanXuat== id);
            data.NhaSanXuats.DeleteOnSubmit(nhasx);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
	}
}