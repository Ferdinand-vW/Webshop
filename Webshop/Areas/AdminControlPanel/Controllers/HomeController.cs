using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;

namespace Webshop.Areas.AdminControlPanel.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /AdminControlPanel/
        public ActionResult Index()
        {
            Debug.WriteLine("test");
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult Statistics()
        {
            return View();
        }
	}
}