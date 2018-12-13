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

namespace QuanLyXe.Controllers
{
    public class TTTaixeController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        public ActionResult Thongtintaixe(int? page)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(7, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var pageNum = (page ?? 1);
            var pageSize = 9;
            var tt = data.TAIXEs.ToList();
            return View(tt.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Chitiettaixe(string id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(7, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.TAIXEs.SingleOrDefault(n => n.TENDNTX == id);
            return View(tt);
        }

        public ActionResult HoatdongTX(string id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(7, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var hd = data.v_DatxeTaixe.Where(n => n.TENDNTX == id && n.MATRANGTHAI == 1).OrderByDescending(n => n.NGAYDI).ToList().Take(8);
            return PartialView(hd);
        }

        public ActionResult Lichlamviec(string id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(7, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var pc = data.Phancongtxes.Where(n => n.TENDNTX == id && n.NGAYBATDAU.Month == DateTime.Now.Month).ToList();
            ViewBag.thang = DateTime.Now.Month;
            return View(pc);
        }

        public ActionResult Thongtindatxe(string id)
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(7, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            var tt = data.spNgaylamtx(id).OrderByDescending(n => n.NGAYDI).ToList();
            return View(tt);
        }

        public ActionResult Themtaixe()
        {
            var user = (USER)Session["USER"]; if(user==null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(8, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Themtaixe(TAIXE tx, FormCollection fc, HttpPostedFileBase fileUpload)
        {

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
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhtaixe"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {
                        tx.CHANDUNG = fileName;
                        tx.MACHUCVU = 5;
                        tx.NAMSINHTX = Convert.ToDateTime(fc["ngaysinh"]);
                        tx.NGAYCAPBL = Convert.ToDateTime(fc["ngaycapbl"]);
                        tx.NGAYHETHANBL = Convert.ToDateTime(fc["ngayhethanbl"]);

                        var ten = fc["TENDNTX"];
                        var tt = data.TAIXEs.SingleOrDefault(n => n.TENDNTX == ten);
                        if (tt != null)
                        {
                            ViewBag.Thongbao1 = "Tên đăng nhập đã tồn tại";
                            return View();
                        }
                        else
                        {
                            data.TAIXEs.Add(tx);
                            data.SaveChanges();
                        }
                        fileUpload.SaveAs(path);
                    }
                }
                return RedirectToAction("Thongtintaixe");
            }
        }

        public ActionResult PhancongTX()
        {
            var user = (USER)Session["USER"]; if (user == null) return RedirectToAction("Dangnhap", "Home");
            var result = new CheckRole().Kiemtraquyen(1021, user.TENDN);
            if (result == false)
                return RedirectToAction("Khongduocphep", "Home");

            ViewBag.taixe = new SelectList(data.TAIXEs.ToList(),"TENDNTX","HOTENTX");
            ViewBag.xe = new SelectList(data.XEs.ToList(), "MAXE", "BIENSO");
            return View();
        }

        [HttpPost]
        public JsonResult PhancongTX(string ngaybd, string ngaykt, string xe, string taixe, string ghichu)
        {
            ViewBag.taixe = new SelectList(data.TAIXEs.ToList(), "TENDNTX", "HOTENTX");
            ViewBag.xe = new SelectList(data.XEs.ToList(), "MAXE", "BIENSO");
            var Ngaybd = Convert.ToDateTime(ngaybd);
            var Ngaykt = Convert.ToDateTime(ngaykt);
            var nv = (USER)Session["User"];
            var maxe = Convert.ToInt32(xe);

            var kt = data.PHANCONGs.Where(n=>n.NGAYBATDAU==Ngaybd && n.NGAYKETTHUC==Ngaykt && n.TENDN==nv.TENDN).Count();
            var ctpc = new CHITIETPHANCONG();

            if (kt == 0)
            {
                var phancong = new PHANCONG();
                phancong.NGAYBATDAU = Ngaybd;
                phancong.NGAYKETTHUC = Ngaykt;
                phancong.TENDN = nv.TENDN;
                data.PHANCONGs.Add(phancong);
                data.SaveChanges();
                
                ctpc.NGAYBATDAU = Ngaybd;
                ctpc.TENDNTX = taixe;
                ctpc.MAXE = maxe;
                ctpc.GHICHUPC = ghichu;
                data.CHITIETPHANCONGs.Add(ctpc);
                data.SaveChanges();
            }
            else
            {
                
                ctpc.NGAYBATDAU = Ngaybd;
                ctpc.TENDNTX = taixe;
                ctpc.MAXE = maxe;
                ctpc.GHICHUPC = ghichu;
                data.CHITIETPHANCONGs.Add(ctpc);
                data.SaveChanges();
            }

            return Json(new
            {
                status=true,
                JsonRequestBehavior.AllowGet
            });
        }
    }
}