using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webgame.Models;

namespace webgame.Models
{
    public class GioHang
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        public string iSoDDH { set; get; }
        public int iMaGame { set; get; }
        public string iTenGame { set; get; }
        public string anh { set; get; }
        public int isoluong { set; get; }
        public double idongia { set; get; }
        public double thanhtien
        {
            get
            {
                return isoluong * idongia;
            }
        }
        public GioHang(int Magame)
        {
            iMaGame = Magame;
            SanPham game = data.SanPhams.Single(n => n.MaSP == iMaGame);
            iTenGame = game.TenSP;
            anh = game.HinhAnh;
            if (game.KhuyenMai == true)
            {
                idongia = double.Parse(game.GiaSale.ToString());
            }
            else {
                idongia = double.Parse(game.GiaBan.ToString());
            }
            
            isoluong = 1;
        }
    }
}