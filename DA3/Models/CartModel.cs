using DA3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA3.Models
{
    public class CartModel
    {
        public SanPham Product { get; set; }
        public int Quantity { get; set; }
    }
}