using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Webshop.Models;
using System.Web.Hosting;
using System.Drawing;
using System.Data.Entity.Validation;
using System.Net;

namespace Webshop.Areas.AdminControlPanel.Controllers
{
    public class ProductController : Controller
    {
        private WebshopEntities db = new WebshopEntities();
        //
        // GET: /AdminControlPanel/Products/
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Create()
        {
            fillCategoriesInViewbag();
            return View();
        }

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(string Name, HttpPostedFileBase file, string ReleaseDate, string Description, string categoryID)
        {
            fillCategoriesInViewbag();

            //Do input validation
            imageFileValidation(file);
            DateTime dt = releasedateValidation(ReleaseDate);

            //If input is correct then possibly save the image and add a product to the database
            if (ModelState.IsValid)
            {
                Product product = new Product() {Name = Name, CategoryID = Int32.Parse(categoryID), ReleaseDate = dt, Description = Description };

                int? imageID = null;
                if (file != null) //If a file was uploaded, then save it to a folder
                {
                    imageID = newImage(file);
                }

                product.ImageID = imageID;
                db.Products.Add(product);
                
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                db.Products.Remove(db.Products.Find(id));

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Product product = db.Products.Find(id);

                return View(product);
            }
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                fillCategoriesInViewbag();
                Product product = db.Products.Find(id);

                return View(product);
            }
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSaveChanges(string ProductID, string Name, HttpPostedFileBase file, string ImageID, string ReleaseDate, string Description, string CategoryID)
        {
            fillCategoriesInViewbag();
            Product product = db.Products.Find(Int32.Parse(ProductID));

            //Check whether a new image was added or there is no change
            int? imgID = null;
            if(file != null)
            {
                imgID = newImage(file);
            }
            else if(ImageID != null)
            {
                imgID = Int32.Parse(ImageID);
            }

            if (ModelState.IsValid)
            {
                releasedateValidation(ReleaseDate);

                product.Name = Name;
                product.ImageID = imgID;
                product.ReleaseDate = DateTime.Parse(ReleaseDate);
                product.Description = Description;
                product.CategoryID = Int32.Parse(CategoryID);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult RemoveImage(int? productid, int? imageid)
        {
            if(productid == null || imageid == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Remove image reference of the corresponding product
                    Product product = db.Products.Find(productid);
                    product.Image = null;
                    product.ImageID = null;

                    //Remove the actual image
                    Webshop.Models.Image image = db.Images.Find(imageid);
                    string path = Server.MapPath(image.Path);
                    if(System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    image = null;

                    db.SaveChanges();
                }
                
                return RedirectToAction("Edit", new { id = productid });
            }
        }

        public int newImage(HttpPostedFileBase file)
        {
            //First upload the file to the server
            string imgpath = Path.Combine(Server.MapPath("~/Images/"), file.FileName);
            file.SaveAs(imgpath);

            //Then get the image properties
            string filename = file.FileName;
            string path = "~/Images/" + file.FileName;
            Bitmap img = new Bitmap(imgpath);
            int Width = img.Width;
            int Height = img.Height;
            img.Dispose();//Release the file, otherwise error occurs that the file is in use

            //Finally add the image to the database and return the ID
            Models.Image image = new Models.Image() { Name = filename, Path = path, Width = Width, Height = Height };
            db.Images.Add(image);
            db.SaveChanges();

            return image.ImageID;
        }

        private void fillCategoriesInViewbag()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.AddRange(db.Categories.ToList().Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.Name }));
            ViewBag.Items = items;
        }

        private void imageFileValidation(HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (!file.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("file", "Must be an image");
                }
                if (file.InputStream.Length >= 1000000)
                {
                    ModelState.AddModelError("file", "Must be smaller than 1 MB");
                }
            }
        }


        private DateTime releasedateValidation(string ReleaseDate)
        {
            DateTime dt = DateTime.MinValue;
            if (ReleaseDate != null)
            {
                if(!DateTime.TryParse(ReleaseDate, out dt))
                {
                    ModelState.AddModelError("ReleaseDate", "Please enter a date");
                }

                if (dt < DateTime.MinValue)
                {
                    ModelState.AddModelError("ReleaseDate", String.Format("Must be later than {0}/{1}/{2}", dt.Day, dt.Month, dt.Year));
                }
                if (dt.Year > 2030)
                {
                    ModelState.AddModelError("ReleaseDate", "Must be earlier than 1/1/2030");
                }
            }

            return dt;
        }
	}
}