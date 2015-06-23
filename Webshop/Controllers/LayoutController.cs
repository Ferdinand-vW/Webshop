using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class LayoutController : Controller
    {
        private WebshopEntities db = new WebshopEntities();

        public LayoutController()
        {
            ViewData["Categories"] = db.Categories.ToList();
        }
	}
}