using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        /*[Display(Name="Category")]
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        [DataType(DataType.Text)]*/
        public string Name { get; set; }
    }
}