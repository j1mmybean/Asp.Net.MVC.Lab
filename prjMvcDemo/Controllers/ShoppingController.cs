using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class ShoppingController : Controller
    {
		public ActionResult CartView()
		{
			List<CShoppingCartItem> cart =  Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
			if (cart == null)
			{
				return RedirectToAction("List");
			}
			return View(cart);
		}
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
        public ActionResult AddToSession(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("List");
            }
            ViewBag.FID = id;
            return View();
		}
		[HttpPost]
		public ActionResult AddToCart(CAddToCartViewModel vm)
		{
			dbDemoEntities db = new dbDemoEntities();
			tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
			if (prod != null)
			{
				tShoppingCart x = new tShoppingCart();
				x.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
				x.fCustomerId = 1;
				x.fProductId = vm.txtFId;
				x.fCount = vm.txtCount;
				x.fPrice = prod.fPrice;
				db.tShoppingCart.Add(x);
				db.SaveChanges();
			}
			return RedirectToAction("List");
		}
		[HttpPost]
		public ActionResult AddToSession(CAddToCartViewModel vm)
		{
			dbDemoEntities db = new dbDemoEntities();
			tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtFId);
			if (prod != null)
			{
				List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
				if (cart == null)
				{
					cart = new List<CShoppingCartItem>();
					Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] = cart;
				}
				CShoppingCartItem x = new CShoppingCartItem()
				{
					productId = vm.txtFId,
					count = vm.txtCount,
					price = (decimal)prod.fPrice,
					product = prod,
				};
				cart.Add(x);
			}
			return RedirectToAction("List");
		}
	}
}