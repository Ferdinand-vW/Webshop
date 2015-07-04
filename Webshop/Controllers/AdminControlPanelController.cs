using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Controllers
{
    public class AdminControlPanelController : Controller
    {
        //
        // GET: /AdminControlPanel/
        public ActionResult Index()
        {
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