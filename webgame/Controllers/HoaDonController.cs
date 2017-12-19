using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanGame.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/HoaDon/
        public ActionResult Index(int ? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var hd = from tt in data.ChiTietDats
                     select tt;
            return View(hd.ToPagedList(pageNum, pagesize));
        }
	}
}