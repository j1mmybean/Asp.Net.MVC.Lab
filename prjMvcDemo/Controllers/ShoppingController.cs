using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            IEnumerable<tProduct> products = from p in (new dbDemoEntities()).tProduct
                           select p;
            return View(products);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            ViewBag.FID = id;
            return View();
        }
    }
}