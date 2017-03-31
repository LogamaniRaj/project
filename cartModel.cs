using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_shopping_mainproject.Models
{
    public class cartModel
    {
        public int CartId { get; set; }
        public int  ProductId { get; set; }
        public string CustomerEmailId { get; set; }
        public DateTime AddedDate { get; set; }

        
    }
}