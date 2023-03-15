using DA3.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace DA3.Areas.Admin.Controllers
{
    public class ProductAdminController : Controller
    {
        DA1Entities objDA1Entities = new DA1Entities();
        // GET: Admin/ProductAdmin
        public ActionResult Index(string SearchString, string currentFilter, int? page)
        {
            var lstProduct = new List<SanPham>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                //lay danh sach san pham theo tu khoa tim kiem
                lstProduct = objDA1Entities.SanPhams.Where(n => n.name.Contains(SearchString)).ToList();
            }
            else
            {
                //lay san pham trong SANPHAM
                lstProduct = objDA1Entities.SanPhams.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            //so luong item cua trang = 4
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            //sap xep san pham theo id, san pham moi dua len dau
            lstProduct = lstProduct.OrderByDescending(n => n.id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));

        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SanPham objProduct)
        {
            try
            {
                if (objProduct.ImageUpload1 != null)
                {
                    string fileName1 = Path.GetFileNameWithoutExtension(objProduct.ImageUpload1.FileName);
                    string fileName2 = Path.GetFileNameWithoutExtension(objProduct.ImageUpload2.FileName);
                    string fileName3 = Path.GetFileNameWithoutExtension(objProduct.ImageUpload3.FileName);
                    string fileName4 = Path.GetFileNameWithoutExtension(objProduct.ImageUpload4.FileName);
                    string extension1 = Path.GetExtension(objProduct.ImageUpload1.FileName);
                    string extension2 = Path.GetExtension(objProduct.ImageUpload2.FileName);
                    string extension3 = Path.GetExtension(objProduct.ImageUpload3.FileName);
                    string extension4 = Path.GetExtension(objProduct.ImageUpload4.FileName);
                    fileName1 = fileName1 + extension1;
                    fileName2 = fileName2 + extension2;
                    fileName3 = fileName3 + extension3;
                    fileName4 = fileName4 + extension4;
                    objProduct.avatar1 = fileName1;
                    objProduct.avatar2 = fileName2;
                    objProduct.avatar3 = fileName3;
                    objProduct.avatar4 = fileName4;
                    objProduct.ImageUpload1.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName1));
                    objProduct.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName2));
                    objProduct.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName3));
                    objProduct.ImageUpload4.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName4));

                }
                objDA1Entities.SanPhams.Add(objProduct);
                objDA1Entities.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var objProduct = objDA1Entities.SanPhams.Where(n=>n.id== id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var objProduct = objDA1Entities.SanPhams.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(SanPham objSanPham)
        {
            var objProduct = objDA1Entities.SanPhams.Where(n => n.id == objSanPham.id).FirstOrDefault();
            objDA1Entities.SanPhams.Remove(objProduct);
            objDA1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var objProduct = objDA1Entities.SanPhams.Where(n => n.id == id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Edit(SanPham objSanPham)
        {
            if (objSanPham.ImageUpload1 != null)
            {
                string fileName1 = Path.GetFileNameWithoutExtension(objSanPham.ImageUpload1.FileName);
                string fileName2 = Path.GetFileNameWithoutExtension(objSanPham.ImageUpload2.FileName);
                string fileName3 = Path.GetFileNameWithoutExtension(objSanPham.ImageUpload3.FileName);
                string fileName4 = Path.GetFileNameWithoutExtension(objSanPham.ImageUpload4.FileName);
                string extension1 = Path.GetExtension(objSanPham.ImageUpload1.FileName);
                string extension2 = Path.GetExtension(objSanPham.ImageUpload2.FileName);
                string extension3 = Path.GetExtension(objSanPham.ImageUpload3.FileName);
                string extension4 = Path.GetExtension(objSanPham.ImageUpload4.FileName);
                fileName1 = fileName1 + extension1;
                fileName2 = fileName2 + extension2;
                fileName3 = fileName3 + extension3;
                fileName4 = fileName4 + extension4;
                objSanPham.avatar1 = fileName1;
                objSanPham.avatar2 = fileName2;
                objSanPham.avatar3 = fileName3;
                objSanPham.avatar4 = fileName4;
                objSanPham.ImageUpload1.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName1));
                objSanPham.ImageUpload2.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName2));
                objSanPham.ImageUpload3.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName3));
                objSanPham.ImageUpload4.SaveAs(Path.Combine(Server.MapPath("~/img/product-img/"), fileName4));

            }
            objDA1Entities.Entry(objSanPham).State = System.Data.Entity.EntityState.Modified;
            objDA1Entities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
    
