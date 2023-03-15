using DA3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA3.Models
{
    public class DanhMucSanPham
    {
        public List<SanPham> lstSanPham { get; set; }
        public List<ThuongHieu> lstThuongHieu { get; set; }
    }
}