using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace mvc_shopping_mainproject.Models
{
    public class administratorModel
    {
       [Display(Name = " Administrator Id:")]
        public int AdminId { get; set; }

        [Display(Name = " Administrator Name:")]
        [Required(ErrorMessage = "Enter the Name")]
      
       [RegularExpression("[a-zA-Z]*$", ErrorMessage = "Only alphabets are allowed")]
    
        [StringLength(100,ErrorMessage = "Max 100 characters only")]
        public string AdminName { get; set; }

        [Display(Name = " Administrator Mobile No:")]
         [Required(ErrorMessage = "Enter the Mobile No ")]
         [RegularExpression("[0-9]{10}", ErrorMessage = "Pls Enter your 10 digit Mobile No")]
        public string AdminMobile { get; set; }

        [Display(Name = "Administrator Email Id:")]
        [Required(ErrorMessage = "Enter the email id")]
        [EmailAddress(ErrorMessage = "Invalid Email id")]
        [Remote("CheckEmailId_admin", "Home", ErrorMessage = "Emailid already been registered")]
        public string AdminEmailId { get; set; }


        [Display(Name = "Administrator Password:")]
        [Required(ErrorMessage = "Enter the Password")]
       
        public string AdminPassword{ get; set; }

       

        [Display(Name = "Security Question :")]
        [Required(ErrorMessage = "Enter the Security Question")]
        public string SecurityQuestion { get; set; }


        [Display(Name = "Security Answer :")]
        [Required(ErrorMessage = "Enter the Security Answer")]
        public string SecurityAnswer { get; set; }

    }
}