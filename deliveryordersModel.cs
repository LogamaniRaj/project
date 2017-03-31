using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_shopping_mainproject.Models
{
    public class deliveryordersModel
    {
        public int deliveryorderid { get; set; }
        public int orderid { get; set; }
        public DateTime deliverydate { get; set; }
    }
}