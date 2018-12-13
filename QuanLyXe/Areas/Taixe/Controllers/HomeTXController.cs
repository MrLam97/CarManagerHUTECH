using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;

namespace QuanLyXe.Areas.Taixe.Controllers
{
    public class HomeTXController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        // GET: Taixe/HomeTX
        public ActionResult Index()
        {
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
                var nd = data.TAIXEs.Where(n => n.MACHUCVU == 5).SingleOrDefault(n => n.TENDNTX == tendn && n.MATKHAUTX == matkhau);
                if (nd != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["USER"] = nd;
                    return RedirectToAction("Index", "HomeTX");
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

        public ActionResult ThongbaoTX()
        {
            var user = (TAIXE)Session["User"];
            var tt = data.spNgaylamtx(user.TENDNTX).ToList();
            ViewBag.dem = data.spNgaylamtx(user.TENDNTX).Count();
            return View(tt);
        }

    }
}