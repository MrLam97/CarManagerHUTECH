using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;

namespace QuanLyXe.Controllers
{
    public class HomeController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();

        public ActionResult Index()
        {
            var user = (USER)Session["USER"]; if (user == null) return RedirectToAction("Dangnhap", "Home");
            return View();
        }
        
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                var nd = data.USERS.SingleOrDefault(n => n.TENDN == tendn && n.MATKHAU == matkhau && n.MACHUCVU!=1);
                if (nd != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["USER"] = nd;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Dangxuat()
        {
            Session["USER"] = null;
            return RedirectToAction("Dangnhap");
        }

        public ActionResult Menu()
        {
            var nd = (USER)Session["USER"];
            var Quyen = data.PHANQUYENs.Where(n => n.TENDN == nd.TENDN);
            return PartialView(Quyen);
        }

        public ActionResult Thongbao()
        {
            var nd = (USER)Session["USER"];
            var Quyen = data.PHANQUYENs.Where(n => n.TENDN == nd.TENDN);
            foreach (var item in Quyen)
            {
                if (item.MAQUYEN == 17)
                {
                    var tt = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 3).ToList();
                    ViewBag.dem = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 3).Count();
                    ViewBag.quyen = item.MAQUYEN;
                    return PartialView(tt);
                }
                if (item.MAQUYEN == 18)
                {
                    var tt = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 5 &&n.MAKHOA==nd.MAKHOA).ToList();
                    ViewBag.dem = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 5).Count();
                    ViewBag.quyen = item.MAQUYEN;
                    return PartialView(tt);
                }
                if (item.MAQUYEN == 19)
                {
                    var tt = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 4).ToList();
                    ViewBag.dem = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 4).Count();
                    ViewBag.quyen = item.MAQUYEN;
                    return PartialView(tt);
                }
            }
            return PartialView();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Khongduocphep()
        {
            return View();
        }
    }
}