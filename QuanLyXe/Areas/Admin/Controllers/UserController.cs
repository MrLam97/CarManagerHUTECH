using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;
using PagedList;
using System.IO;

namespace QuanLyXe.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        // GET: Admin/User
        public ActionResult Taikhoan(int ? page)
        {
            var pageNum = (page ?? 1);
            var pageSize = 9;
            var tt = data.TAIXEs.ToList();
            var tk = data.v_Taikhoan.Where(n=>n.MACHUCVU!=1).ToList();
            return View(tk.ToPagedList(pageNum, pageSize));
        }
        
        public ActionResult Chitiettaikhoan(string id)
        {
            var tt = data.v_Taikhoan.SingleOrDefault(n => n.TENDN == id);
            ViewBag.machucvu = new SelectList(data.CHUCVUs.ToList(), "MACHUCVU", "TENCHUCVU",tt.MACHUCVU);
            ViewBag.makhoa = new SelectList(data.KHOAs.ToList(), "MAKHOA", "TENKHOA",tt.MAKHOA);
            return View(tt);
        }

        [HttpPost]
        public ActionResult Chitiettaikhoan(string id, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            var tt = data.USERS.SingleOrDefault(n => n.TENDN == id);
            ViewBag.machucvu = new SelectList(data.CHUCVUs.ToList(), "MACHUCVU", "TENCHUCVU", tt.MACHUCVU);
            ViewBag.makhoa = new SelectList(data.KHOAs.ToList(), "MAKHOA", "TENKHOA", tt.MAKHOA);

            
            tt.MAKHOA = Convert.ToInt32(fc["makhoa"]);
            tt.MACHUCVU = Convert.ToInt32(fc["machucvu"]);
            tt.NAMSINH = Convert.ToDateTime(fc["ngaysinh"]);

            if (fileUpload == null)
            {
                var tt1 = data.USERS.SingleOrDefault(n=>n.TENDN==tt.TENDN);
                UpdateModel(tt1);
                data.SaveChanges();
                var tk = data.v_Taikhoan.SingleOrDefault(n => n.TENDN == id);
                return View(tk);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhtaikhoan"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";

                        var tk1 = data.v_Taikhoan.SingleOrDefault(n => n.TENDN == id);
                        return View(tk1);
                    }
                    else
                    {

                        tt.AVATAR = fileName;

                        var tt1 = data.USERS.SingleOrDefault(n => n.TENDN == tt.TENDN);
                        UpdateModel(tt1);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }

                var tk = data.v_Taikhoan.SingleOrDefault(n => n.TENDN == id);
                return View(tk);
            }
        }


        public ActionResult ThemUser()
        {
            ViewBag.machucvu = new SelectList(data.CHUCVUs.ToList(), "MACHUCVU", "TENCHUCVU");
            ViewBag.makhoa = new SelectList(data.KHOAs.ToList(), "MAKHOA", "TENKHOA");
            return View();
        }

        [HttpPost]
        public ActionResult ThemUser(USER user, FormCollection fc, HttpPostedFileBase fileUpload)
        {
            ViewBag.machucvu = new SelectList(data.CHUCVUs.ToList(), "MACHUCVU", "TENCHUCVU");
            ViewBag.makhoa = new SelectList(data.KHOAs.ToList(), "MAKHOA", "TENKHOA");

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
                    var path = Path.Combine(Server.MapPath("~/Content/Hinhtaikhoan"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        return View();
                    }
                    else
                    {

                        user.AVATAR = fileName;
                        
                        user.MACHUCVU = Convert.ToInt32(fc["machucvu"]);
                        user.MAKHOA = Convert.ToInt32(fc["makhoa"]);
                        data.USERS.Add(user);
                        data.SaveChanges();
                        fileUpload.SaveAs(path);
                    }
                }
                return RedirectToAction("Taikhoan");
            }
        }

        public ActionResult QuyenhanUser()
        {
            var user = data.USERS.Where(n=>n.MACHUCVU!=1).ToList();
            return View(user);
        }

        [HttpPost]
        public JsonResult Quyenhan(string tendn)
        {
            var quyen = data.sp_Quyenuserksd(tendn).ToList();
            var quyenuser = data.sp_Quyenusersd(tendn).ToList();
            List<QuyenUser> lstQuyen = new List<QuyenUser>();
            foreach (var item in quyenuser)
            {
                QuyenUser list = new QuyenUser(tendn, "checked", item.MAQUYEN, item.TENQUYEN);
                lstQuyen.Add(list);
            }

            foreach (var item in quyen)
            {
                QuyenUser list = new QuyenUser(tendn, "", item.MAQUYEN, item.TENQUYEN);
                lstQuyen.Add(list);
            }


            return Json(new
            {
                lstQuyen,
                JsonRequestBehavior.AllowGet
            });
        }

        public ActionResult Danhsachquyen()
        {
            var q = data.QUYENHANs.ToList();
            return PartialView(q);
        }

        [HttpPost]
        public JsonResult Capnhatquyen(string tendn, int maquyen)
        {
            var pq = data.PHANQUYENs.Where(n => n.TENDN == tendn && n.MAQUYEN == maquyen).SingleOrDefault();
            if (pq == null)
            {
                PHANQUYEN phanquyen = new PHANQUYEN();
                phanquyen.TENDN = tendn;
                phanquyen.MAQUYEN = maquyen;
                data.PHANQUYENs.Add(phanquyen);
                data.SaveChanges();
            }
            else
            {
                data.PHANQUYENs.Remove(pq);
                data.SaveChanges();
            }


            var quyen = data.sp_Quyenuserksd(tendn).ToList();
            var quyenuser = data.sp_Quyenusersd(tendn).ToList();
            List<QuyenUser> lstQuyen = new List<QuyenUser>();
            foreach (var item in quyenuser)
            {
                QuyenUser list = new QuyenUser(tendn, "checked", item.MAQUYEN, item.TENQUYEN);
                lstQuyen.Add(list);
            }

            foreach (var item in quyen)
            {
                QuyenUser list = new QuyenUser(tendn, "", item.MAQUYEN, item.TENQUYEN);
                lstQuyen.Add(list);
            }


            return Json(new
            {
                lstQuyen,
                JsonRequestBehavior.AllowGet
            });
        }

        
    }
}