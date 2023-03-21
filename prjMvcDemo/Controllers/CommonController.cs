using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
			if (Session[CDictionary.SK_LOGINED_USER] == null)
			{
				return RedirectToAction("Login");
			}
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
		[HttpPost]
        public ActionResult Login(CLoginViewModel vm)
        {
			CCustomer user = (new CCustomerFactory()).queryByEmail(vm.txtAccount);
			if (user != null)
			{
				if (user.fPassword.Equals(vm.txtPassword))
				{
					Session[CDictionary.SK_LOGINED_USER] = user;
					return RedirectToAction("Home");
				}
			}
            return View();
        }
    }
}