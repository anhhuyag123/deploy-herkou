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
    public class HomeController : Controller
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        private List<SanPham> Laygamemoi(int count)
        {
            return db.SanPhams.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int? page)
        {
            int pagesize = 6;
            int pagenum = (page ?? 1);
            var gamemoi = Laygamemoi(6);
            return View(gamemoi.ToPagedList(pagenum, pagesize));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}