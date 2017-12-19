﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webgame.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanGame.Areas.Admin.Controllers
{
    public class DaXongController : Controller
    {
        //
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: /Admin/DaXong/
        public ActionResult Index(int ? page)
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("Login", "Admin");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var nhanhang = from tt in data.DatHangs
                           where tt.DaGiao == true
                         && tt.HTGiaoHang == true
                       && tt.HTThanhToan == true
                           select tt;
            return View(nhanhang.ToPagedList(pageNum, pagesize));
        }
	}
}