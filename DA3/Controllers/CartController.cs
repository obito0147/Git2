using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA3.Context;
using DA3.Models;
namespace DA3.Controllers
{
    public class CartController : Controller
    {
        DA1Entities objDA1Entities = new DA1Entities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["Cart"] == null)
            {
                List<CartModel> cart = new List<CartModel>();
                cart.Add(new CartModel { Product = objDA1Entities.SanPhams.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> cart = (List<CartModel>)Session["Cart"];
                //kiem tra san pham co ton tai trong gio hang chua
                int index = isExist(id);
                if (index != -1)
                {
                    //neu sp ton tai trong gio hang thi cong them so luong
                    cart[index].Quantity += quantity;

                }
                else
                {
                    //neu khong ton tai thi them san pham vao gio hang
                    cart.Add(new CartModel { Product = objDA1Entities.SanPhams.Find(id), Quantity = quantity });
                    //tinh lai so san pham trong gio hang
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["cart"] = cart;
            }
            return Json(new {Message = "Thành công", JsonRequestBehavior.AllowGet});
        }
        private int isExist(int id)
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            for(int i = 0; i < cart.Count; i++)
                if (cart[i].Product.id.Equals(id))
                    return i;
            return -1;
        }
    }
}