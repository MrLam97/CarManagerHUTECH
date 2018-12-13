using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using QuanLyXe.DAO;
using Newtonsoft.Json;

namespace QuanLyXe.Controllers
{
    public class XeController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();


        public ActionResult Thongtinxe(int? page)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var pageNum = (page ?? 1);
                    var pageSize = 9;
                    DateTime dt = DateTime.Now;
                    var tt = data.sp_Xedadat(dt,dt).ToList();
            List<Tinhtrangsudungxe> ttsd = new List<Tinhtrangsudungxe>();
            foreach(var item in tt)
            {
                Tinhtrangsudungxe ttsdx = new Tinhtrangsudungxe(item.MAXE,item.BIENSO, item.NHANHIEU, item.TENTINHTRANG, item.TENTRANGTHAI, item.TENLOAIXE, item.NHIENLIEU, item.HOPSO, item.HINHXE);
                ttsd.Add(ttsdx);
            }
            var ttcd = data.sp_Xechuadat(dt, dt).ToList();
            foreach (var item in ttcd)
            {
                Tinhtrangsudungxe ttsdx = new Tinhtrangsudungxe(item.MAXE, item.BIENSO, item.NHANHIEU, item.TENTINHTRANG, item.TENTRANGTHAI, item.TENLOAIXE, item.NHIENLIEU, item.HOPSO, item.HINHXE);
                ttsd.Add(ttsdx);
            }

            return View(ttsd.OrderBy(n=>n._MAXE).ToPagedList(pageNum, pageSize));
        }

        public ActionResult Chitietxe(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.Thongtinxes.SingleOrDefault(n => n.MAXE == id);
            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE",tt.MAXE);
            ViewBag.MaTT = new SelectList(data.TINHTRANGXEs.ToList().OrderBy(n => n.MATINHTRANG), "MATINHTRANG", "TENTINHTRANG",tt.MATINHTRANG);
            return View(tt);
        }

        [HttpPost]
        public ActionResult Chitietxe(int id, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            var user = (USER)Session["USER"]; if (user == null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(6, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.XEs.SingleOrDefault(n => n.MAXE == id);
            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE", tt.MAXE);
            ViewBag.MaTT = new SelectList(data.TINHTRANGXEs.ToList().OrderBy(n => n.MATINHTRANG), "MATINHTRANG", "TENTINHTRANG", tt.MATINHTRANG);

            tt.MAXE = Convert.ToInt32(fc["MaLX"]);
            tt.MATINHTRANG = Convert.ToInt32(fc["MaTT"]);

            if (fileUpload == null)
            {
                var tt1 = data.XEs.SingleOrDefault(n => n.MAXE == tt.MAXE);
                UpdateModel(tt1);
                data.SaveChanges();
                var tk = data.Thongtinxes.SingleOrDefault(n => n.MAXE == id);
                return View(tk);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhxe"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";

                        var tk1 = data.Thongtinxes.SingleOrDefault(n => n.MAXE == id);
                        return View(tk1);
                    }
                    else
                    {

                        tt.HINHXE = fileName;

                        var tt1 = data.XEs.SingleOrDefault(n => n.MAXE == tt.MAXE);
                        UpdateModel(tt1);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }

                var tk = data.Thongtinxes.SingleOrDefault(n => n.MAXE == id);
                return View(tk);
            }
        }

        public ActionResult Thongkethang(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tk = data.sp_TKThang(DateTime.Now.Month, DateTime.Now.Year, id).SingleOrDefault();
            ViewBag.thang = tk.THANG;
            ViewBag.nam = tk.NAM;
            ViewBag.sochuyen = tk.SOCHUYEN;
            ViewBag.sokm = tk.SOKM;

            var cttk = data.sp_CTTKThang(id, DateTime.Now.Month).OrderBy(n=>n.NGAYDI).ToList();
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (var item in cttk)
            {
                dataPoints.Add(new DataPoint("Ngày: "+Convert.ToString(item.NGAYDI.Value.Day+"-"+item.NGAYVE.Value.Day), Convert.ToDecimal(item.SOKMDI + item.SOKMVE)));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return PartialView(tk);
        }

        public ActionResult Hoatdongganday(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var hd = data.v_DatxeTaixe.Where(n => n.MAXE == id && n.MATRANGTHAI == 1 && n.NGAYVE.Value.Month==DateTime.Now.Month).OrderByDescending(n => n.NGAYDI).ToList().Take(8);
                    return PartialView(hd);
        }

        public ActionResult ThongtinXanglop(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.XANGLOPs.Where(n => n.MAXE == id).OrderByDescending(n => n.NGAYDO).ToList().Take(8);
            return PartialView(tt);
        }

        public ActionResult ThongtinSuachua(int id)
        {

            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.v_Suachua.Where(n => n.MAXE == id).OrderByDescending(n => n.NGAYKT).ToList().Take(8);
            return PartialView(tt);
        }

        public ActionResult ThongtinPhikhac(int id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(5, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");
            

            var tt = data.PHIKHACs.Where(n => n.MAXE == id).OrderByDescending(n => n.NGAYTHUCHIEN).ToList().Take(8);
            return PartialView(tt);
        }

        public ActionResult Themxe()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(6, user.TENDN);
            if(result==false)
                return RedirectToAction("Khongduocphep", "Home");

            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE");
            return View();

        }

        [HttpPost]
        public ActionResult Themxe(XE xe, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaLX = new SelectList(data.LOAIXEs.ToList().OrderBy(n => n.MALOAIXE), "MALOAIXE", "TENLOAIXE");

            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhxe"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {

                        xe.HINHXE = fileName;

                        xe.MATINHTRANG = 1;
                        xe.MALOAIXE = Convert.ToInt32(fc["MaLX"]);
                        xe.HANBAOHIEM = Convert.ToDateTime(fc["hanbaohiem"]);
                        xe.HANBAOHIEM = Convert.ToDateTime(fc["hanluuhanh"]);
                        data.XEs.Add(xe);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }
                return RedirectToAction("Thongtinxe");
            }
        }


    }
}