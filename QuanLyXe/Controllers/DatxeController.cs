using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using QuanLyXe.DAO;

namespace QuanLyXe.Controllers
{
    public class DatxeController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        
        public ActionResult Dexuatxe()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(1, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var lx = data.LOAIXEs.ToList();
            return View(lx);
        }
        [HttpPost]
        public ActionResult Dexuatxe(FormCollection fc)
        {
            var lx = data.LOAIXEs.ToList();
            var ng = (USER)Session["USER"];
            if (ng == null)
            {
                return RedirectToAction("Dangnhap", "Home");
            }
            DATXE dx = new DATXE();
                dx.TENDN = ng.TENDN;
                dx.NGAYDX = DateTime.Now;
                dx.MALOAIXE = Convert.ToInt32(fc["Chonlx"]);
                dx.NGAYDI = Convert.ToDateTime(fc["Ngaydi"]);
                dx.NGAYVE = Convert.ToDateTime(fc["Ngayve"]);

                dx.GIODI = fc["giodi"];
                dx.GIOVE = fc["giove"];
                dx.CHUTRIDOAN = fc["chutri"];
                dx.SOLUONGDOAN= Convert.ToInt32(fc["soluong"]);
                dx.LOTRINHDI = fc["lotrinhdi"];
                dx.SOKMDI = Convert.ToInt32(fc["sokmdi"]);
                dx.LOTRINHVE = fc["lotrinhve"];
                dx.SOKMVE = Convert.ToInt32(fc["sokmve"]);
                dx.MATRANGTHAI = 5;
                data.DATXEs.Add(dx);
                data.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        public ActionResult Lichsudatnv()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(4, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            
            var ls = data.ThongtinDatxes.Where(n => n.TENDN == user.TENDN).ToList();
            return View(ls);
        }

        public ActionResult Lichsudattk()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(1017, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            
            var tt = data.ThongtinDatxes.Where(n => n.MAKHOA == user.MAKHOA).ToList();
            return View(tt);
        }

        public ActionResult Lichsudatqt()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(1018, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            
            var tt = data.ThongtinDatxes.ToList();
            return View(tt);
        }

        public ActionResult Thongtindexuat()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(2, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            
            var tt = data.ThongtinDatxes.Where(n => n.MAKHOA == user.MAKHOA && n.MATRANGTHAI == 5).ToList();
            return View(tt);
        }

        public ActionResult Duyetdexuat(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(2, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 5 && n.MADX == id).SingleOrDefault();
            return View(tt);
        }

        [HttpPost]
        public ActionResult Duyetdexuat(int id, FormCollection fc)
        {
            var tt = data.DATXEs.Where(n => n.MADX == id).SingleOrDefault();
            tt.MATRANGTHAI = 4;
            data.SaveChanges();
            return RedirectToAction("Thongtindexuat", "Datxe");
        }

        [HttpPost]
        public JsonResult Huydexuat(int id)
        {
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            dx.MATRANGTHAI = 6;
            UpdateModel(dx);
            data.SaveChanges();
            var host = HttpContext.Request.Url.Authority;
            var url = "http://"+ host;
            return Json(new {
                status = true,
                url=url,
                JsonRequestBehavior.AllowGet
            });
        }

        #region Đặt xe quản trị
        public ActionResult Thongtindatxe()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(3, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            
            var tt = data.ThongtinDatxes.Where(n => n.MATRANGTHAI == 4).ToList();
            return View(tt);
        }

        public ActionResult Xulidatxe(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(3, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var dx = data.ThongtinDatxes.SingleOrDefault(n => n.MADX == id);
            return View(dx);
        }

        [HttpPost]
        public ActionResult Xulidatxe(int id, FormCollection fc)
        {
            ViewBag.Thongbao = "Bạn chưa chọn xe.";
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            var ttdx = data.ThongtinDatxes.SingleOrDefault(n => n.MADX == id);
            if (fc["chon"] != null)
            {
                int maxec= Convert.ToInt32(fc["chon"]);
                dx.MAXE = maxec;
                dx.GHICHUDUYET = fc["message"];
                dx.MATRANGTHAI = 3;
                data.SaveChanges();

                int thang = DateTime.Now.Month;
                int nam = DateTime.Now.Year;

                var tk = data.THONGKETHANGs.Where(n => n.MAXE == maxec && n.THANG == thang && n.NAM == nam).Count();

                if (tk == 0)
                {
                    THONGKETHANG tkt = new THONGKETHANG();
                    tkt.MAXE = maxec;
                    tkt.THANG = thang;
                    tkt.NAM = nam;
                    tkt.SOCHUYEN = 0;
                    tkt.SOKM = 0;
                    data.THONGKETHANGs.Add(tkt);
                    data.SaveChanges();
                }

                return RedirectToAction("Thongtindatxe");
            }
            else
                return View(ttdx);
        }

        public ActionResult Xechuadat(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(3, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var dx = data.ThongtinDatxes.SingleOrDefault(n => n.MADX == id);
            var xe = data.sp_Xechuadat(dx.NGAYDI, dx.NGAYVE).ToList();
            return PartialView(xe);
        }

        public ActionResult Xedadat(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(3, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var dx = data.ThongtinDatxes.SingleOrDefault(n => n.MADX == id);
            var xe = data.sp_Xedadat(dx.NGAYDI, dx.NGAYVE).OrderBy(n => n.MALOAIXE).ToList();
            return PartialView(xe);
        }

        [HttpPost]
        public JsonResult Xoadatxe(int id)
        {
            bool status = true;
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            if (dx.MATRANGTHAI != 1 && dx.MATRANGTHAI != 2)
            {
                data.DATXEs.Remove(dx);
                data.SaveChanges();
            }
            else
                status = false;
            
            var host = HttpContext.Request.Url.Authority;
            var url = "http://" + host;
            return Json(new {
                status,
                url=url,
                JsonRequestBehavior.AllowGet
            });
        }

        #endregion

        public ActionResult Suadatxe(int id)
        {
            var user = (USER)Session["USER"]; if (user == null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(2, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var dx = data.ThongtinDatxes.SingleOrDefault(n => n.MADX == id);
            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE",dx.MALOAIXE);
            return View(dx);
        }

        [HttpPost]
        public ActionResult Suadatxe(int id, FormCollection fc)
        {
            
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE", dx.MALOAIXE);
            dx.MATRANGTHAI = 5;
            dx.MALOAIXE = Convert.ToInt32(fc["MaLX"]);
            UpdateModel(dx);
            data.SaveChanges();
            return RedirectToAction("Thongtindexuat");
        }
    }
}