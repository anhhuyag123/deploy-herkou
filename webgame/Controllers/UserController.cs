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
    public class UserController : Controller
    {
        // GET: User
        DataClasses1DataContext db = new DataClasses1DataContext();
        private List<SanPham> Laygamemoi(int count)
        {
            return db.SanPhams.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index(int ? page)
        {
            int pagesize = 6;
            int pagenum = (page ?? 1);
            var gamemoi = Laygamemoi(12);
            return View(gamemoi.ToPagedList(pagenum,pagesize));
        }
        public ActionResult loaigame()
        {
            var loaigame = from lg in db.LoaiGames select lg;
            return PartialView(loaigame);
        }
        public ActionResult DongMay()
        {
            var hemay = from hm in db.HeMays select hm;
            return PartialView(hemay);
        }
       //tao kieu may tao PartViriew ma
        public ActionResult NhaSanXuat()
        {
            var nhasanxuat = from nsx in db.NhaSanXuats select nsx;
            return PartialView(nhasanxuat);
        }
        public ActionResult listspcungloai(int id)
        {
            var nhasanxuat = from nsx in db.SanPhams
                             where nsx.MaNhaSanXuat == id
                             select nsx;
            return PartialView(nhasanxuat);
        }
        public ActionResult listloaisp(int id)
        {
            var loai = from l in db.SanPhams
                             where l.MaLoai == id
                             select l;
            return PartialView(loai);
        }
        public ActionResult Dexuat()
        {
            var gamemoi = Laygamemoi(3);
            return PartialView(gamemoi);
        }
        public ActionResult listloaigame()
        {
            var hemay = from lg in db.HeMays select lg;
            return PartialView(hemay);
        }
        public ActionResult laysanphammoi(int id)
        {
            var nhasanxuat = from nsx in db.SanPhams
                             where nsx.MaLoai == id
                             select nsx;
            return PartialView(nhasanxuat);
        }

        public ActionResult chitiet(int id)
        {
            var chitiet = from ct in db.SanPhams
                          where ct.MaSP == id
                          select ct;
            return View(chitiet.Single());
        }
        public ActionResult Sptheodongmay(int id)
        {
            var dongmay = from dm in db.SanPhams
                          where dm.Madong == id
                          select dm;
            return View(dongmay);
        }
        public ActionResult Sptheonhasanxuat(int id)
        {
            var nhasanxuat = from nsx in db.SanPhams
                             where nsx.MaNhaSanXuat == id
                             select nsx;
            return View(nhasanxuat);
        }
        public ActionResult Sptheoloaigame(int id)
        {
            var loaigame = from lg in db.SanPhams
                           where lg.MaLoai == id
                           select lg;
            return View(loaigame);
        }
        public ActionResult Sptieubieu()
        {
            var gamemoi = Laygamemoi(12);
            return View(gamemoi);
        }
        public ActionResult Spkhuyenmai(int ? page)
        {
            int pagesize = 3;
            int pagenum = (page ?? 1);
            var gamemoi = Laygamemoi(6);
            return View(gamemoi.ToPagedList(pagenum, pagesize));
        }
        public ActionResult huongdanmuahang()
        {
            return View();
        }
        public ActionResult lienhe()
        {
            return View();
        }
    }
}