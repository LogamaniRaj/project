using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace mvc_shopping_mainproject.Models
{
    public class customerModel
    {
        [Display(Name = " Customer Id:")]
        public int CustomerId { get; set; }

        [Display(Name = " Customer Name:")]
        [RegularExpression("[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed")]
        [StringLength(100, ErrorMessage = "Max 100 characters only")]
        public string CustomerName { get; set; }


        [Display(Name = " Customer Mobile No:")]
        [Required(ErrorMessage = "Enter the First Name")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Pls Enter your 10 digit Mobile No")]
        public string CustomerMobile { get; set; }

        [Display(Name = "Customer Email Id:")]
        [Required(ErrorMessage = "Enter the email id")]
        [EmailAddress(ErrorMessage = "Invalid Email id")]
        [Remote("CheckEmailId_customer", "Home", ErrorMessage = "Emailid already been registered")]
        public string CustomerEmailId { get; set; }


        [Display(Name = "Customer Password:")]
        [Required(ErrorMessage = "Enter the Password")]
        // [Remote("CheckPwd", "Home", ErrorMessage = "Wrong Password")]
        public string CustomerPassword { get; set; }

        


        [Display(Name = "Security Question :")]
        [Required(ErrorMessage = "Enter the Security Question")]
        public string SecurityQuestion { get; set; }


        [Display(Name = "Security Answer :")]
        [Required(ErrorMessage = "Enter the Security Answer")]
        public string SecurityAnswer { get; set; }

    }
}