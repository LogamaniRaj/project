using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Web.Mvc;

namespace mvc_shopping_mainproject.Models
{
    public class deliveryModel
    {

        [Display(Name = " Delivery Id:")]
        public int DeliveryIdNO { get; set; }

        [Display(Name = "  Name:")]
        
        [Required(ErrorMessage = "Enter the Name")]
        [RegularExpression("[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed")]
        [StringLength(100,  ErrorMessage = "Max 100 characters only")]
        public string DeliveryName { get; set; }

        [Display(Name = "  Mobile No:")]
        [Required(ErrorMessage = "Enter the First Name")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Pls Enter your 10 digit Mobile No")]

        public string DeliveryMobile { get; set; }

        [Display(Name = " Email Id:")]
        [Required(ErrorMessage = "Enter the email id")]
        [EmailAddress(ErrorMessage = "Invalid Email id")]
        [Remote ("CheckEmailId_delivery", "Home", ErrorMessage = "Emailid already been registered")]
        public string DeliveryEmailId { get; set; }


        [Display(Name = " Password:")]
        [Required(ErrorMessage = "Enter the Password")]
        // [Remote("CheckPwd", "Home", ErrorMessage = "Wrong Password")]
        public string DeliveryPassword { get; set; }



        [Display(Name = "Security Question :")]
        [Required(ErrorMessage = "Enter the Security Question")]
        public string SecurityQuestion { get; set; }


        [Display(Name = "Security Answer :")]
        [Required(ErrorMessage = "Enter the Security Answer")]
        public string SecurityAnswer { get; set; }



     
    }
}