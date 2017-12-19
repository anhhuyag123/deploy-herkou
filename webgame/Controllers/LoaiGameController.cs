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
    public class LoaiGameController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/LoaiGame/
        public ActionResult Index(int? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.LoaiGames.ToList().OrderBy(n => n.MaLoai).ToPagedList(pagenumber, pagesize));
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
        public ActionResult Them(FormCollection collection, LoaiGame lg)
        {
            var CB_Name = collection["txttenloai"];
            if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên loại  không được để trống ";
            }
            else
            {
                lg.TenLoai = CB_Name;
                data.LoaiGames.InsertOnSubmit(lg);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Them();
        }
        public ActionResult Editlg(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_lg = data.LoaiGames.First(m => m.MaLoai == id);
            return View(E_lg);
        }
        [HttpPost]
        public ActionResult Editlg(int id, FormCollection collection)
        {
            var CB_Name = collection["txttenlg"];
            var Elg = data.LoaiGames.First(m => m.MaLoai == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên không được để trống";
            }

            else
            {
                Elg.TenLoai = CB_Name;
                UpdateModel(Elg);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editlg(id);
        }
        public ActionResult Deletelg(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_nsx = data.LoaiGames.First(m => m.MaLoai == id);
            return View(D_nsx);
        }
        [HttpPost]
        public ActionResult Deletelg(int id, FormCollection collection)
        {

            var lg = data.LoaiGames.First(m => m.MaLoai == id);
            data.LoaiGames.DeleteOnSubmit(lg);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
	}
}