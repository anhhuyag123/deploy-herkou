using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanGame.Areas.Admin.Controllers
{
    public class MapController : Controller
    {
        // GET: Admin/Map
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Map()
        {
            return View();
        }
        public ActionResult Thongbao()
        {
            return View();
        }
    }
}