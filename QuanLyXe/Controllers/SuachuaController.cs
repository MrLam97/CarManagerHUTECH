using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using System.IO;
using QuanLyXe.DAO;

namespace QuanLyXe.Controllers
{
    public class SuachuaController : Controller
    {
        // GET: Suachua
        QUANLYXEEntities data = new QUANLYXEEntities();
        public ActionResult Xanglop()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(15, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.XANGLOPs.OrderByDescending(n => n.NGAYDO).ToList();
            return View(tt);
        }

        public ActionResult Suachuax()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(16, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.v_Suachua.OrderByDescending(n => n.NGAYKT).ToList();
            return View(tt);
        }

        public ActionResult Phikhac()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(14, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.PHIKHACs.OrderByDescending(n => n.NGAYTHUCHIEN).ToList();
            return View(tt);
        }

        public ActionResult Themxanglop()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(10, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            ViewBag.xe = new SelectList(data.XEs.ToList().OrderBy(n => n.MALOAIXE), "MAXE", "BIENSO");
            return View();
        }

        [HttpPost]
        public ActionResult Themxanglop(XANGLOP x, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            ViewBag.xe = new SelectList(data.XEs.ToList().OrderBy(n => n.MALOAIXE), "MAXE", "BIENSO");

            x.NGAYDO = Convert.ToDateTime(fc["ngay"]);
            x.MAXE = Convert.ToInt32(fc["xe"]);


            if (fileUpload == null)
            {
                data.XANGLOPs.Add(x);
                data.SaveChanges();
                return RedirectToAction("Xanglop");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhhoadon"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        x.ANHHOADON = fileName;
                        data.XANGLOPs.Add(x);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }
                return RedirectToAction("Xanglop");
            }
        }


        public ActionResult Themphikhac()
        {
            var user = (USER)Session["USER"]; if (user == null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(12, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            ViewBag.xe = new SelectList(data.XEs.ToList().OrderBy(n => n.MALOAIXE), "MAXE", "BIENSO");
            return View();
        }

        [HttpPost]
        public ActionResult Themphikhac(PHIKHAC x, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            ViewBag.xe = new SelectList(data.XEs.ToList().OrderBy(n => n.MALOAIXE), "MAXE", "BIENSO");

            x.NGAYTHUCHIEN = Convert.ToDateTime(fc["ngay"]);
            x.MAXE = Convert.ToInt32(fc["xe"]);


            if (fileUpload == null)
            {
                data.PHIKHACs.Add(x);
                data.SaveChanges();
                return RedirectToAction("Phikhac");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhhoadon"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        x.HOADONPHI = fileName;
                        data.PHIKHACs.Add(x);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }
                return RedirectToAction("Phikhac");
            }
        }
    }
}