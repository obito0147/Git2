using DA3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DA3.Controllers
{
    public class HomeController : Controller
    {
        DA1Entities objDA1Entities = new DA1Entities();
        public ActionResult Index()
        {
            var lstProduct = objDA1Entities.SanPhams.ToList();
            return View(lstProduct);
        }
        //chuc nang dang ky
        [HttpGet]
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objDA1Entities.Users.FirstOrDefault(s => s.Emaill == _user.Emaill);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objDA1Entities.Configuration.ValidateOnSaveEnabled = false;
                    objDA1Entities.Users.Add(_user);
                    objDA1Entities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string bye25string = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                bye25string = targetData[i].ToString("x2");
            }
            return bye25string;
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string emaill, string password)
        {
            if (ModelState.IsValid)
            {

                var f_password = GetMD5(password);
                var data = objDA1Entities.Users.Where(s => s.Emaill.Equals(emaill) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FirstName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Emaill;
                    Session["idUser"] = data.FirstOrDefault().id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("DangNhap");
                }
            }
            return View();
        }

        //Logout
        public ActionResult DangXuat()
        {
            Session.Clear();//remove session
            return RedirectToAction("DangNhap");
        }
    }
}