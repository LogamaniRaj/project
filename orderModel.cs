using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mvc_shopping_mainproject.Models
{
    public class orderModel
    {
        public int OrderID { get; set; }

        public string CustomerEmailId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public int ProductPrice { get; set; }
        

      
        [Display(Name = " Product Qty :")]
        [Required(ErrorMessage = "Select the Product Qty ")]
        [RegularExpression("[1-9]{1}", ErrorMessage = "Qty must be numeric and it should be within 1-9")]
        public int ProductQuantity { get; set; }



        //[Display(Name = " Order Price:")]
        
        public float OrderPrice { get; set; }

        
        public  DateTime OrderDate { get; set; }

        [Display(Name = " Order Address :")]
        [Required(ErrorMessage = "Enter the Order Address ")]
        //[RegularExpression("^[0-9a-zA-Z#/, -]", ErrorMessage = "Please provide a valid address")]
        
        [StringLength(100, ErrorMessage = "Max 100 characters only")]
        public string DeliveryAddress { get; set; }

        
        [Display(Name="Customer Name")]
        [Required(ErrorMessage="Enter your Name")]
        [RegularExpression("[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed")]
        [StringLength(100, ErrorMessage = "Max 100 characters only")]
        public string CustomerName { get; set; }
      
        

          [Display(Name = " Delivery City :")]
        [Required(ErrorMessage = "Select the City")]
      
        public string DeliveryCity{get;set;}

          [Display(Name = " State :")]
        [Required(ErrorMessage = "Select the State ")]
       
        public string State{get;set;}

          [Display(Name = " Pin Code  :")]
          [Required(ErrorMessage = "Enter the Pin Code ")]
          [RegularExpression("[0-9]{6}", ErrorMessage = "Invalid Pincode")]
          public string PinCode { get; set; }


          [Display(Name = " Mobile No  :")]
        [Required(ErrorMessage = "Enter the Mobile No ")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Mobile no must be numeric")]
        public string MobileNo{get;set;}
         
        [Display(Name = " Payment option:")]
          [Required(ErrorMessage = "select any one type ")]
          public string PaymentOption { get; set; }


       
       
    }
}