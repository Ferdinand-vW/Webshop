using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;

namespace Webshop.Areas.AdminControlPanel.Controllers
{
    public class CategoryController : Controller
    {
        WebshopEntities db = new WebshopEntities();

        //
        // GET: /AdminControlPanel/Category/
        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            return View(db.Categories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed(string Name)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category() { Name = Name };
                db.Categories.Add(category);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? categoryId)
        {
            if(categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Category category = db.Categories.Find(categoryId);

                return View(category);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(string CategoryID, string Name)
        {
            Category category = db.Categories.Find(Int32.Parse(CategoryID));
            category.Name = Name;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? categoryId)
        {
            if(categoryId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                //Check whether a product has this category
                if(db.Products.Select(x => x.CategoryID).Any(x => x == categoryId))
                {
                    //In that case don't allow the removal of this category
                    TempData["Error"] = "This category has a reference to one or more products";
                }
                else
                {
                    db.Categories.Remove(db.Categories.Find(categoryId));
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }
	}
}