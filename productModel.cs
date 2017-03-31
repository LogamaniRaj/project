using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace mvc_shopping_mainproject.Models
{
    public class productModel
    {
        [Display(Name = " Category Name:")]
        [Required(ErrorMessage = "Select A Category ")]
        
        public string CategoryName { get; set; }

        [Display(Name = " Product Id :")]
        [Required(ErrorMessage = "Enter the ProductId ")]
        public int ProductId { get; set; }


        [Display(Name = " Product Name:")]
        [Required(ErrorMessage = "Enter the Product Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Max 100 characters only")]
        public string ProductName { get; set; }

        [Display(Name = " Product Price :")]
        [Required(ErrorMessage = "Enter the Product Price ")]
        public int ProductPrice { get; set; }


        [Display(Name = " Product Description:")]
        [Required(ErrorMessage = "Enter the Product Description")]
        [StringLength(10000, MinimumLength = 2, ErrorMessage = "Max 100 characters only")]
        public string ProductDescription { get; set; }


        public string ProductImageAddr { get; set; }


        [Display(Name = " Available Qty :")]
        [Required(ErrorMessage = "Enter the Available Qty ")]
        public int AvailableQty { get; set; }
    }
}