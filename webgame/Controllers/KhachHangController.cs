using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanGame.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/KhachHang/
        public ActionResult Index(int? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagenumber = (page ?? 1);
            int pagesize = 5;
            return View(data.KhachHangs.ToList().OrderBy(n => n.MaKhachHang).ToPagedList(pagenumber, pagesize));
        }
        public ActionResult Deletekh(int id)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            var D_kh = data.KhachHangs.First(m => m.MaKhachHang == id);
            return View(D_kh);
        }
        [HttpPost]
        public ActionResult Deletekh(int id, FormCollection collection)
        {

            var kh = data.KhachHangs.First(m => m.MaKhachHang == id);
            data.KhachHangs.DeleteOnSubmit(kh);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
	}
}