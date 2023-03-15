using DA3.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA3.Controllers
{
    public class DetailController : Controller
    {
        DA1Entities objDA1Entities = new DA1Entities();
        // GET: Detail
        public ActionResult Index(int id)
        {
            var objProduct = objDA1Entities.SanPhams.Where(n => n.id== id).FirstOrDefault();
            return View(objProduct);
        }
    }
}