using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ContactView()
        {

            return View();
        }
        public ActionResult AboutView()
        {

            return View();
        }
        public ActionResult ShopView()
        {

            return View();
        }
        public ActionResult ShopPro()
        {

            return View();
        }
    }
}