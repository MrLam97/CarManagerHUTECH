using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyXe.Models
{
    public class Tinhtrangsudungxe
    {
        public int _MAXE { get; set; }
        public string _BIENSO { get; set; }
        public string _NHANHIEU { get; set; }
        public string _TENTINHTRANG { get; set; }
        public string _TENTRANGTHAI { get; set; }
        public string _TENLOAIXE { get; set; }
        public string _NHIENLIEU { get; set; }
        public string _HOPSO { get; set; }
        public string _HINHXE { get; set; }

        public Tinhtrangsudungxe(int MAXE, string BIENSO, string NHANHIEU, string TENTINHTRANG,string TENTRANGTHAI, string TENLOAIXE, string NHIENLIEU, string HOPSO, string HINHXE)
        {
            _MAXE = MAXE;
            _BIENSO = BIENSO;
            _NHANHIEU = NHANHIEU;
            _TENTINHTRANG = TENTINHTRANG;
            _TENTRANGTHAI = TENTRANGTHAI;
            _TENLOAIXE = TENLOAIXE;
            _NHIENLIEU = NHIENLIEU;
            _HOPSO = HOPSO;
            _HINHXE = HINHXE;
        }
    }
}