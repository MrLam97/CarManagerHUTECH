using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyXe.Models;

namespace QuanLyXe.Areas.Taixe.Controllers
{
    public class DatxeTXController : Controller
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        // GET: Taixe/DatxeTX
        public ActionResult Lichlamviec()
        {
            var user = (TAIXE)Session["User"];
            var pc = data.Phancongtxes.Where(n => n.TENDNTX == user.TENDNTX).OrderByDescending(n=>n.NGAYBATDAU).ToList();
            return View(pc);
        }

        public ActionResult Thongtindatxe()
        {
            var user = (TAIXE)Session["User"];
            var tt = data.spNgaylamtx(user.TENDNTX).OrderByDescending(n=>n.NGAYDI).ToList();
            return View(tt);
        }

        [HttpPost]
        public ActionResult Capnhat(int id)
        {
            var user = (TAIXE)Session["User"];
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            dx.MATRANGTHAI = 2;
            data.SaveChanges();
            var ttx = data.spNgaylamtx(user.TENDNTX).OrderByDescending(n => n.NGAYDI).ToList();
            return View("Thongtindatxe",ttx);
        }

        [HttpPost]
        public ActionResult CapnhatKT(int id)
        {
            var user = (TAIXE)Session["User"];
            var dx = data.DATXEs.SingleOrDefault(n => n.MADX == id);
            dx.MATRANGTHAI = 1;
            data.SaveChanges();
            var ttx = data.spNgaylamtx(user.TENDNTX).OrderByDescending(n => n.NGAYDI).ToList();
            return View("Thongtindatxe", ttx);
        }
    }
}