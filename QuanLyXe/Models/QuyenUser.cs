using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyXe.Models
{
    public class QuyenUser
    {
        public string _tendn { get; set; }
        public string _check { get; set; }

        public int _maquyen { get; set; }
        public string _tenquyen { get; set; }

        public QuyenUser(string tendn, string check, int maquyen, string tenquyen)
        {
            _tendn = tendn;
            _check = check;
            _maquyen = maquyen;
            _tenquyen = tenquyen;
        }
    }
}