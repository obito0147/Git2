using DA3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA3.Models
{
    public class ProductModel
    {
        public List<SanPham> lstProduct { get; set; }
        public List<DanhMuc> lstDanhMuc { get; set; }
    }
}