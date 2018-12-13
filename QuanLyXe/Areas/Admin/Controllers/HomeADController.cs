using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;

namespace QuanLyXe.Areas.Admin.Controllers
{
    public class HomeADController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();

        // GET: Admin/HomeAD
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
                var nd = data.USERS.SingleOrDefault(n => n.TENDN == tendn && n.MATKHAU == matkhau && n.MACHUCVU==1);
                if (nd != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["ADMIN"] = nd;
                    return RedirectToAction("Index", "HomeAD");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Dangxuat()
        {
            Session["ADMIN"] = null;
            return RedirectToAction("Dangnhap");
        }
    }
}