using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Webshop.Utils.Validation;

namespace Webshop.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }

        [Display(Name= "Release date")]
        [DataType(DataType.Date)]
        [DateTimeValidation]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true)]
        public DateTime ReleaseDate { get; set; }
        public string Rating { get; set; }
        public string Description { get; set; }

        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}