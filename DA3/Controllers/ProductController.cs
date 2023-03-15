using DA3.Context;
using DA3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA3.Controllers
{
    public class ProductController : Controller
    {
        DA1Entities objDA1Entities = new DA1Entities();
        // GET: Product
        public ActionResult Index()
        {
            ProductModel objProductModel = new ProductModel();
            objProductModel.lstDanhMuc = objDA1Entities.DanhMucs.ToList();
            objProductModel.lstProduct = objDA1Entities.SanPhams.ToList();
            return View(objProductModel);
        }
        public ActionResult ChiTietSanPham(int id) 
        {
            DanhMucSanPham objDanhMucSanPham = new DanhMucSanPham();
            objDanhMucSanPham.lstThuongHieu = objDA1Entities.ThuongHieux.ToList();
            objDanhMucSanPham.lstSanPham = objDA1Entities.SanPhams.Where(n => n.DanhMucId == id).ToList();
            return View(objDanhMucSanPham);
        }
        public ActionResult SanPhamTheoThuongHieu(int id) 
        {
            var lstSanPham = objDA1Entities.SanPhams.Where(n => n.ThuongHieuId == id).ToList();
            return View(lstSanPham);
        }
    }
}