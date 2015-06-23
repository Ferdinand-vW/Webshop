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
        }

        public ActionResult showCategories()
        {
            return PartialView("_CategoriesPartial", db.Categories.ToList());
        }
	}
}