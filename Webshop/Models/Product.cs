using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Utils.Validation;

namespace Webshop.Models
{
    public class Product
    {

        public Product()
        {
            this.NumberSold = 0;
            this.StockLevel = 0;
            this.Rating = 0;
        }
        [Key]
        public int ProductID { get; set; }

        [Required]
        [Display(Name= "Product Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name="Upload image")]
        [ForeignKey("Image")]
        public int? ImageID { get; set; }
        public virtual Image Image { get; set; }

        [Required]
        [Display(Name= "Release date")]
        [DataType(DataType.Date)]
        [DateTimeValidation]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name= "Rating")]
        public int Rating { get; set; }
        public int NumVotes { get; set; }

        [AllowHtml]
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name= "Description")]
        public string Description { get; set; }

        [Display(Name= "# Sold")]
        public int NumberSold { get; set; }

        [Display(Name= "# Inventory")]
        public int StockLevel { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}