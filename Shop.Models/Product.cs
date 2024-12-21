using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CakeShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Product Name ")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DisplayName("Item Nember")]
        public int ProductId { get; set; }

        [Required]
        public string supplier { get; set; }
        [Required]
        [DisplayName("Cost Price")]
        public double ListPrice { get; set; }

        //Setting a foreign Key
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [DisplayName("Image Url")]
        [ValidateNever]
        public string  ImageUrl { get; set; }


    }
}
