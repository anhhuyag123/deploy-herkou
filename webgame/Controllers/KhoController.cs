using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanGame.Areas.Admin.Controllers
{
    public class KhoController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/Kho/
        public ActionResult Index(int ? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var giaohang = from tt in data.DatHangs
                           where tt.DaGiao == false
                            || tt.HTGiaoHang == false
                          || tt.HTThanhToan == false
                           select tt;
            return View(giaohang.ToPagedList(pageNum, pagesize));   
        }
        public ActionResult giaohang(int id, string strUrl)
        {
            var Egiao = data.DatHangs.First(m => m.SoDH == id);
            Egiao.DaGiao = true;
            UpdateModel(Egiao);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult thanhtoan(int id, string strUrl)
        {
            var Etoan = data.DatHangs.First(m => m.SoDH == id);
            Etoan.HTThanhToan = true;
            UpdateModel(Etoan);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult htgiaohang(int id, string strUrl)
        {
            var Ehang = data.DatHangs.First(m => m.SoDH == id);
            Ehang.HTGiaoHang = true;
            UpdateModel(Ehang);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
        public ActionResult chinhsua(int id, string strUrl)
        {
            var Ehang = data.DatHangs.First(m => m.SoDH == id);
            Ehang.HTGiaoHang = false;
            Ehang.DaGiao = false;
            Ehang.HTThanhToan = false;
            UpdateModel(Ehang);
            data.SubmitChanges();
            return Redirect(strUrl);
        }
	}
}