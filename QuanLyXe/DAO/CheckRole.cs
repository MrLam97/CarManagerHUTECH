using QuanLyXe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyXe.DAO
{
    public class CheckRole
    {
        QUANLYXEEntities data = new QUANLYXEEntities();
        public bool Kiemtraquyen(int id,string tendn)
        {
           var quyen = data.PHANQUYENs.Where(n => n.TENDN == tendn).ToList();
            int dem = 0;
            foreach (var item1 in quyen)
            {
                if (item1.MAQUYEN != id)
                {
                    dem++;
                    if (dem == data.PHANQUYENs.Where(n => n.TENDN == tendn).Count())
                        return false;
                }
            }
            return true;
        }
    }
}