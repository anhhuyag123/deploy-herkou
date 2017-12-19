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
    public class DongMayController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/DongMay/
        public ActionResult Index(int? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.HeMays.ToList().OrderBy(n => n.Madong).ToPagedList(pagenumber, pagesize));
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
        public ActionResult Them(FormCollection collection, HeMay hm)
        {
            var CB_Name = collection["txttendongmay"];
            if (string.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi1"] = " Tên Dòng Máy không được để trống ";
            }
            else
            {
                hm.Tendong = CB_Name;
                data.HeMays.InsertOnSubmit(hm);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Them();
        }
        public ActionResult Editdm(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var E_dm = data.HeMays.First(m => m.Madong == id);
            return View(E_dm);
        }
        [HttpPost]
        public ActionResult Editdm(int id, FormCollection collection)
        {
            var CB_Name = collection["txttendong"];
            var Edm = data.HeMays.First(m => m.Madong == id);
            if (String.IsNullOrEmpty(CB_Name))
            {
                ViewData["Loi"] = "Tên không được để trống";
            }

            else
            {
                Edm.Tendong = CB_Name;
                UpdateModel(Edm);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Editdm(id);
        }
        public ActionResult Deletedm(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_dm = data.HeMays.First(m => m.Madong == id);
            return View(D_dm);
        }
        [HttpPost]
        public ActionResult Deletedm(int id, FormCollection collection)
        {

            var dmay = data.HeMays.First(m => m.Madong == id);
            data.HeMays.DeleteOnSubmit(dmay);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

	}
}