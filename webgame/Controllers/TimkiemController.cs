using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;

namespace webgame.Controllers
{
    public class TimkiemController : Controller
    {
        // GET: Timkiem
        DataClasses1DataContext db = new DataClasses1DataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KetQuaTimKiem(string txttimkiem, int? page, string Stukhoa)
        {
            ViewBag.TuKhoa = txttimkiem;
            List<SanPham> listKQ = db.SanPhams.Where(n => n.TenSP.Contains(txttimkiem)).ToList();
            if (listKQ.Count == 0)
            {
                ViewBag.Thongbao = "Không có kết quả";
            }
            return View(listKQ.OrderBy(n => n.TenSP));
        }
    }
}