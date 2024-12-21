using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        //I to hold the dropdown
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
