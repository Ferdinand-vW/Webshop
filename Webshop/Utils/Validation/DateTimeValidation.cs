using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop.Utils.Validation
{
    public class DateTimeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;

            if (date != null)
            {
                return date.Year >= 1973;
            }

            return true;
        }
    }
}